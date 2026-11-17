# ObFramework.Adapter.Godot

Godot scene-tree application hosting, frame dispatch, resource loading and Control-based UI for ObFramework. The current adapter project targets **.NET 8**, while the engine-independent runtime targets .NET Standard 2.1. It references a local GodotSharp installation; it is not a self-contained engine distribution.

| Task | Guide |
|---|---|
| Start an application and preserve it across scenes | [Core](Core/README.md) |
| Register paired UI scenes and business Views | [UI](UI/README.md) |
| Load resources and raw files | [Resource](Resource/README.md) |
| Inspect declared API signatures | [Generated API reference](api/README.md) |

## Build prerequisites

[The project](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/ObFramework.Adapter.Godot.csproj) currently resolves GodotSharp through a machine-relative `HintPath` under `Program Files/Godot/GodotSharp/Api/Release`. Provide a compatible Godot .NET installation and adapt that reference in your integration environment before treating a build as portable. Its source comment records an installed GodotSharp 4.7.2/.NET 8 combination; this is evidence of the current build setup, not a support promise for all Godot releases.

From `Framework/`, after the GodotSharp reference resolves:

```powershell
dotnet build ../ObFramework.Adapter.Godot/ObFramework.Adapter.Godot.csproj
```

Godot and UnrealSharp setup are application-level integrations; `ob init --engine unity` deploys Unity tooling and does not configure this adapter. The current Godot project has a normal project reference to `ObFramework.Generator`; do not infer that this automatically installs all analyzer behavior into a consuming game's compilation. Configure generators explicitly where the game compiles attributed business source.

## Runtime boundaries

One `GodotRuntimeHost` lives under the SceneTree root and owns a `PluginApplication`. UI uses explicit View/window/resource registrations and explicit factories. This adapter currently has no generated Godot ViewBinding/control subscription layer equivalent to Unity's; implement native control wiring in the consuming application while retaining the engine-independent business View contract.

`GodotDiagnostics.Create()` configures Serilog output under the Godot user-data `Logs` directory, using Information in debug builds and Warning otherwise. Include the returned plugin in your manifest if required.

## Verify in the engine

Build with the matching GodotSharp DLL, run the application's Bootstrap scene, transition scenes, reopen UI, and exercise awaited shutdown before qualifying an engine version or export target. Confirm scene loading, signal dispatch, native destruction and exported resource access in the actual Godot project.
