# Godot UI scenes

`GodotUIAdapter` pairs one native `Control` window with one engine-independent `AUIView`. Register each pair explicitly with a `PackedScene` key and a factory. The adapter never scans assemblies, preloads scenes or reads `setup.json` at runtime.

## Compose a UI plugin

This is an application `CreatePlugins()` excerpt. `applicationTree` is the running `SceneTree`; `ShopView` and `ShopWindow` are application types:

```csharp
using System.Threading.Tasks;
using ObFramework.Adapter.Godot.Resource;
using ObFramework.Adapter.Godot.UI;
using ObFramework.Integrated.Resource;
using ObFramework.Integrated.UI;

var resources = ResourcePlugin.Create(new GodotAssetAdapter());
var ui = new UIPlugin(
    assets => new GodotUIAdapter(assets),
    initialize: (adapter, ct) =>
    {
        ct.ThrowIfCancellationRequested();
        var godotUi = (GodotUIAdapter)adapter;
        godotUi.Root = UIRoot.Ensure(applicationTree);
        godotUi.Register<ShopView, ShopWindow>(
            "res://UI/ShopView.tscn",
            architecture => new ShopView());
        return Task.CompletedTask;
    });
// Return resources, ui and the application's other plugins from CreatePlugins().
```

The factory overload records the dependency on `ResourcePlugin`. `UIRoot.Ensure` creates `SceneLayer/Root`, `CommonLayer/Root` and `PopupLayer/Root` when absent; `EUILevel.Overlay` maps to Popup. An existing root must contain those paths. When the runtime host owns a newly created root, it frees it after application/plugin shutdown.

## Declare the pair

Example declarations, using the `GameArchitecture` defined in the [Core guide](../Core/README.md):

```csharp
using ObFramework.Adapter.Godot.UI;
using ObFramework.Core;
using ObFramework.Integrated.UI;

public sealed class ShopView : AUIView
{
    public string Title { get; private set; } = "Shop";

    protected override void OnOpen(IUIData? data)
    {
        Title = "Shop";
        NotifyPropertyChanged(nameof(Title));
    }
}

public partial class ShopWindow : AUIWindow
{
    public override IArchitecture GetArchitecture() => GameArchitecture.Interface;
    public override EUILevel Level => EUILevel.Common;
}
```

Save a scene with `ShopWindow` on its root `Control` at the registered path. Native control subscriptions/rendering are application code in this adapter; the declaration above establishes the pair/lifecycle and does not automatically display `Title`.

## Construction and Architecture

The adapter loads the registered `PackedScene`, instantiates it, verifies the root is a `Control` of the registered window type, hides it and adds it to the target layer. Adding it first lets `_Ready` run before the factory asks the window for Architecture.

The factory uses `adapter.Architecture` when explicitly assigned; otherwise it uses `window.GetArchitecture()`. The returned View must match registration and is attached through `ICanSetArchitecture`. Constructing the adapter before Architecture exists is therefore supported; resolve Architecture when the window is created after startup.

## Open, close and release

After startup, use the running `UIPlugin` through Architecture:

```csharp
var ui = architecture.GetPlugin<UIPlugin>();
var view = await ui.OpenAsync<ShopView>();
ui.Close(view);
```

`UIManager` calls business initialization/open/close. The adapter only reparents, brings forward, shows/hides or queues the native Control for deletion. `Release` returns the `PackedScene` resource handle and disposes the business View exactly once, even when native-node cleanup fails. Until a Godot generated binding layer exists, this adapter is the binding owner; avoid a second disposal/lifecycle-forwarding path on the Control.

Failed construction cleans up the View, native node and resource handle. Calling `Open` on a released/freed native window throws `ObjectDisposedException`; closing a native node already queued for deletion is ignored. Always assign `Root` before opening, keep resource keys valid, and release UI before stopping its resource plugin.

Sources: [adapter](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/GodotUIAdapter.cs), [window](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/AUIWindow.cs), [root](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/UIRoot.cs).
