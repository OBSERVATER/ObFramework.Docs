# ObFramework.Adapter.UnrealSharp

UnrealSharp application hosting, frame dispatch, UObject resources and paired UMG UI for ObFramework. The current project targets **.NET 10** and C# 12. It depends on UnrealSharp assemblies and source generators from a locally configured Unreal project.

| Task | Guide |
|---|---|
| Launch and stop an application through a GameInstance subsystem | [Core](Core/README.md) |
| Register and open UMG widget/View pairs | [UI](UI/README.md) |
| Load UObject assets or raw files | [Resource](Resource/README.md) |
| Inspect declared APIs | [Generated API reference](api/README.md) |

## Build/integration prerequisites

[The project file](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/ObFramework.Adapter.UnrealSharp.csproj) contains machine-specific assembly/analyzer references under an `Unreal Projects/ObFramework_example/Plugins/UnrealSharp/Binaries/Managed` installation. A portable build requires matching UnrealSharp binaries and reference paths configured for the consuming Unreal project. The project references `.NET 10` runtime assemblies and `.NET Standard 2.0` analyzer assemblies; do not substitute arbitrary versions without rebuilding/import testing.

From `Framework/`, after those paths resolve:

```powershell
dotnet build ../ObFramework.Adapter.UnrealSharp/ObFramework.Adapter.UnrealSharp.csproj
```

`ob init --engine unity` configures Unity, not UnrealSharp. Native reflection/glue generators, UClass registration, widget assets and packaged content remain responsibilities of the Unreal integration. The normal project reference to `ObFramework.Generator` does not by itself prove that attributed game source is compiled with every framework analyzer; configure that in the game build where required.

## Runtime model

`AGameLauncher.BeginPlay()` starts one application through `UObFrameworkSubsystem`, a tickable GameInstance subsystem. Business Views remain engine-independent. `UnrealUIAdapter` uses explicit factories and owns View disposal; this project currently has no generated UnrealSharp ViewBinding/control-subscription layer equivalent to Unity's.

## Verification limits

Build against the selected UnrealSharp plugin, run the real game, transition maps, open/close/reopen UI, stop with pending loads and validate cooked content paths on each target platform. Confirm Unreal object lifetime and UMG loading in the actual application.
