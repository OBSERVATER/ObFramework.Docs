# Unity Editor deployment

`ob unity deploy` installs or updates only the Editor/analyzer payload. `ob init` also performs this step, alongside package and development-tool setup. Deployment itself does not run Unity or restore NuGet packages.

```powershell
ob unity deploy "D:/UnityProjects/MyGame" --dry-run
ob unity deploy "D:/UnityProjects/MyGame"
ob unity remove "D:/UnityProjects/MyGame" --dry-run
```

Removal is a separate intentional operation:

```powershell
ob unity remove "D:/UnityProjects/MyGame"
```

## Options and inputs

```text
ob unity <deploy|remove> [PROJECT] [--dry-run]
         [--source <DIR>] [--manifest <FILE>]
```

`PROJECT` defaults to the current directory. The default payload and deployment plan are embedded in the CLI. `--source` selects a local payload directory; its `deploy-manifest.json` overrides the embedded plan if present. `--manifest` explicitly selects the plan. `--dry-run` reports intended writes/deletes without performing them.

The schema-1 plan declares `manifestPath`, `targetRoot`, `autoDeployResources`, optional excludes, explicit file mappings, and `requireComplete`. The shipped plan maps Editor source beneath `Assets/ObFramework/Editor`, with explicit analyzer mappings beneath `Assets/ObFramework/Analyzers`. It excludes Markdown/doc payloads. Source and target paths are relative; targets and the ownership manifest must remain under `Assets/`. `setup.json` is protected, traversal segments and duplicate targets are rejected.

## Ownership and failure behavior

The owned-file manifest at `Assets/ObFramework/obf-deploy.json` stores deployed paths and SHA-256 hashes. Repeating an identical deployment does not rewrite unchanged files. Updates remove stale owned files only when their current bytes still match the previously recorded hash. Removal follows the same check and preserves user-modified content.

The shipped plan uses `requireComplete=true`: a missing required resource, an existing unowned conflicting target, or a modified required target fails preflight before the planned deployment writes files. More permissive custom plans may skip conflicts and return warnings. An absent payload is an error. Files already preserved from a prior deployment can leave warnings/ownership records; inspect the result before treating removal as complete.

After preserving custom changes elsewhere, retry deployment with the intended CLI version. Do not manually edit a deployed file expecting an update to overwrite it. Keep application scripts/prefabs separate from managed deployment paths.

## Public extension points

`UnityEditorDeployer` accepts `IDeployableFileSource` and `DeploymentManifest`. Use `EmbeddedDeployableFileSource` for assembly resources or `DirectoryDeployableFileSource` for a local payload. `Deploy`/`Remove` return `DeploymentResult`, including changes, warnings, summary, and dry-run state. `UnityDeployCliModule.Register(cli)` adds the command through `CliModule.InjectCommand<T>`.

Sources: `ObFramework.Cli/Deployment/UnityEditorDeployer.cs`, `ObFramework.Cli/Deployment/DeploymentManifest.cs`, `ObFramework.Cli/Resources/UnityDeployPlan/deploy-manifest.json`, [file sources](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeployableFileSource.cs).
