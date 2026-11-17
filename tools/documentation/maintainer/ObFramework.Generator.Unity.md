# Unity generator diagnostics and validation

## Diagnostic map

Maintain the user-facing [Unity diagnostic reference](../../../docs/ObFramework.Generator.Unity/diagnostics.md) alongside descriptor changes. Business-generator diagnostics have their [own public reference](../../../docs/ObFramework.Generator/diagnostics.md).

## Source and release metadata

[ViewBindingGenerator.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/ViewBindingGenerator.cs) owns OBFV descriptors. [UIBindingSupport.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/UIBindingSupport.cs) owns OBFU descriptors, source convention analysis and planning. [UIBindingEmitter.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/UIBindingEmitter.cs) emits the control code.

`ObFramework.Generator.Unity/AnalyzerReleases.Unshipped.md` records changes planned for the next release. [AnalyzerReleases.Shipped.md](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/AnalyzerReleases.Shipped.md) records actual published versions. Keep category and severity aligned with the descriptors. Move entries to Shipped when a version is published; a local preview build does not itself create release history. These files remain in the source project because the compiler consumes them as AdditionalFiles.

## Validation workflow

From the repository root:

```powershell
dotnet test ObFramework.Test/ObFramework.Test.csproj --filter "FullyQualifiedName~ViewBindingGeneratorTest|FullyQualifiedName~UnityBindingGeneratorTest|FullyQualifiedName~UnityBindingDiagnosticsTest|FullyQualifiedName~GeneratorContractTest"
```

| Test group | What it establishes |
|---|---|
| `ViewBindingGeneratorTest` | Real emitted View factories use the existing Architecture dependencies; invalid controller/contract/constructor declarations report expected rules |
| `UnityBindingGeneratorTest` | Both generators together produce compilable, executable control bindings and command dispatch |
| `UnityBindingDiagnosticsTest` | Invalid binding examples report expected IDs and severity; valid bindings compile |
| `GeneratorContractTest` | Every descriptor has accurate release metadata; a failed executed exercise fails the test |

The source generator builds against Roslyn 4.3.1 while the test harness currently runs Roslyn 5.9. Tests use C# 9 input and Unity control substitutes. After changes to analyzer deployment, compiler APIs or native lifecycle assumptions, check real import and compilation in the supported Unity Editor. Prefab/scene behavior and IL2CPP need their own application acceptance checks.

For a new control, trace the complete path: control classification and event source in `UIBindingSupport`, value conversion, planner validation, emitted subscribe/unsubscribe and refresh/event code in `UIBindingEmitter`, then an executable test that drives the event and checks resulting state. Keep property/command naming in the shared `CdeSourceRules` implementation so both generators agree.
