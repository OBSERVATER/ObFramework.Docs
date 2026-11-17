# Observable state

`[ObservableProperty]` turns one mutable backing field in an engine-independent `AUIView` into a public property and a typed change event. Attribute definitions live in the runtime namespace `ObFramework.Integrated.UI`.

## Example

```csharp
using ObFramework.Integrated.UI;

namespace Example
{
    public sealed partial class ProfileView : AUIView
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
        private string _name = "Guest";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private bool _isDirty;

        public string DisplayName => "Player: " + Name;

        [RelayCommand(CanExecute = nameof(CanSave))]
        private void Save() => IsDirty = false;

        private bool CanSave() => IsDirty;
    }
}
```

Generated members include `Name`, `NameChanged`, `IsDirty`, `IsDirtyChanged`, and nested `SaveCommand`. Changing `Name` also emits a `PropertyChanged` notification for `DisplayName`; changing `IsDirty` emits a `CommandStateChanged` notification for `SaveCommand`.

```csharp
var view = new Example.ProfileView();
view.NameChanged += value => System.Console.WriteLine(value);
view.PropertyChanged += name => System.Console.WriteLine("Property: " + name);
view.CommandStateChanged += name => System.Console.WriteLine("Command: " + name);
view.Name = "Ava";
view.IsDirty = true;
new Example.ProfileView.SaveCommand(view).Execute();
view.Dispose();
```

## Generated contract

For `[ObservableProperty] private int _count = 5;`:

| Generated member | Behavior |
|---|---|
| `public int Count { get; set; }` | Reads and writes `_count`; the field initializer remains the initial value |
| `public event Action<int>? CountChanged` | Receives the new value after a changed assignment |
| Inherited `PropertyChanged` | Receives `"Count"`, followed by declared dependent property names |
| Inherited `CommandStateChanged` | Receives declared dependent command names |

The setter first compares with `EqualityComparer<T>.Default`. Equal assignments return immediately. A changed assignment writes the backing field, invokes the typed event, then emits property and command notifications. Assigning the backing field directly bypasses this setter and its notifications. Mutating a referenced object or collection in place also does not invoke the generated setter.

Notification callbacks execute inline with the assignment. The generated property supplies neither thread marshalling nor exception isolation for subscribers. Apply state changes on the execution context required by your engine bindings.

## Names and eligibility

| Input | Derived name |
|---|---|
| `_count` or `count` | `Count` |
| `m_count` or `m__count` | `Count` |
| `__title` | `Title` |

The owning type must be a top-level, non-generic class deriving from `AUIView`; all its source class declarations must be `partial`. The field must be an instance field that can be assigned. Static, const, readonly, pointer, function-pointer, ref-like and void field types are rejected. Generated property and event names must be valid identifiers and must not collide with existing or generated members.

`NotifyPropertyChangedFor` and `NotifyCanExecuteChangedFor` allow multiple attributes and deduplicate repeated targets. Empty target names report OBF120. The emitter does not resolve a non-empty target to prove that a property or command exists; prefer `nameof(...)` to let C# check spelling. These notifications apply when the decorated observable property changes; they do not establish a general dependency-tracking system.

## Related guidance

For command generation see [relay commands](relay-commands.md). For invalid field/View declarations see [compiler diagnostics](../../diagnostics.md).

Implementation: [CdeViewEmitter.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/CDE/CdeViewEmitter.cs), [CdeSourceRules.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/CDE/CdeSourceRules.cs).
