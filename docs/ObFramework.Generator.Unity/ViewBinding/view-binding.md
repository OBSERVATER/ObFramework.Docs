# View construction and lifecycle binding

`[BindView(typeof(BusinessView), typeof(IContract))]` belongs to a Unity controller. The controller implements `IContract`; generated code constructs the business object using that controller and dependencies from the initialized Architecture.

## Minimal construction example

The following source declarations require the Framework runtime, Unity adapter, UnityEngine, and the Unity analyzer. This example demonstrates a plain business object; add `AUIView` and control bindings for a managed UI window.

```csharp
using ObFramework.Adapter.Unity.ViewBinding;
using ObFramework.Core;
using UnityEngine;

namespace Example
{
    public interface IActorDisplay
    {
        void SetVisible(bool visible);
    }

    public sealed class Actor
    {
        public Actor(IActorDisplay display, IArchitecture architecture)
        {
            Display = display;
            Architecture = architecture;
        }

        public IActorDisplay Display { get; }
        public IArchitecture Architecture { get; }
    }

    [BindView(typeof(Actor), typeof(IActorDisplay))]
    public partial class ActorController : MonoBehaviour, IActorDisplay
    {
        [SerializeField] private Renderer _renderer;

        public void SetVisible(bool visible) => _renderer.enabled = visible;
    }
}
```

Attach the controller and assign `_renderer` in Unity. The application's `UnityViewBindings` runtime must be prepared for scene/controller binding; the attribute alone does not bootstrap an Architecture. See the [Unity adapter guide](../../ObFramework.Adapter.Unity/README.md).

## Controller and contract rules

The controller must be a concrete, top-level, non-generic partial `MonoBehaviour`. Each source declaration must be partial. It must implement the accessible interface declared as the second BindView argument. It cannot already implement `IGeneratedViewBinding` or inherit a controller with a BindView-generated binding.

The business type must be a concrete accessible C# class outside the `UnityEngine.Object` hierarchy, with exactly one public constructor. Control binding additionally requires an `AUIView` business type and source declarations in the current compilation; basic View construction does not require `AUIView`.

The generator owns Unity `OnEnable`, `OnDisable`, and `OnDestroy` callbacks. A declaration with any of those names on the controller or its base hierarchy is rejected with OBFV005. Place business lifecycle logic in the business View's supported lifecycle interfaces or `AUIView` overrides. Refer to the adapter lifecycle guide for engine setup.

## Constructor dependency resolution

| Parameter shape | Generated argument |
|---|---|
| Exact declared contract interface | `(IContract)this` |
| `IArchitecture` | The initialized Architecture supplied to the binding factory |
| Unambiguous system type implementing `ISystem` | `architecture.GetSystem<T>()` |
| Unambiguous model type implementing `IModel` | `architecture.GetModel<T>()` |
| Concrete plugin type implementing `IPlugin` | Controller plugin capability lookup |

For plugin injection the controller must implement `ICanGetPlugin`; the requested plugin type must be concrete. A type that belongs to more than one of system/model/plugin categories is ambiguous. Value types, arbitrary service types, strings, by-reference parameters and unsupported constructors report OBFV004. This resolution uses existing Architecture/PluginHost registrations; it does not create another service container or auto-register missing dependencies.

## Emitted contract

The generated partial implements `IGeneratedViewBinding.ControllerType` and `CreateView(IArchitecture)` explicitly. Each direct factory call constructs an object; the runtime binding owner is responsible for binding one View to the appropriate controller lifetime. Use that owner rather than calling the internal factory repeatedly in application code.

Generated Unity callbacks forward enable/disable/destroy signals to `GeneratedViewBinding`. If control bindings are present, the same callbacks also manage display subscriptions. A native enable/disable signal is distinct from `AUIView.OnOpen`/`OnClose`, which the UI manager drives. Calling business lifecycle hooks again from the native controller duplicates that responsibility.

## Related guidance

[ViewBindingGenerator.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/ViewBindingGenerator.cs) contains the constructor classification and callback emission. Runtime ownership is described by [UnityViewBindings.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/UnityViewBindings.cs).
