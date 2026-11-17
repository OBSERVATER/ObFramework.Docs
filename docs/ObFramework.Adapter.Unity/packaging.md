# Unity package build and installation

The package set is `ObFramework`, `ObFramework.Adapter.Unity` and the `ObFramework.Cli` dotnet tool. Both source generators and the Editor payload are embedded by the CLI build; consumers do not separately install generator packages. Runtime packages include XML API documentation and exclude bundled engine DLLs.

## Build from source

From `Framework/`, with configured NuGet feeds, a suitable .NET SDK and Unity installed, use explicit project commands. The earlier `scripts/pack-unity.ps1` command was removed from the guide because that script is absent from this checkout.

```powershell
$version = "0.1.0-preview.1" # choose the version for all three packages
$packages = Join-Path (Get-Location) "artifacts/nupkg"
dotnet pack ObFramework.csproj -c Release -p:PackageVersion=$version -o $packages
dotnet pack ../ObFramework.Adapter.Unity/ObFramework.Adapter.Unity.csproj -c Release -p:PackageVersion=$version -p:UnityEditorPath="D:/Unity/Editor" -o $packages
dotnet pack ../ObFramework.Cli/ObFramework.Cli.csproj -c Release -p:PackageVersion=$version -o $packages
```

`UnityEditorPath` is the Editor directory. `UnityEditorDataRoot` instead names `Editor/Data/Managed`. Discovery and `UnityEditorsRoot` are defined in `ObFramework.Adapter.Unity/UnityReferences.props`. The CLI project references the generators so its normal build produces the embedded DLLs. Keep the same package version across the set. Inspect resulting nupkg contents before distribution: engine DLLs and a second copy of Editor source content must remain absent.

Optional compile validation of the deployed Editor glue uses `-p:ObfBuildEditorTools=true` and a separate ArtifactsPath; see [Unity source build](README.md#source-build). This mode is non-packable. It does not substitute for real Unity import because the deployed assembly boundary differs from the combined validation compilation.

## Install a local package set

```powershell
dotnet tool install --global ObFramework.Cli --version 0.1.0-preview.1 --add-source "D:/LocalFeed"
ob init --engine unity --root "D:/UnityProjects/MyGame" --source "D:/LocalFeed" --package-version 0.1.0-preview.1
ob doctor --root "D:/UnityProjects/MyGame" --json
```

Close Unity during init. Use the chosen matching version rather than assuming the example version exists in a feed. For optional Addressables, pass `--resources addressables`; see [integration](Integrations/Addressables.md). Backend selection, modified deployment files, restore failures and retries follow the [CLI init contract](../ObFramework.Cli/Commands/Init.md).

Run `ob doctor --root <project> --unity --unity-editor <Unity.exe>` for real import/compilation and verify your application's bootstrap, prefabs and resource catalog in the Editor. Historical checks recorded by earlier notes do not establish that the current worktree or target platform passes. No engine or package-publication acceptance is claimed by the documentation checks.
