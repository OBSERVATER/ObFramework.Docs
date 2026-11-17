# Generator maintenance and diagnostics

## Project boundaries

This project targets .NET Standard 2.0 and builds against Roslyn 4.3.1, the baseline selected for Unity 2022.3. It contains two incremental generators:

| Entry point | Pipeline |
|---|---|
| `CdeGenerator` | Attributed field/method candidates → semantic attribute matching → grouping and View validation → member analysis → source plus diagnostics |
| `SaveGenerator` | `CanSaveAttribute` semantic lookup → directly declared SaveField fields → snapshot source |

[CdeSourceRules.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/CDE/CdeSourceRules.cs) is linked into the Unity generator as source. Both pipelines therefore derive the same property and command names. The generators execute independently; the Unity generator infers the other generator's intended members from user-authored attributes rather than expecting its generated output as input in the same run.

For a naming change, inspect both generators and the tests that run them together. For async or UI lifecycle behavior, inspect the runtime command and View implementations as well as the emitters.

## CDE diagnostics

Maintain the user-facing [diagnostic reference](../../../docs/ObFramework.Generator/diagnostics.md) alongside descriptor changes. Save generation currently has no corresponding diagnostic range.

## Validation

Run from the repository root, with the required project dependencies restored:

```powershell
dotnet test ObFramework.Test/ObFramework.Test.csproj --filter "FullyQualifiedName~CDEGeneratorTests|FullyQualifiedName~GeneratorContractTest|FullyQualifiedName~UnityBindingGeneratorTest"
```

`CDEGeneratorTests` runs the real generator, compiles the generated C# 9 source against the runtime, then executes representative members. Its negative cases check descriptor IDs and severity. `GeneratorContractTest` checks that every declared diagnostic is recorded exactly once with accurate category and severity in the release tracking files. Unity binding tests run the two generators together against test substitutes for engine controls.

The test project currently uses Roslyn 5.9. Successful .NET tests do not establish every Unity compiler version, real Prefab import, scene behavior or IL2CPP compatibility. Use a supported real Unity Editor to verify changes affecting analyzer loading or native bindings.

## Release metadata and generated output

`ObFramework.Generator/AnalyzerReleases.Unshipped.md` records planned diagnostic changes. [AnalyzerReleases.Shipped.md](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/AnalyzerReleases.Shipped.md) records released versions. Keep entries aligned with `DiagnosticDescriptor`; move them to a real shipped version when publishing. These files are machine-consumed analyzer metadata, so they remain beside the generator project.

To inspect generated files in an application, build that application with `EmitCompilerGeneratedFiles=true` and choose `CompilerGeneratedFilesOutputPath` outside its normal source include tree. Rebuilding with generated files under an implicit `Compile` glob can compile the same members twice.

## Earlier development notes

The original `SourceGenerator.md` explored incremental provider pipelines and the first Save emitter. It predates the current contracts. In particular, `[CanSave]` requires a string key, `[SaveField]` has no key parameter, and the generated save identity is `Key`, not `SaveKey`. Use the current [save guide](../../../docs/ObFramework.Generator/Integrated/Save/save-generation.md) for application code and the actual emitters for implementation work.
