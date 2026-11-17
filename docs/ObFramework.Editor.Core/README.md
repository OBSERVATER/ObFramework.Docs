# ObFramework.Editor.Core

This .NET Standard 2.1 project provides the Unity-free UI authoring/generation core: validate setup snapshots, turn a component binding table into editable View/Window/contract source, merge regeneration with existing source, and emit a static runtime registration method.

## Source ownership

The project compiles linked source from [ObFramework.Adapter.Unity/EditorDeploy/Core](https://github.com/OBSERVATER/ObFramework/tree/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core). There are no separate implementation copies in this project. Those same files are deployed into a Unity project's Editor assembly by the CLI. Changes to generation logic belong in the linked source, and must continue to compile without `UnityEngine` or `UnityEditor` references.

The surrounding Unity Editor glue owns Prefab inspection, file reads/writes, user previews, `AssetDatabase` operations, and serialized component assignment. This core operates on strings and data records, returning files and diagnostics. Its templates contain Unity type names as emitted text, while the core itself remains engine independent.

## Task map

| Task | Entry point | Guide |
|---|---|---|
| Generate or update editable UI source | `EditorTemplateGenerator.Generate(EditorGenerationRequest)` | [Source generation and merge](Core/Generation.md) |
| Validate development UI paths/resource rules | `UISetupSnapshotFactory.Create(UISetupSection)` | [Setup and registry](Core/Registry.md) |
| Produce static adapter registrations | `UIRegistrySourceGenerator.Generate(UIRegistryGenerationInput)` | [Setup and registry](Core/Registry.md) |
| Interpret diagnostic/result objects | `EditorDiagnostic`, `EditorGenerationResult` | [Source generation and merge](Core/Generation.md) |
| Normalize stored Prefab hierarchy paths | `BindingTablePath` | [Source generation and merge](Core/Generation.md) |

See the source-derived [API inventory](api/README.md), the [CLI](../ObFramework.Cli/README.md) for deployment/configuration commands, and the [documentation home](../README.md) for other projects.

## Build and use

From the repository root, run `dotnet build ObFramework.Editor.Core/ObFramework.Editor.Core.csproj`. Use the deployed Editor tools in a real Unity project to generate a View/controller pair, assign Prefab references and confirm the emitted source imports successfully.
