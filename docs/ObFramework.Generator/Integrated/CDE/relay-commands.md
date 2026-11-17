# Relay commands

`[RelayCommand]` emits a nested command type that captures one business View and, optionally, one event value. It does not emit a command-valued property on the View.

Use that wrapper for UI input and View-local presentation behavior. When it changes shared System/Model business state, let the relay method dispatch a separate business Command as shown in the [UI attribute workflow](../../../Framework/Integrated/UI.md#prefer-attributes-for-ui-state-and-input). Generated UI commands share the runtime command surface, but their View capture and execution state belong to the UI lifetime; business operations remain reusable without that View.

## Synchronous and asynchronous example

```csharp
using System.Threading;
using System.Threading.Tasks;
using ObFramework.Integrated.UI;

namespace Example
{
    public sealed partial class SearchView : AUIView
    {
        [ObservableProperty] private string _query = "";
        [ObservableProperty] private string _result = "";

        [RelayCommand]
        private void SetQuery(string value) => Query = value;

        [RelayCommand(CanExecute = nameof(CanSearch))]
        private async Task SearchAsync(CancellationToken ct)
        {
            await Task.Delay(10, ct);
            Result = Query;
        }

        private bool CanSearch() => Query.Length > 0;
    }
}
```

```csharp
var view = new Example.SearchView();
new Example.SearchView.SetQueryCommand(view, "framework").Execute();
var search = new Example.SearchView.SearchCommand(view);
await search.Execute(CancellationToken.None);
System.Console.WriteLine(view.Result); // framework
view.Dispose();
```

The second excerpt belongs in an async method with `using System.Threading;`. Engine bindings normally construct and dispatch these commands through the application's Architecture. This direct exercise demonstrates generated behavior without creating a window. In production code, marshal business state updates to the engine's required context when an awaited operation can leave that context.

## Supported source signatures

| Return type | Parameters | Generated base |
|---|---|---|
| `void` | None, or one event value | `AUICommand` |
| `Task` / `ValueTask` | None | `AUIAsyncCommand` |
| `Task` / `ValueTask` | One event value, or one `CancellationToken` | `AUIAsyncCommand` |
| `Task` / `ValueTask` | One event value followed by `CancellationToken` | `AUIAsyncCommand` |

Methods must be ordinary, non-static, non-generic instance methods on an eligible partial `AUIView`. A `ref`, `out`, `in`, pointer or ref-like parameter cannot be captured. Two business parameters, a cancellation token on a void method, and `Task<T>` / `ValueTask<T>` return types are rejected. Expose command results as View state. Although an `async void` method has a void signature, use `Task` or `ValueTask` for async work so completion, cancellation and faults remain observable.

`SearchAsync` generates `SearchCommand`; `Search` also generates `SearchCommand`. The suffix rule applies by name, and those methods cannot coexist as relay methods in the same View. A value command gets a constructor `(ViewType view, ValueType value)`; other commands get `(ViewType view)`.

## CanExecute

Set `[RelayCommand(CanExecute = nameof(CanSearch))]` to select an accessible instance method returning `bool`. It must be non-generic and take zero parameters, or one parameter accepting an implicit conversion from the command's event value type. It cannot take a cancellation token or a by-reference parameter.

The generator searches the View hierarchy. One matching value-taking overload takes precedence over a parameterless overload; multiple matching overloads at the selected arity are rejected. A value-taking predicate receives the captured value for `CanExecute()`, or the supplied value for `CanExecute(object?)`. Supply a correctly typed value to the latter; generated casts can reject an incompatible boxed object at runtime.

The runtime rechecks `CanExecute()` when executing. A false predicate skips business code. For controls that need to refresh interactability when a property changes, declare `[NotifyCanExecuteChangedFor(nameof(SearchCommand))]` on that property's observable backing field. The predicate is checked even when this notification attribute is absent.

## Async execution and close behavior

| Attribute option | Default | Effect |
|---|---|---|
| `AllowConcurrentExecutions` | `false` | Duplicate executions sharing a View and command name are skipped while one is running |
| `CancelOnClose` | `true` | Closing the owning View requests cooperative cancellation through a linked token |

These options apply to Task/ValueTask commands. Explicitly setting either on a void relay method reports warning OBF138 and has no effect.

Running state belongs to the View and command name, so creating a second command object does not bypass duplicate protection. Commands on different Views have independent running state. A cached, reopened View retains the state of work that has not finished. Running state is cleared in the runtime's `finally` path after success, cancellation or failure.

Cancellation requests do not force business work to stop. A relay method must accept and observe the token for cancellation-aware work. With `CancelOnClose = false`, close does not cancel that execution, so its owner must account for work continuing after the window closes. Await the returned `ValueTask` to observe completion and exceptions.

## Related guidance

Inspect the generated command type in the consuming project and observe CanExecute, running state and cancellation during its View lifecycle.

Implementation: [CdeViewEmitter.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/CDE/CdeViewEmitter.cs), [UICommand.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UICommand.cs). See [diagnostics](../../diagnostics.md) for failures OBF130–OBF139.
