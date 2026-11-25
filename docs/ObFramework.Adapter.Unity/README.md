# ObFramework.Adapter.Unity

Unity lifecycle, generated View binding, UI prefab ownership and engine logging for the engine-independent ObFramework runtime. The runtime assembly targets .NET Standard 2.1; the documented integration baseline is Unity 2022.3 and C# 9. The base adapter has no Addressables or YooAsset dependency.

## Find the right guide

| Task | Guide | Source boundary |
|---|---|---|
| Start, persist and shut down an application | [Core](Core/README.md) | `AGameLauncher`, `UnityRuntimeHost`, `UnityFrameLoopBridge` |
| Register and open UI prefabs | [UI](UI/README.md) | `UnityUIAdapter`, `AUIWindow`, `UIRoot` |
| Connect View state and commands to controls | [ViewBinding](ViewBinding/README.md) | Binding attributes, generated callbacks, `ObUnity` |
| Use the optional Addressables provider | [Addressables integration](Integrations/Addressables.md) | Project-compiled integration source |
| Generate editable View/controller files and registry | [EditorDeploy](EditorDeploy/README.md) | Deployed Editor tools |
| Look up declared public/protected APIs | [Generated API reference](api/README.md) | Source declarations grouped by folder |

## Install into a Unity project

Close the Unity Editor, install the `ObFramework.Cli` tool from the feed containing your framework version, and initialize the project:

```powershell
dotnet tool install --global ObFramework.Cli
ob init --engine unity --root "D:/UnityProjects/MyGame"
ob doctor --root "D:/UnityProjects/MyGame" --json
```

For a private/local feed, use the matching CLI installation source and pass `--source <feed>` and, when necessary, `--package-version <version>` to init. Init installs packages, analyzers and Editor tools and creates an editable launcher and UI root prefab. Attach the generated `GameLauncher` to your startup scene and add your game content. See the [startup guide](Core/README.md) and [CLI installation details](../ObFramework.Cli/Commands/Init.md).

## Source build

From `Framework/`, with Unity installed:

```powershell
dotnet build ../ObFramework.Adapter.Unity/ObFramework.Adapter.Unity.csproj -p:UnityEditorPath="D:/Unity/Editor"
dotnet build ../ObFramework.Adapter.Unity/ObFramework.Adapter.Unity.csproj -p:ObfBuildEditorTools=true -p:UnityEditorPath="D:/Unity/Editor" -p:ArtifactsPath="artifacts/unity-editor-tools"
```

`UnityEditorPath` identifies an Editor directory; `UnityEditorDataRoot` can instead identify `Editor/Data/Managed`. `ObFramework.Adapter.Unity/UnityReferences.props` also supports installation discovery and `UnityEditorsRoot`. Unity DLLs are build-only references and must remain outside distributed NuGet packages. The Editor-tools mode is non-packable and needs separate outputs/intermediates to avoid mixing it with the runtime build. Optional integration sources are excluded from both normal runtime compilation and the runtime package assembly.

## Verification boundaries

Verify native scene, Prefab, PlayerLoop and resource behavior in the supported Unity Editor. Confirm serialized references, control refresh, subscriptions and command dispatch in the actual application; target-platform and IL2CPP builds need their own acceptance checks.

After import, run `ob doctor --unity --unity-editor <Unity.exe>` to check Unity import/compilation, then play the actual Bootstrap scene and exercise loading, reopening, cancellation and shutdown. Review the current-source limitations in [EditorDeploy](EditorDeploy/README.md) before relying on automatic registry discovery.

Sources: `ObFramework.Adapter.Unity/ObFramework.Adapter.Unity.csproj`, `ObFramework.Cli/Resources/UnityGuide.md`.
