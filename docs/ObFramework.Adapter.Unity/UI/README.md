# Unity UI prefabs

A window is a pair: an engine-independent `AUIView` holds business state/lifecycle, and a Unity `AUIWindow` controller owns native control references. `UIManager` drives business initialization/open/close; generated View binding constructs and ultimately disposes the business View.

## Compose the adapter

The following is an application-composition excerpt. `root` is your scene's initialized `UIRoot`; `ShopView` and `ShopWindow` are the types in the [binding example](../ViewBinding/README.md). Put this plugin construction inside `CreatePlugins()` and include a `ResourcePlugin` matching your provider:

```csharp
using System.Threading.Tasks;
using ObFramework.Adapter.Unity.UI;
using ObFramework.Integrated.UI;

var uiPlugin = new UIPlugin(
    resources => new UnityUIAdapter(resources, new UIViewRegistry()),
    initialize: (adapter, ct) =>
    {
        ct.ThrowIfCancellationRequested();
        root.Init();
        var unityUi = (UnityUIAdapter)adapter;
        unityUi.Root = root;
        unityUi.Register<ShopView, ShopWindow>("UI/ShopView");
        return Task.CompletedTask;
    });
```

The factory overload declares the UI plugin's dependency on `ResourcePlugin`. When directly supplying an already-created adapter, the caller is responsible for the complete dependency/lifetime composition. The resource key must resolve through your provider; registration does not create an address or preload an asset.

After startup, an Architecture-bound caller can open/close by business View type. This excerpt assumes `architecture` is an initialized `IArchitecture`:

```csharp
var ui = architecture.GetPlugin<UIPlugin>();
var view = await ui.OpenAsync<ShopView>();
ui.Close(view);
```

## Prepare the prefab and root

1. Add `ShopWindow` to the prefab root; registration identifies that exact root component type.
2. Keep the controller partial and use `[BindView]` so binding creates `ShopView` before display.
3. Assign every serialized control field in the Inspector.
4. Provide a `UIRoot` with direct children named `Scene`, `Common` and `Popup`, then call `Init()` and assign the adapter's `Root` before the first open.

`EUILevel.Scene` uses `SceneRoot`; `Common` uses `CommonRoot`; `Overlay` uses the root whose native child name is `Popup`. `AUIWindow.Level` selects the layer. `UIRoot.Init()` locates children; it does not create missing hierarchy objects.

## Ownership and failure

`CreateAsync` loads a `GameObject` prefab through the resource plugin, then instantiates it under an inactive temporary holder with `ObUnity.Instantiate`. Generated bindings create the View while the clone remains hidden. It verifies that the registered controller exists on the root and that the created View's exact type matches registration. The returned clone stays inactive until managed initialization/open has run.

`Open` activates the native object and explicitly refreshes bindings, including an already-active Single window that receives no additional `OnEnable`. `Close` deactivates it. Neither forwards business lifecycle methods. `Release` destroys the GameObject and disposes the prefab handle; generated binding owns the View's disposal through destruction/sweeping. Do not add a second manual View disposal path to the controller.

Creation failures destroy the clone and dispose any acquired handle before returning control. A native object destroyed outside the manager can no longer be opened; releasing its tracked instance must still dispose the prefab handle. Stop the UI plugin before its resource provider so cached/released prefab handles have a live manager to return to.

## Diagnose setup errors

| Symptom | Check |
|---|---|
| View is not registered | Call hand-written `Register<TView,TWindow>` or generated `RegisterAll` before opening. |
| Root/hierarchy missing | Assign `Root`, call `Init()` and check the three named children. |
| No bound business View | Ensure both analyzers are imported and application binding startup has completed. |
| Bound View type differs | Compare `[BindView(typeof(...), ...)]` with registry types; regenerate after renames. |
| Null controls | Assign serialized references on the prefab; run Editor UI validation. |

Sources: [UnityUIAdapter](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/UnityUIAdapter.cs), [UIRoot](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/UIRoot.cs), [UI plugin](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIPlugin.cs).
