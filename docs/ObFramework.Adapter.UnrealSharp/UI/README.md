# UnrealSharp UMG UI

`UnrealUIAdapter` pairs a native `UUserWidget`/`IUIWindow` with a business `AUIView`. Each View requires an explicit registration factory and widget-class resource path. Registration does not scan assemblies, read `setup.json` or cook/load assets in advance.

## Create the native root

Create the UMG widget class at `/Game/UI/WBP_ObUIRoot.WBP_ObUIRoot_C`. Its widget tree must contain CanvasPanels named exactly `Scene`, `Common` and `Popup`. `UIRoot` resolves them by name and fails if any are absent. `EUILevel.Overlay` maps to the native `Popup` panel.

The adapter requires a live owning `APlayerController`. Its `InitAsync` asynchronously loads this root class, creates the widget, keeps a `TStrongObjectPtr` and adds it to the owning player's screen. Init must finish before opening a window.

## Plugin composition

This is a `CreatePlugins()` excerpt. `owningPlayerController` is a live application-owned Unreal controller. `ShopView`/`ShopWindow` are the application's business/native types:

```csharp
using System.Threading.Tasks;
using ObFramework.Adapter.UnrealSharp.UI;
using ObFramework.Integrated.UI;

var adapter = new UnrealUIAdapter(owningPlayerController);
adapter.Register<ShopView, ShopWindow>(
    "/Game/UI/ShopView.ShopView_C",
    architecture => new ShopView());

var ui = new UIPlugin(
    adapter,
    initialize: (native, ct) => ((UnrealUIAdapter)native).InitAsync(ct),
    shutdown: native =>
    {
        ((UnrealUIAdapter)native).Deinit();
        return Task.CompletedTask;
    });
// Return ui with the rest of the application's plugins.
```

Unlike Unity/Godot UI, this adapter loads UMG classes directly through Unreal's class loader rather than a `ResourcePlugin`. Compose any other plugin dependencies explicitly.

Example pair declarations, using `GameArchitecture` from the [Core guide](../Core/README.md):

```csharp
using ObFramework.Adapter.UnrealSharp.UI;
using ObFramework.Core;
using ObFramework.Integrated.UI;
using UnrealSharp.Attributes;

public sealed class ShopView : AUIView { }

[UClass]
public partial class ShopWindow : AUIWindow
{
    public override IArchitecture GetArchitecture() => GameArchitecture.Interface;
    public override EUILevel Level => EUILevel.Common;
}
```

The loaded widget class must be compatible with `ShopWindow`. Creating these declarations does not create the UMG asset or its native event wiring. With no generated UnrealSharp control-binding layer in this project, implement the application's rendering/subscription code explicitly.

## Pair construction and ownership

Creation checks cancellation before/after async class loading, returns to the Unreal context, checks the owning controller, creates a collapsed widget and adds it to the layer before constructing the View. The factory receives `adapter.Architecture` if assigned; otherwise it receives the native window's Architecture. The returned View is checked against registration and attached through `ICanSetArchitecture`.

`UIManager` drives business initialization/open/close. `Open` places the widget on the correct canvas and makes it visible; `Close` collapses it. Avoid forwarding those lifecycle methods again from the native widget.

The adapter is the binding owner: `Release` removes the widget from its parent, disposes its strong reference and disposes the paired View exactly once. `Deinit` attempts to release every pair, then removes/releases the root and aggregates failures. Failed construction cleans up every already-acquired resource. The supplied shutdown callback is therefore necessary even though `UIManager` already releases its entries.

After startup, use `architecture.GetPlugin<UIPlugin>().OpenAsync<ShopView>()` and close the returned View through the same plugin. A destroyed widget cannot be reopened. Cancellation checks do not promise immediate cancellation of Unreal's underlying class-load operation.

Sources: [adapter](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/UnrealUIAdapter.cs), [root](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/UIRoot.cs), [window base](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/AUIWindow.cs).
