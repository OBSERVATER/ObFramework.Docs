# Unity View binding

The common generator produces observable properties and nested command classes. The Unity generator produces the controller factory and native control subscriptions. Keep both analyzer DLLs and their `RoslynAnalyzer` metadata installed. The factory resolves Architecture capabilities; the binding runtime owns per-controller View instances.

## Example View and controller

Place these declarations in your Unity application's source, with Unity UI installed and both generators enabled. Assign the two serialized fields in the Inspector. This example declares all application types it uses; it still requires the Bootstrap and resource registration described in [Core](../Core/README.md) and [UI](../UI/README.md):

```csharp
using ObFramework.Adapter.Unity.UI;
using ObFramework.Adapter.Unity.ViewBinding;
using ObFramework.Core;
using ObFramework.Integrated.UI;
using UnityEngine;
using UnityEngine.UI;

public interface IShopWindow { }

public partial class ShopView : AUIView
{
    [ObservableProperty] private string _title = "Shop";

    [RelayCommand]
    private void Buy() => Title = "Purchased";
}

[BindView(typeof(ShopView), typeof(IShopWindow))]
public partial class ShopWindow : AUIWindow, IShopWindow
{
    public override EUILevel Level => EUILevel.Common;
    public override IArchitecture GetArchitecture() => GameArchitecture.Interface;

    [SerializeField, UIBindData(nameof(ShopView.Title))]
    private Text _titleText;

    [SerializeField, UIBindCommand(nameof(ShopView.BuyCommand))]
    private Button _buyButton;
}

public sealed class GameArchitecture : AArchitecture<GameArchitecture>
{
    protected override void Init() { }
}
```

If combining this with the Core example, keep its single `GameArchitecture` declaration. A View must have exactly one public constructor; its parameters can use the declared contract, `IArchitecture`, an unambiguous model/system type, or a concrete plugin type when the controller implements `ICanGetPlugin`. The generator rejects unsupported or ambiguous dependency shapes; inspect `OBFV` diagnostics rather than adding a second DI container.

## Data binding

`[UIBindData(nameof(View.Property))]` defaults to OneWay: current View state is pushed into the control. `Mode = UIBindMode.TwoWay` additionally subscribes to input and writes parsed input back into a writable property. `Format = "{0:F1}"` applies composite formatting to displayed text.

| Control | Supported data | Direction |
|---|---|---|
| `UnityEngine.UI.Text`, `TMPro.TMP_Text` | Displayable View property | OneWay |
| `InputField`, `TMP_InputField` | Supported string/numeric conversion | OneWay or TwoWay |
| `Toggle` | `bool` | OneWay or TwoWay |
| `Slider` | `float` or `int` | OneWay or TwoWay |
| `Button` | Use a command binding | Command events |

Unsupported controls, missing targets, invalid direction and incompatible conversion report `OBFU` diagnostics. A read-only display property can be hand-written and notified by `[NotifyPropertyChangedFor]`. The business generator and Unity generator infer each other's declarations from original attributed source; generator execution order does not make one generator's fresh output visible to the other.

## Commands and subscriptions

`[UIBindCommand(nameof(View.BuyCommand))]` identifies a nested command type, not the private method name. Button clicks have no value argument. InputField/TMP_InputField, Toggle and Slider events can supply a compatible command value. Generated bindings update control interactability from `CanExecute` and running state.

Control/View subscriptions attach on enable, refresh current state, and detach on disable/destroy. Repeated refresh does not create duplicate handlers. `UIBindCommandAttribute.CanExecute` is currently reserved metadata; generated bindings call the command's `CanExecute` directly. Declare the business method through `[RelayCommand(CanExecute = nameof(...))]` and use `[NotifyCanExecuteChangedFor]` for dependencies.

Generated async event handlers dispatch through `GeneratedUIBindingRuntime`; exceptions are observed and logged through Unity. A fire-and-forget engine event does not itself create an application shutdown task. Business commands must cooperate with cancellation and preserve View/resource lifetime after awaits.

The binding runtime captures Unity's main-thread synchronization context during startup and configures `UIBindingDispatch` for notifications. Business state is not generally made thread-safe by this dispatch. Start the application on Unity's main thread and keep native API calls there.

## Dynamic objects and lifecycle

Use `ObUnity.Instantiate(prefab, parent)` after application initialization for dynamically created bound GameObjects/Components. It binds controllers in the clone and rolls back on binding failure. Direct `Object.Instantiate` does not run this explicit clone-binding path. Loaded scenes are scanned through scene events, including inactive child controllers.

The generator owns `OnEnable`, `OnDisable` and `OnDestroy` callbacks; declaring conflicting callbacks produces `OBFV005`. Put business behavior on View lifecycle methods. Non-window child `AUIView` instances receive initialization with null data from binding, then use `Bind(IUIData)` for reusable data. Window initialization belongs to `UIManager`. Bound Views implementing `IDisposable` are disposed once by their binding owner; inactive objects that Unity never activates are swept because Unity can omit their `OnDestroy` callback.

Sources: [attributes/runtime](https://github.com/OBSERVATER/ObFramework/tree/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding).
