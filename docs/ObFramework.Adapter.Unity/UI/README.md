# Unity UI prefabs

A window is a pair: an engine-independent `AUIView` holds business state/lifecycle, and a Unity `AUIWindow` controller owns native control references. `UIManager` drives business initialization/open/close; generated View binding constructs and ultimately disposes the business View.

## Compose the adapter

The following is an application-composition excerpt. `root` is the initialized `UnityRuntimeHost.Current.RuntimeUIRoot`. For a custom launcher, initialize the `UIRoot` before composing the adapter. `ShopView` and `ShopWindow` are the types in the [binding example](../ViewBinding/README.md). Return the UI plugin and a `ResourcePlugin` matching your provider from the concrete `GameLauncher` template's `ConfigurePlugins()` method.

```csharp
using ObFramework.Adapter.Unity.UI;
using ObFramework.Integrated.UI;

var registry = new UIViewRegistry();
registry.Register<ShopView, ShopWindow>("UI/ShopView");
var uiPlugin = new UIPlugin(resources =>
    new UnityUIAdapter(resources, registry) { Root = root });
```

The factory overload declares the UI plugin's dependency on `ResourcePlugin`. Use the optional `initialize` callback for setup that needs to await work during plugin startup. The resource key must resolve through your provider. When supplying an existing adapter, the caller is responsible for its dependencies and lifetime.

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
