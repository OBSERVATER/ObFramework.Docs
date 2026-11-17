# Architecture, commands and events

[Core API](../api/Core.md) · `Framework/Core/IArchitecture.cs`

For layer responsibilities, capability rules, business writes and System-owned Services, read [architecture conventions and recommended usage](Conventions.md). This page covers the registration and API mechanics used by those conventions.

Subclass `AArchitecture<T>`, register models/systems in `Init()`, then access the initialized instance through `T.Interface`. Models derive from `AModel`, systems from `ASystem`; their `OnInit` runs under the lifecycle coordinator after the container is built. The coordinator manages model/system initialization and reverse lifecycle cleanup. Use `RegisterPubService` for shared services and the local-service APIs for an owner's services. Services have no `IService` initialization callback; their disposal belongs to the container.

```csharp
using ObFramework.Core;

public interface IPlayer : IModel { int Health { get; } }
public sealed class Player : AModel, IPlayer
{
    public int Health { get; private set; }
    protected override void OnInit() => Health = 100;
}
public sealed class Game : AArchitecture<Game>
{
    protected override void Init() => RegisterModel<IPlayer, Player>();
}
// In a method: var health = Game.Interface.GetModel<IPlayer>().Health;
// At shutdown: Game.Interface.Deinit();
```

`SendCommand`/`SendCommandAsync` attach an Architecture before executing command logic. Commands implement `ICommand`/`IAsyncCommand`, or derive from their `A*Command` bases; result-bearing overloads return the declared result. Pass cancellation explicitly to asynchronous operations. [Queries](Query.md) read current data without refreshing it. `ICan*` interfaces express which extension methods a caller can use; the extension receiver must already belong to an Architecture.

Event messages are structs. Keep and dispose registration tokens:

```csharp
using System;
using ObFramework.Core;

public readonly struct Damage { public int Amount { get; init; } }
public static class DamageExample
{
    public static void Publish(IArchitecture architecture)
    {
        using IDisposable subscription = architecture.RegisterEvent<Damage>(e => Console.WriteLine(e.Amount));
        architecture.SendEvent(new Damage { Amount = 5 });
    }
}
```

Async registrations use `Func<T, CancellationToken, ValueTask>`; `SendEventAsync` accepts a MessagePipe publish strategy. Subscriptions must end with their business owner's lifecycle. A general async lambda is not automatically tracked by `PluginApplication.RunAsync`.

`BindableProperty<T>` offers `Bind`, `UnBind` and `SetWithoutEvent`. Equal values suppress notification; `Bind(trigger: true)` invokes the listener immediately. Its comparer is static per `T`, so `WithComparer` changes comparison for all `BindableProperty<T>` instances. UI generated observable properties use the separate View notification contract.
