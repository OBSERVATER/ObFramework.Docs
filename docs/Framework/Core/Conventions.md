# Architecture conventions and recommended usage

Use these conventions when assigning business responsibilities or adding a feature. The Controller/System/Model separation follows the same general approach as QFramework, with ObFramework-specific services, plugins and generated UI bindings. The table of capabilities below describes the current ObFramework interfaces; the design rules describe how application code should use them.

## Responsibilities

| Role | Responsibility | Typical contents |
| --- | --- | --- |
| Controller (`IController`) | Receive input, maintain presentation state and display business state | `AUIView`, application interaction handlers and native ViewControllers |
| System (`ISystem`, usually `ASystem`) | Coordinate shared business behavior across Controllers and Models | Inventory workflows, achievements and purchasing rules |
| Model (`IModel`, usually `AModel`) | Own business data and the methods that keep that data consistent | Player progress, inventory contents and their valid mutations |
| Service (`IService`) | Supply a bounded capability used by its owning System, with explicit lifetime ownership | A System's cache, transport client, serialization or storage helper |
| Utility (`Utility/`, ordinary helper types) | Supply reusable operations without implicit Architecture access | Collection helpers, algorithms and pure transformations |

There is no `IUtility` interface in the current runtime. Ordinary helpers and lifecycle-managed Services cover different needs. Plugins are a separate application composition boundary: the PluginHost starts/stops integrated features such as resources, configuration and UI in dependency order. A System uses these features without taking over their host-owned lifetime.

An engine `MonoBehaviour`, Godot node or Unreal widget that handles input/rendering belongs at the presentation boundary. Engine hosts, launchers and adapters instead supply infrastructure. Keep business rules on engine-independent Models and Systems; native Controllers forward input and bind the resulting View state.

## Current interface capabilities

These are the extension-method capabilities inherited by the interfaces today, not a security boundary. See [the interface declarations](../api/Core.md) and `Framework/Extension/ICanExtension.cs` for signatures.

| Interface | Get System / Model / Plugin | Send business Command | Run Query | Listen to Event | Send Event | Register/get own local Service |
| --- | --- | --- | --- | --- | --- | --- |
| `IController` | Yes | Yes | Yes | Yes | No | No |
| `ISystem` | Yes | No | Yes | Yes | Yes | Yes |
| `IModel` | Yes | No | Yes | Yes | Yes | No |
| `ICommand`, `ICommand<T>`, async variants | Yes | No | No | No | Yes | No |
| `IQuery<T>` | Yes | No | Yes | No | No | No |
| `IService` | Explicitly supplied dependencies | No | No | No | No | No |

Controllers inherit `ICanRegisterEvent` for listening, but do not inherit `ICanSendEvent`. Input that must publish a business Event goes through a business Command, or the System/Model called by that Command. Models can obtain Systems, other Models and plugins, but should use that access sparingly: preserve one clear owner for each piece of business state and keep workflow coordination in Systems. `IService` is an empty marker; it has no automatic Architecture attachment or `Init`/`Deinit` callbacks.

Additional `ICan*` interfaces can deliberately extend a concrete role within these conventions. Keep Event sending on the business side rather than adding `ICanSendEvent` to Controllers or reaching through their Architecture to publish directly. A plain object should receive its actual dependencies explicitly instead of acquiring an Architecture solely to look up everything.

## Communication and state changes

1. Route Controller-originated changes to System/Model business state through a business Command. A Controller may update its own display state and input drafts directly.
2. Put reusable workflows and cross-Model coordination in Systems. Put data invariants and valid mutation methods in the owning Model. Commands call those methods rather than editing unrelated public fields.
3. Notify presentation code after a business change through Events or observable properties. Data and business code do not retain native Controller/View instances for callbacks into the UI.
4. Read through a Query or a read-only Model/System interface. Direct reads are useful for simple values; a Query packages a business selection across sources. A read does not refresh, mutate, cache or automatically snapshot its sources.
5. Keep business dependencies directed toward data and supporting capabilities. Controllers call business operations; upward notifications carry data in Events/observable values instead of acquiring the presentation object.
6. Own and release every subscription with its subscriber's lifetime. Reusable UI instances need separate open-cycle subscriptions and instance-lifetime cleanup.

Controller-to-Controller navigation and View-local input handling are presentation concerns. A UI relay method can change a local draft or request window navigation directly. Once an action changes shared System/Model business state, dispatch a business Command.

## Commands and queries

Business Commands derive from `ACommand`/`AAsyncCommand` or implement their result-bearing interfaces. Queries derive from `AQuery<T>` and implement `Q()`. The Architecture attaches itself before executing either operation.

Treat them as short-lived operation objects. Constructor arguments and readonly request parameters are allowed: "no state" means they do not own persistent business state, histories or hidden cross-call caches. The framework also stores their Architecture association. Do not reuse one mutable instance across concurrent calls or different Architectures. Long-lived state belongs to its Model/System, or to an explicitly owned Service when it is infrastructure state.

This complete example routes a business write through a Command, publishes the resulting value, then reads it through a Query:

```csharp
using ObFramework.Core;
using ObFramework.Extension;

namespace Example.Architecture
{
    public readonly struct CountChanged
    {
        public int Value { get; }
        public CountChanged(int value) => Value = value;
    }

    public interface ICounterModel : IModel
    {
        int Count { get; }
        void Increment();
    }

    public sealed class CounterModel : AModel, ICounterModel
    {
        public int Count { get; private set; }
        protected override void OnInit() => Count = 0;

        public void Increment()
        {
            Count++;
            this.SendEvent(new CountChanged(Count));
        }
    }

    public sealed class IncrementCount : ACommand
    {
        public override void Execute() => this.GetModel<ICounterModel>().Increment();
    }

    public sealed class ReadCount : AQuery<int>
    {
        public override int Q() => this.GetModel<ICounterModel>().Count;
    }

    public sealed class CounterGame : AArchitecture<CounterGame>
    {
        protected override void Init() => RegisterModel<ICounterModel, CounterModel>();
    }
}
```

After initializing `CounterGame`, `CounterGame.Interface.SendCommand(new IncrementCount())` changes the count from zero to one and publishes `CountChanged(1)`. `CounterGame.Interface.SendQuery(new ReadCount())` returns one. Deinitialize the Architecture at the owning application's shutdown. The [UI guide](../Integrated/UI.md#prefer-attributes-for-ui-state-and-input) shows a View using these exact types. A feature with shared workflow logic can introduce an `ISystem` between its Commands and Models; this small counter does not need one.

## System-owned services and lifetime

Use local Services to group infrastructure under the System that needs it. The System decides how to use the service, owns its business subscriptions/work, and registers all its local service types before resolving the first one. The Architecture creates the owner's DI scope lazily on its first `GetLocalService<T>()`; registrations added after that scope exists are not rebuilt into it.

```csharp
using System;
using System.Collections.Generic;
using ObFramework.Core;
using ObFramework.Extension;

namespace Example.Services
{
    public interface ISessionCache : IService
    {
        void Remember(string key, int value);
    }

    public sealed class SessionCache : ISessionCache, IDisposable
    {
        private readonly Dictionary<string, int> _values = new Dictionary<string, int>();
        public void Remember(string key, int value) => _values[key] = value;
        public void Dispose() => _values.Clear();
    }

    public interface ISessionSystem : ISystem { }

    public sealed class SessionSystem : ASystem, ISessionSystem
    {
        private ISessionCache? _cache;

        protected override void OnInit()
        {
            this.RegisterLocalService<ISessionCache, SessionCache>();
            _cache = this.GetLocalService<ISessionCache>();
            _cache.Remember("last-opened-tab", 0);
        }

        protected override void OnDeinit()
        {
            // End this System's subscriptions/work; the Architecture disposes the service scope.
            _cache = null;
        }
    }
}
```

Register this System with `RegisterSystem<ISessionSystem, SessionSystem>()` inside the application's Architecture `Init()`. The owning System is the key to its local service scope: another System registering the same service contract gets its own scope/instance. Resolve the registered interface, and treat the returned service as borrowed. Let the container dispose a resolved service implementing `IDisposable`; the System does not manually dispose it a second time.

The current lifetime boundary is Architecture teardown, not an independently unloadable System. Models initialize before Systems; successful model/system initialization is unwound in reverse order. During Architecture deinitialization, System/Model `OnDeinit` callbacks run before the local service scopes are disposed, then the root container releases shared services. Calling one System's lifecycle callback directly does not independently release its service scope. Arrange asynchronous draining in the owning application/plugin before synchronous Architecture teardown.

For Architecture-wide shared infrastructure, `RegisterPubService` registers a root-container singleton that can be supplied through DI. `IService` itself does not create initialization callbacks or manage a System's lifecycle. Use [plugins](Plugins.md) when an independently started/stopped feature and asynchronous dependency ordering are required. See [the lifecycle coordinator](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ArchitectureLifecycleCoordinator.cs) for the exact ownership boundaries.

## Applying the conventions

Before inspecting or changing a System, use the CLI [reference workflow](../../ObFramework.Cli/Templates/README.md#dependency-workflow) to inspect both its dependencies and dependents. After a change, update the manually maintained graph with the supported node/edge commands and inspect it again. The graph records known dependencies; the command does not discover C# dependencies automatically.

Start with the responsibilities your feature actually needs. Use Models for authoritative data, Controller/View state for presentation, and a Command for the first user action that changes business state. Add a System when workflows must be shared, and a local Service when that System needs a managed supporting capability. Prefer [UI attributes](../Integrated/UI.md#prefer-attributes-for-ui-state-and-input) for repetitive property/command/control wiring while keeping domain behavior in the business layer.
