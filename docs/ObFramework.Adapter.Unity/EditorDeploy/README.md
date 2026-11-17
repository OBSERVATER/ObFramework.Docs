# Unity Editor UI tooling

`ob init` deploys these sources into the consuming Unity project as Editor tools. The runtime adapter excludes `EditorDeploy/**/*.cs`. Platform-independent validation/template code is linked into the separate [ObFramework.Editor.Core project](../../ObFramework.Editor.Core/README.md); Unity-only glue stays thin.

## Authoring workflow

1. Configure `setup.json` through the CLI's Unity configuration flow. The Editor searches from the Unity project root upward and validates that `root` points to the current project.
2. Select native controls and run **Tools/OBF/UI/Bind Selected Controls**, or use a component's **OBF: Add to UI Binding Table** context menu.
3. Inspect **Tools/OBF/UI/Binding Table** to choose View members/commands and binding direction/format.
4. Run **Tools/OBF/UI/Generate View+VC**. Preview generated changes, assign serialized component references on the prefab and implement behavior in the editable View.
5. After successful script compilation, run **Tools/OBF/UI/Generate UI Registry** and inspect the generated registrations. Explicit hand-written registration remains available.
6. Run **Tools/OBF/UI/Validate**, then import/compile and play the actual Bootstrap scene.

The relevant setup fields are `modules.unity.scriptOutputRoot`, `uiAssetRoot`, `resourceKeyFormat` and optional `resourceKeyOverrides`. These are generation-time inputs. The runtime consumes explicit registrations and never reads `setup.json` or scans assemblies to find UI.

## Generated-file ownership

View and controller templates are application-owned editable source. Regeneration adds missing observable fields, command stubs and lifecycle hooks while preserving existing business members and formatting. Binding Mode/Format updates can update generator-owned declarations. Deleting or retargeting serialized fields is represented as a destructive change requiring explicit confirmation because it can invalidate prefab references.

The static UI registry is replaceable generated output. Its `RegisterAll` uses the same adapter registration API as hand-written startup. Generating it does not assign native resource addresses or preload any prefab.

The binding table stores component paths and serializable state used to resolve selected controls. After hierarchy changes, inspect unresolved references rather than assuming old paths still identify the correct control.

## Validation levels

| Check | What it establishes |
|---|---|
| Editor validation | Setup/path/template/registry input diagnostics and missing prefab/control references |
| `ObfBuildEditorTools=true` build | Editor glue compiles with the adapter sources and local Unity DLLs |
| `ob doctor --unity` | Real Unity imports and compiles the deployed project; its batch entry point waits for compilation/update to finish |
| Play Mode/platform build | Scene registration, resource content, binding activation, cleanup and target-specific behavior |

The build-only Editor mode compiles glue and runtime sources together. It cannot catch an accidental use of an adapter-internal type that the separately compiled deployed Editor assembly cannot access. Keep such dependencies on public API and verify actual Unity import.

## Current source limitations

Registry discovery currently checks the base type name `ObFramework.Integrated.UI.AUIWindow`, while the adapter declares `ObFramework.Adapter.Unity.UI.AUIWindow`. Until that mismatch is corrected, **Generate UI Registry** can omit window types; inspect the output and use explicit `Register<TView,TWindow>` for application startup. This is a known source discrepancy, not a guarantee that discovery succeeds.

`UnityDiagnostics.Create()` also contains a conditional branch using `Loglevel.Information` under `UNITY_EDITOR || DEVELEMENT_BUILD`; that identifier is not declared in this project, and the development-build symbol is spelled as shown. A compiled DLL built without those symbols and source compiled with them can therefore behave differently. Inspect/fix that branch before adopting a source-based build under those symbols. Direct `DiagnosticsPlugin` composition can use explicit Serilog levels.

## Evidence and source map

- [Menus](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/ObfUiMenus.cs), [setup loader](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/ObfUiSetup.cs), [registry discovery](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/ObfUiRegistryCommands.cs), `ObFramework.Adapter.Unity/EditorDeploy/ObfBatchValidation.cs`.
