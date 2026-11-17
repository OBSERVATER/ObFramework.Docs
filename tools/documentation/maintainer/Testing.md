# ObFramework.Test

The .NET 8 NUnit project tests the engine-neutral runtime, source generators, pure Editor generation core, and CLI installation contracts. It references those projects directly; its managed test build does not require compiling the Unity/Godot/UnrealSharp runtime adapters.

## Restore, build, run

Run these commands from the repository root with an SDK that can target .NET 8 and the runtime required by the test runner:

```powershell
dotnet restore ObFramework.Test/ObFramework.Test.csproj
dotnet build ObFramework.Test/ObFramework.Test.csproj --no-restore
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --no-restore
```

Normal project-reference ordering builds the runtime, both generators, Editor.Core, and CLI before tests. The CLI embeds the compiled generator DLLs, so avoid bypassing project-reference builds on a clean checkout. Building the full solution additionally involves engine-specific adapter prerequisites; the test project is the managed validation entry point.

Keep test outputs inside the checkout. `GeneratorContractTest` finds the repository by walking upward from the test output directory to the generator projects, then reads their release metadata. Copying the test DLLs to an unrelated temporary directory can break that source-checkout requirement.

The commands below assume the preceding build is current. Drop `--no-build` after a source change when a rebuild is needed.

## Test map by production project

| Production area | Test folder/fixtures | What it exercises |
|---|---|---|
| Framework / Core | `ObFramework.Test/Core/CoreArchitectureTest.cs`, `ObFramework.Test/Core/QueryTest.cs` | Architecture registration/access, current-value Query and Blackboard contracts. |
| Framework / Core/Async | [TaskTest](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Core/Async/TaskTest.cs), [ScopeTest](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Core/Async/ScopeTest.cs) | Awaitables, cancellation, task ownership, shutdown/drain behavior. |
| Framework / Core/Plugins | [Core/Plugins](https://github.com/OBSERVATER/ObFramework/tree/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Core/Plugins) | Plugin composition/order/dependencies, Architecture integration, application startup/rollback/shutdown, Resource/Config/Save/Diagnostics/UI integration. |
| Framework / Integrated/Clock | [Core/FrameLoop](https://github.com/OBSERVATER/ObFramework/tree/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Core/FrameLoop) | Tick ordering, frame dispatch, scheduling, exit gates, and rejected nested advancement. |
| Framework / Integrated/UI | [Core/UI](https://github.com/OBSERVATER/ObFramework/tree/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Core/UI), [ChildViewTest](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/UI/ChildViewTest.cs) | View registry, lifecycle, command execution, binding dispatch, child-View ownership. |
| ObFramework.Generator | [CDEGeneratorTests](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Generator/CDEGeneratorTests.cs), [CDETestHarness](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Generator/CDETestHarness.cs) | Observable properties, relay commands, valid/invalid declarations and generated behavior. |
| ObFramework.Generator.Unity | [UnityBindingGeneratorTest](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Generator/UnityBindingGeneratorTest.cs), `ObFramework.Test/Generator/UnityBindingDiagnosticsTest.cs`, [ViewBindingGeneratorTest](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Generator/ViewBindingGeneratorTest.cs) | View/controller/control binding output, cross-generator compilation, subscription lifetime and diagnostics. |
| Both generator projects | `ObFramework.Test/Generator/GeneratorContractTest.cs` | Declared diagnostic IDs/categories/severities match release tracking; test exercises returning false fail. |
| ObFramework.Editor.Core | [Editor](https://github.com/OBSERVATER/ObFramework/tree/5471678c694280782ef353e887accd18a03296e8/ObFramework.Test/Editor) | Editable template merge, destructive-change confirmation, setup snapshots, registry emission, diagnostics and Prefab hierarchy paths. |
| ObFramework.Cli | `ObFramework.Test/Cli/UnityInstallationTest.cs` | Manifest preservation, NuGet layout, optional integration source, compatible versions, backend choice and required deployment preflight. |


## Targeted commands

```powershell
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ObFramework.Test.Core.Plugins"
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ObFramework.Test.Core.QueryTest"
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ScopeTest|FullyQualifiedName~TaskTest"
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ObFramework.Test.Core.FrameLoop"
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ObFramework.Test.Core.UI|FullyQualifiedName~ObFramework.Test.UI"
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ObFramework.Test.Generator"
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ObFramework.Test.Editor"
dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --filter "FullyQualifiedName~ObFramework.Test.Cli"
```

`ScopeTest` lives in namespace `ObFramework.Test`, despite its `Core/Async` folder; the explicit class-name filter includes it. For one regression, filter on the fully qualified fixture and method name. Inspect discovered names with `dotnet test ObFramework.Test/ObFramework.Test.csproj --no-build --list-tests`.

## What a passing run establishes

Generator tests use real Roslyn drivers and compile generated output. The Unity binding harness runs the engine-neutral CDE generator and Unity binding generator together over a C# 9 compilation, using a stub Unity/adapter surface, then emits/executes representative code. This verifies their shared property/command contract; it does not load a real Unity Editor. The harness currently uses Roslyn 5.9, while the generators target a Roslyn 4.3.1 compilation baseline.

Editor.Core tests exercise source strings and pure models, with implementation linked from the deployed Editor/Core folder. CLI installation tests create temporary project-shaped directories; they do not demonstrate a full online package restore or real engine import. Runtime tests use fake adapters and controlled tasks, which make lifecycle and release assertions repeatable.

Real Unity validation remains a separate step: [run doctor with `--unity`](../../../docs/ObFramework.Cli/Unity/Doctor.md), then test the application's bootstrap scene, Prefabs, selected resource provider, and target build. Godot/UnrealSharp behavior also requires their real engine projects. Managed green tests alone do not establish scene correctness, platform builds, or IL2CPP compatibility.

## Adding a regression

Place the test under the production project's corresponding folder, then the relevant module folder. For async lifecycle behavior, use controlled completion sources to expose the ordering assertion; release held work before teardown. Clean up Architecture singleton state, owned handles, event subscriptions, temporary files, and started plugin hosts so subsequent tests do not inherit state. For generator changes, include successful output compilation/execution where applicable and assert exact diagnostic IDs/severities for rejected inputs. Update release metadata alongside changes to generator descriptors.

Run the relevant filter first, then the complete managed suite when shared runtime/generator contracts changed. Record which commands actually passed and any engine acceptance still outstanding. See the [documentation home](../../../docs/README.md) for project guides.

The [standalone documentation example](../../../examples/Documentation/README.md) consumes a chosen NuGet package version. Testing source remains with the implementation in the main repository.
