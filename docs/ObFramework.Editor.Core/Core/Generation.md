# Editable UI source generation

`EditorTemplateGenerator.Generate` receives the selected type names, Architecture, UI level, output paths, binding table, and any existing editable source. It returns `GeneratedFile` records rather than touching disk.

## Minimal pure example

This fragment can run in a console program referencing `ObFramework.Editor.Core`; it needs no Unity references to generate source text. The emitted source itself belongs in a Unity game with the runtime adapter and source generators installed.

```csharp
using System;
using ObFramework.Editor.Core;

var request = new EditorGenerationRequest
{
    ViewName = "ShopView",
    Namespace = "Game.UI",
    ArchitectureTypeFullName = "Game.GameArchitecture",
    Level = "Common",
    OutputDirectory = "Assets/Generated/UI",
    PrefabPath = "Assets/ObFramework/UI/ShopView.prefab"
};
request.Rows.Add(new BindingTableRow
{
    ComponentName = "Title",
    Kind = UIComponentKind.Text,
    FieldName = "titleText",
    ViewMemberName = "Title"
});
request.Rows.Add(new BindingTableRow
{
    ComponentName = "Buy",
    Kind = UIComponentKind.Button,
    FieldName = "buyButton",
    ViewMemberName = "BuyCommand"
});

var result = EditorTemplateGenerator.Generate(request);
foreach (var diagnostic in result.Diagnostics)
    Console.WriteLine(diagnostic);
if (!result.Success)
    throw new InvalidOperationException("Generation failed; do not write files.");
foreach (var file in result.Files)
    Console.WriteLine($"{file.Kind}: {file.Path}; changed={file.IsModified}");
```

The default names derived from `ShopView` are `ShopWindow` and `IShopContract`. Initial output contains editable partial View, Window, and contract files. The View uses `[ObservableProperty]` fields and `[RelayCommand]` methods. The Window uses the chosen Architecture and the runtime adapter's binding attributes; the Roslyn generators later provide property/command/binding implementations. Set `GenerateContractInterface=false` if the contract is maintained elsewhere.

## Binding shapes

| Kind | Generated View state | Default binding |
|---|---|---|
| `Button` | Relay command | Command invocation |
| `Text`, `TmpText` | `string` | One-way display |
| `InputField`, `TmpInputField` | `string` | Two-way input |
| `Toggle` | `bool` | Two-way input |
| `Slider` | `float` | Two-way input |

`Mode` overrides a data row's default; `Format` becomes a `UIBindData.Format` value. `ControlTypeNameOverrides` supports project control subclasses by kind. Unknown kinds, invalid identifiers, duplicate serialized fields, and conflicting state types produce diagnostics. `ViewMemberName` accepts command method/generated-command names such as `Submit`, `SubmitAsync`, or `SubmitCommand`. Rows are processed in order.

Output paths must be project-relative `.cs` paths. Explicit `ViewOutputPath`, `WindowOutputPath`, and `ContractOutputPath` override the default directory-derived paths. The supported levels are `Scene`, `Common`, and `Overlay`. Omitting the Architecture emits a warning and a `GetArchitecture()` placeholder throwing `NotImplementedException`; generation success therefore does not mean the game is ready to run.

## Regeneration and confirmation

Supply `ExistingViewSource`, `ExistingWindowSource`, and `ExistingContractSource` with current file contents before regenerating. The merger preserves hand-written members/attributes, adds missing declarations, and reports byte-identical output with `IsModified=false`. An existing target class/interface that cannot be edited safely, such as a non-partial type, produces an error instead of a replacement file. The merger is a focused source-text editor, not a substitute for compiler validation.

Changing a generated serialized field's target or deleting its binding produces a `DestructiveChange`. Unconfirmed changes remain pending; a successful result may still have `HasPendingDestructiveChanges=true`. The Editor must present those changes and, after explicit user confirmation, regenerate with only the selected IDs in `ConfirmedDestructiveChangeIds`. IDs are stable, for example `EditorGenerationResult.WindowFieldChangeId("titleText")` yields `window-field:titleText`. Confirmed changes report `Applied=true`.

The caller checks `Success`, handles pending changes, writes only appropriate modified files, imports them, and assigns Prefab references. The core does not show a dialog or authorize destructive changes itself.

## Hierarchy paths and diagnostics

`BindingTablePath.MakeRelative(storedPath, prefabRootName)` accepts already relative paths and normalizes older paths prefixed by the root name. The root itself becomes an empty path. `CombinePath` joins caller-supplied names in root order. These are hierarchy lookup paths, separate from the Prefab asset path and runtime resource key.

| Diagnostic family | Meaning |
|---|---|
| `OBFE001`–`OBFE005` | Setup root, module paths, resource rules, missing section, or override parsing. |
| `OBFE101`–`OBFE105` | Invalid/duplicate registry entries or resource keys, empty registry, unused overrides. |
| `OBFE201`–`OBFE204` | Identifiers, output paths, UI level, or missing Architecture. |
| `OBFE301`–`OBFE306` | Binding row/control/duplicate/Prefab issues or an empty table. |
| `OBFE401`–`OBFE403` | Pending/applied destructive changes or uneditable existing source. |

Inspect `EditorDiagnostic.Severity`, `Code`, `Message`, and `Target`; `IsError` controls generation failure. Warnings can accompany usable output. Exact constants and signatures live in the API inventory and [EditorDiagnostic.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorDiagnostic.cs).

Sources: [request](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationRequest.cs), [entry point](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorTemplateGenerator.cs), [result contract](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationResult.cs), [binding rows](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTableRow.cs), [path helper](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTablePath.cs).
