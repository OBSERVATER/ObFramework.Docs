# Business Views and native windows

[UI API](../api/Integrated/UI.md) · [View source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/AUIView.cs) · [Manager source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIManager.cs)

`AUIView` holds engine-independent business state and lifecycle. An engine VC implements `IUIWindow` and owns rendering objects. `IUIAdapter` creates a `UIViewInstance` pair, shows/hides it and releases it. Register pairs explicitly with the adapter's `UIViewRegistry`; runtime registration does not scan assemblies, read setup.json or preload resources.

## Prefer attributes for UI state and input

Use C# attributes for repetitive property notifications, UI command wrappers and native control wiring. Keep the business behavior explicit: generated binding connects a control to a View, while a business Command changes shared Model/System state. A View may update its own presentation properties or input drafts directly.

| Attribute | Recommended use |
| --- | --- |
| `[ObservableProperty]` | Generate a View property and change notifications from one backing field |
| `[NotifyPropertyChangedFor]` | Refresh a derived display property when its input changes |
| `[RelayCommand]` | Generate a UI input wrapper around a View method, including optional CanExecute/async options |
| `[NotifyCanExecuteChangedFor]` | Refresh command availability when a relevant View property changes |
| `[BindView]` (Unity) | Generate the native Controller-to-business View construction/lifecycle bridge |
| `[UIBindData]` (Unity) | Bind native control data to View properties; default to OneWay for display |
| `[UIBindCommand]` (Unity) | Bind native input to a generated nested UI command type |

The business attributes require `ObFramework.Generator`; the Unity-specific attributes additionally require `ObFramework.Generator.Unity`. Declare generated classes `partial`. Unity control fields still need their native references assigned in the Inspector. Godot/UnrealSharp adapters do not gain Unity-generated binding from these attributes; use their adapter-specific wiring.

This View uses the complete `CountChanged`, `IncrementCount`, `ReadCount` and `CounterGame` definitions in [architecture conventions](../Core/Conventions.md#commands-and-queries). Include those types and the business generator in the consuming project. Bind the View to its initialized Architecture through the engine binding layer before opening it:

```csharp
using System;
using Example.Architecture;
using ObFramework.Extension;
using ObFramework.Integrated.UI;

namespace Example.Presentation
{
    public sealed partial class CounterView : AUIView
    {
        [ObservableProperty] private int _count;
        private IDisposable? _subscription;

        protected override void OnOpen(IUIData? data)
        {
            ClearSubscription();
            _subscription = this.RegisterEvent<CountChanged>(change => Count = change.Value);
            Count = this.Query(new ReadCount());
        }

        [RelayCommand]
        private void Increment() => this.SendCommand(new IncrementCount());

        protected override void OnClose() => ClearSubscription();
        protected override void OnDestroy() => ClearSubscription();

        private void ClearSubscription()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
    }
}
```

Opening the registered View displays the Model's current count. A native command binding to `nameof(CounterView.IncrementCommand)` invokes the relay method, which dispatches `IncrementCount`. The Model publishes `CountChanged`; the View updates its generated `Count` property, and the data binding refreshes the control. Closing removes the subscription; reopening replaces it and reads the current state again.

The View inherits Event listening through `IController`/`ICanRegisterEvent`. Controllers do not inherit `ICanSendEvent`: business Events are emitted by the Command/System/Model side, and the View does not publish them directly.

Use TwoWay data bindings for editable View-local drafts. Commit business changes through a command and validate the business invariant in its Model/System; a UI CanExecute condition alone is not authoritative validation. Write generated properties such as `Count` rather than their backing fields after initialization, so notifications can run. When an attribute already owns a notification, listener or lifecycle callback, use its supported View extension points instead of duplicating that generated wiring.

For a complete native Controller example and control compatibility rules, see [Unity View binding](../../ObFramework.Adapter.Unity/ViewBinding/README.md) and [control bindings](../../ObFramework.Generator.Unity/UIBinding/control-bindings.md). Engine-specific factory/constructor rules are in [Unity](../../ObFramework.Adapter.Unity/README.md), [Godot](../../ObFramework.Adapter.Godot/README.md) and [UnrealSharp](../../ObFramework.Adapter.UnrealSharp/README.md). Unity generated binding requires exactly one public constructor; it may be parameterless or take supported dependencies, including optional `IArchitecture`.

## Open, close and reuse

`OnInit(data)` runs once for a managed instance; `OnOpen(data)` runs for each open, including a Single reopen. `OnClose()` runs when closing to the cache and clears latest open data. Cache eviction or shutdown releases the pair and disposes the View, invoking `OnDestroy()` once. `Close<T>` closes the latest active instance; `CloseAll<T>` closes every active instance. `TryGet<T>` reads the latest active instance. `EUIOpenMode.Multiple` permits multiple instances.

`Push(view)` is valid for active Common-level windows, stores type/data/open mode and closes the instance. `Pop<T>()` reopens the matching top entry and restores the entry on a reopen failure. It is navigation metadata, not a guarantee that the same native object is returned.

`Bind(IUIData)` reassigns data to a reusable child through `OnBind`. It does not rerun initialization/open/close and does not cancel business work. Native enabled/disabled callbacks use the separate `IViewEnabled`/`IViewDisabled` contract.

## Properties and commands

Generated observable setters raise `PropertyChanged` with a property name. Command state uses `CommandStateChanged`, and `IsCommandRunning(name)` reports View-local execution state. Generated relay commands support CanExecute, running-state exclusion and optional cancellation on close; cancellation remains cooperative. Binding dispatch observes asynchronous command failures rather than leaking exceptions from native events. See [business generator](../../ObFramework.Generator/README.md) and [Unity binding generator](../../ObFramework.Generator.Unity/README.md).

UI manager synchronization is not a general engine-thread dispatcher. Apply native changes on the engine's required context. An application owns the resource provider; the UI plugin's factory overload declares its ResourcePlugin dependency and owns its created adapter. See [plugin ownership](../Core/Plugins.md).
