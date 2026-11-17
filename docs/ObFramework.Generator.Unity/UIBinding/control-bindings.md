# Unity control bindings

Controller fields marked `[UIBindData]` display business View state. Fields marked `[UIBindCommand]` dispatch generated or compatible hand-written nested UI commands. Attribute types and `UIBindMode` are in `ObFramework.Adapter.Unity.ViewBinding`.

## Complete source example

This example requires both generators, the Framework runtime, Unity adapter and UGUI. It defines the source types; the application must initialize its Architecture/binding runtime and register the window with its UI adapter.

```csharp
using ObFramework.Adapter.Unity.UI;
using ObFramework.Adapter.Unity.ViewBinding;
using ObFramework.Core;
using ObFramework.Integrated.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Example
{
    public interface IProfileWindow { }

    public sealed class Game : AArchitecture<Game>
    {
        protected override void Init() { }
    }

    public partial class ProfileView : AUIView
    {
        [ObservableProperty] private string _title = "Profile";
        [ObservableProperty] private int _age = 18;

        public ProfileView(IProfileWindow window) { }

        [RelayCommand]
        private void Birthday() => Age++;
    }

    [BindView(typeof(ProfileView), typeof(IProfileWindow))]
    public partial class ProfileWindow : AUIWindow, IProfileWindow
    {
        public override IArchitecture GetArchitecture() => Game.Interface;
        public override EUILevel Level => EUILevel.Common;

        [SerializeField, UIBindData(nameof(ProfileView.Title))]
        private Text _title;

        [SerializeField, UIBindData(nameof(ProfileView.Age), Mode = UIBindMode.TwoWay)]
        private InputField _age;

        [SerializeField, UIBindCommand(nameof(ProfileView.BirthdayCommand))]
        private Button _birthday;
    }
}
```

Assign the three fields on a Prefab. Opening it through the initialized UI manager should display `Profile` and `18`. Entering a parseable integer updates `Age`; clicking Birthday increments it and refreshes the input. Replace the sample `Game` with the Architecture initialized by your application and select it through `GetArchitecture()`, as described in the [Unity adapter](../../ObFramework.Adapter.Unity/README.md).

## Supported controls

| Control, including subclasses | Data display | TwoWay write event | Command event/value |
|---|---|---|---|
| `UnityEngine.UI.Text`, `TMPro.TMP_Text` | Text conversion or composite format | Unsupported | Unsupported |
| `UnityEngine.UI.InputField`, `TMPro.TMP_InputField` | Text conversion or composite format | `onValueChanged`, string | `onEndEdit`, string |
| `UnityEngine.UI.Toggle` | `bool` → `isOn` | `onValueChanged`, bool | `onValueChanged`, bool |
| `UnityEngine.UI.Slider` | `float`, `int`, `double`, `long` → float `value` | `onValueChanged`, float | `onValueChanged`, float |
| `UnityEngine.UI.Button` | Unsupported | Unsupported | `onClick`, no value |

`UIBindData.Mode` defaults to `OneWay`. A TwoWay target needs an accessible setter. Only one TwoWay control may write a given property in a controller; several OneWay controls may display it. A single field cannot carry both binding attributes. Separate fields can refer to the same native control when data and command behavior are needed; programmatic data refresh is guarded so it does not dispatch a command as user input.

`Format = "{0:F1}"` applies `string.Format` to textual display. Without it the emitter uses `Convert.ToString`. Formatting is not a custom parser; malformed composite formats can fail at runtime. Hand-authored readable properties are supported, but the View must emit the appropriate `PropertyChanged` notification when they change. A direct backing-field or in-place collection mutation does not trigger a generated property setter.

## Input conversion

| Event source | Supported target types |
|---|---|
| String input | `string`, `object`, `int`, `long`, `float`, `double`, `decimal` |
| Toggle bool | `bool`, `object` |
| Slider float | `float`, `double`, `object`, `int`, `long` |

Numeric string conversion uses the target type's ordinary `TryParse`. Invalid text skips the update or command. Slider-to-integer conversion uses `Math.Round` followed by an integer cast. Parsing/formatting use the ordinary .NET defaults in this implementation; no explicit invariant-culture or configurable converter is emitted. Toggle data display specifically requires `bool`; Slider data display supports the numeric types in the control table, even though the command conversion table also accepts `object`.

## Command targets and state

Use the nested command type name, such as `nameof(ProfileView.BirthdayCommand)`, rather than the relay method name. Button clicks have no value, so they cannot feed commands that require an event value. Input controls can supply values using the conversion table above. A command with no value can ignore an input control's event value.

The generated handler dispatches through `GeneratedUIBindingRuntime` and the business View's Architecture. Command execution checks CanExecute again. Interactability follows CanExecute and the running state for async commands that disallow concurrency. For generated value commands, refresh uses a control's current bool/float value; numeric text parsing may fall back to the previously stored value during command-state refresh.

Hand-written nested commands must derive from `AUICommand` or `AUIAsyncCommand` and expose a compatible public convention constructor: the generator analyzes View and/or value constructors, with parameterless/value-only alternatives for hand-written commands. Plain `ICommand` implementations do not satisfy the UI plugin dispatch contract. Read [UIBindingSupport.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/UIBindingSupport.cs) when adding a new hand-written constructor shape. The `UIBindCommand.CanExecute` property is currently reserved and unused; configure a relay method's CanExecute attribute or implement the command predicate itself.

Async failures crossing the engine event boundary are observed and reported through Unity's exception logger. Cancellation remains cooperative; command close/concurrency defaults come from the [business command runtime](../../ObFramework.Generator/Integrated/CDE/relay-commands.md).

## Display subscription lifetime

Generated bindings store the constructed View and subscribe once per display cycle. Initial bind and re-enable refresh all controls. Disable and destroy remove View and control listeners; re-enable resubscribes and shows the current values. Repeated `RefreshBindings()` calls refresh values without duplicating subscriptions. A generated public `View` accessor is added if the controller has no member named `View`.

Property and command notifications can arrive after awaited work. The emitter has main-thread dispatch guards for those notification handlers; the application still needs to initialize the adapter's dispatcher and obey native engine threading requirements. This does not make arbitrary View state mutation thread-safe.

## Related guidance

In the real Unity Prefab, verify serialized references, initial display, input conversion, listener lifetime and command state across open, close and reopen. Check any chosen TMP controls against the installed TextMeshPro package.
