# Unity diagnostics

`ob doctor` checks the selected Unity project's installation. By default it reads project/configuration/package files and queries the installed SDK; it does not import or compile the project.

```powershell
ob doctor --root "D:/UnityProjects/MyGame"
ob doctor --root "D:/UnityProjects/MyGame" --json
ob doctor --root "D:/UnityProjects/MyGame" --unity --unity-editor "D:/Unity/Editor/Unity.exe"
```

`--root` defaults to the current directory. `--unity-editor` takes the executable file, not the Editor directory. `UNITY_EDITOR_PATH` is the fallback when that option is omitted.

## Checks

| ID | Subject |
|---|---|
| `OBD001` | Unity project markers and minimum Unity 2022.3 version. A failure stops subsequent static checks. |
| `OBD002` | An installed .NET SDK 8 or newer. |
| `OBD010` | `setup.json`, its root, and registered module paths. |
| `OBD020` | A recognized NuGetForUnity dependency. |
| `OBD030` | NuGet files, selected framework versions, restored packages, one DLL per framework assembly, and no Addressables references in the base runtime assemblies. |
| `OBD040` | Owned Editor payload files/checksums, required current files, and generator `RoslynAnalyzer` labels. |
| `OBD050` | Duplicate framework/provider DLLs and Unity engine DLLs bundled into the NuGet repository. |
| `OBD060` | The selected optional Addressables version and integration source/asmdef. |
| `OBD070` | Configured Luban tool files; missing tools become a warning when initialization explicitly recorded a skipped download. |
| `OBD900` | Actual Unity import and compilation; pending unless requested and executed successfully. |

Statuses are `pass`, `fail`, `warning`, and `pending`. The exit code is `0` when no check failed, even with warnings or a pending Unity check. Treat `OBD900=pass` as the requirement when automation needs evidence of engine compilation.

## JSON contract

`--json` emits an object with `schemaVersion` (currently `1`), `root`, `success`, and a `checks` array. Each check contains `id`, `status`, and `message` in camelCase. Consumers should inspect identifiers/statuses rather than matching human-readable message text.

## Engine execution and recovery

With `--unity`, all static checks must first have no failures. Doctor then requires a closed Editor and launches batch mode with `-nographics`, the project path, and `ObFramework.Adapter.Unity.EditorTools.ObfBatchValidation.Run`. This writes Unity `Library` state and `.obf/doctor-unity.log`; it has a ten-minute process timeout. Failure adds `OBD900=fail` with the log location. If static checks fail, compilation stays pending.

Resolve the reported prerequisite before rerunning. Examples include updating an explicitly relocated setup root, restoring packages, preserving/moving modified deployment files before init, or removing duplicate installed framework assemblies. Doctor reports issues; default execution does not repair them.

Unity compilation alone does not validate gameplay scenes, resource catalogs, target platforms, or IL2CPP. Follow a passing engine check with the application's runtime acceptance steps.

Sources: `ObFramework.Cli/Commands/DoctorCommand.cs`, `ObFramework.Cli/Unity/UnityDoctor.cs`, `ObFramework.Cli/Unity/UnityProject.cs`.
