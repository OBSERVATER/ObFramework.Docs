# Initialization

`ob init` prepares `setup.json` first. For Unity, it installs packages and integrations, prepares the business test scaffold, creates the launcher and UI root assets, and deploys Editor tools. It installs Luban last. Initialization is incremental and can be retried after fixing an error. Earlier configuration or package writes can remain when a later step fails.

## Options

```text
ob init [--root <DIR>] [--engine <ENGINE>]
        [--package-version <VERSION>] [--source <FEED>]
        [--skip-packages] [--skip-download]
```

| Option | Behavior |
|---|---|
| `--root` | Defaults to the current directory; converted to an absolute path. |
| `--engine` | `auto` by default; accepts `unity`, `godot`, `unreal`, or `unrealsharp`. |
| `--package-version` | Selects matching `ObFramework` and `ObFramework.Adapter.Unity` versions; defaults to the CLI informational version without build metadata. |
| `--source` | Additional NuGet feed. Relative filesystem paths are resolved against the working directory. |
| `--skip-packages` | Skips framework NuGet restoration. With Addressables selected, UPM configuration and integration source deployment still run. |
| `--skip-download` | Skips Luban installation; other steps still run. A network failure during the Luban download also lets initialization continue and records the skip. |

Auto detection looks first for Unity `Assets/` plus `ProjectSettings/`, then `project.godot`, then a `*.uproject`. An unrecognized directory uses the engine-neutral setup/Luban path. Selecting an engine explicitly validates its project marker. Godot/Unreal initialization currently does not install engine adapter packages or deploy their Editor integrations.

## Unity prerequisites and installed files

For an initialized project, ordinary initialization requires the matching CLI version. Use [ob update](Update.md) to upgrade the project and CLI together.

Use a Unity 2022.3+ project with `ProjectSettings/ProjectVersion.txt`, .NET SDK 8+, and a closed Editor. Initialization checks the Unity lock file and refuses a currently open project. It does not create gameplay scenes or resource catalogs.

The Unity path:

1. Adds NuGetForUnity `4.5.0` through OpenUPM if neither recognized NuGetForUnity package ID is already selected; preserves an existing selection, unrelated UPM dependencies, and other registry scopes.
2. Resolves the complete .NET Standard 2.1 NuGet dependency closure through `dotnet restore`. Existing non-framework package versions are pinned; incompatible dependency selections fail rather than being silently replaced.
3. Uses a project-local NuGetForUnity.Cli `4.5.0` tool targeting `net8.0` to restore packages. `.obf/` holds ignored resolver/cache/tool files and its own `.gitignore`.
4. Installs an optional integration and creates `Assets/ObFramework/README.md` only if that file is absent.
5. Deploys Editor source and the two Roslyn generators with analyzer `.meta` labels. The CLI package embeds these payloads; separate generator package installation is unnecessary.

NuGet configuration prefers an existing `Packages/nuget-packages/NuGet.config`, otherwise uses `Assets/NuGet.config`. Existing `PackagesConfigDirectoryPath` and `repositoryPath` settings in the Assets layout are respected. Existing package sources and credentials are retained; `--source` updates the `ObFramework` source entry.

## Start from an existing Unity scene

Attach `Assets/Scripts/Unity/GameLauncher.cs` to a GameObject in the startup scene. In Play mode it creates the persistent runtime and loads `Assets/ObFramework/Resources/ObFramework/UIRoot.prefab`. The game owns the generated launcher, prefab, and prefab GUID, so later `ob init` runs preserve those files. The template requires runtime and adapter packages at least as recent as the CLI. See the [Unity startup guide](../../ObFramework.Adapter.Unity/Core/README.md) for plugin registration and game entry.

If your project already has its own or an older `GameApplication` and lacks a launcher, init provides `Assets/Scripts/Unity/GameLauncher.cs.txt`. Adapt the constructor call and plugin configuration to your application, then save it as `GameLauncher.cs` and attach it to the startup scene.

## Test business code with the .NET SDK

Unity initialization creates the business assembly under `Assets/Scripts/Core/` and the .NET
projects under `Tests/`. The core project compiles those same Unity C# files with the
engine-independent ObFramework source generator. Keep engine bindings outside this assembly.

From the Unity project root, run the test command printed by `ob init`.

The generated `Tests/ObFramework.Unity.props` uses the same ObFramework version and
NuGet configuration as Unity. Re-run `ob init`
to refresh these settings after a package upgrade. Existing source files, project files,
and tests are preserved. The first test run restores the project dependencies.
Projects with additional business dependencies should add the corresponding .NET references
to the core project.

## Optional Addressables integration

Unity projects default to the `custom` resource backend. To opt into [Addressables](../../ObFramework.Adapter.Unity/Integrations/Addressables.md), set `modules.installation.resources` in `setup.json` to `addressables` and run `ob init` from the Unity project root.

The Addressables integration deploys source and an asmdef to `Assets/ObFramework/Integrations/Addressables/`, compiled by the game against its own UPM package. An existing registry version in the supported `1.21.x` range is preserved; an absent selection receives `1.21.21`. An incompatible, Git, or local-path selection fails validation.

The integration exposes `ObFramework.Adapter.Unity.Resource.AddressablesAssetAdapter`; projects with explicit assembly references may need `ObFramework.Integrations.Addressables`. Custom adapters implement `IAssetAdapter<TNativeHandle>` and optionally `IRawFileAdapter<TNativeHandle>` instead.

The selected backend is recorded at `modules.installation.resources`. Switching it requires preserving/removing the prior integration and explicitly updating that recorded value before initialization. Existing customized integration files are protected by deployment ownership checks.

## Luban and configuration ownership

Setup adds absent `unity` (Unity only), `luban`, and `diagnostics` sections. Existing values and unknown JSON properties are retained. A pre-existing `root` pointing to another directory is an error; use [configuration commands](Config.md) to update an intentionally relocated project.

Luban installation downloads the pinned official `v5.1.0/Luban.7z` and optional `v5.1.0/Luban.Agent.7z` assets directly from `https://github.com/focus-creative-games/luban/releases/download/` to configured directories. It does not query the GitHub REST API, so API quota exhaustion does not block access to otherwise available release files. A `.obf-luban-version` marker identifies managed installations. A pre-existing unmarked target directory is rejected. Downloads extract into a temporary sibling directory; replacement uses a backup/rollback step. If an asset download fails, init prints a warning, records the skip and completes the other steps. Retry `ob init` when network access is available. `--skip-download` records an intentional skip for doctor diagnostics.

## Verify and recover

Successful init prints static diagnostics and explicitly leaves Unity compilation/runtime unverified. Follow with [doctor](../Unity/Doctor.md), then exercise your bootstrap scene. A required Editor payload conflict fails before that deployment writes its planned files, but prior pipeline steps may already have changed the project. Preserve/move the conflicting file, repair the reported package/feed/configuration issue, and rerun init.

Sources: `ObFramework.Cli/Commands/Init/InitCommand.cs`, `ObFramework.Cli/Commands/Init/InitCliModule.cs`, `ObFramework.Cli/Unity/UnityPackageInstaller.cs`, `ObFramework.Cli/Unity/UnityIntegrationInstaller.cs`, `ObFramework.Cli/Commands/Init/LubanReleaseInstaller.cs`.
