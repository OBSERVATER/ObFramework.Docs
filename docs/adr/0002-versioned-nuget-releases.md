# Publish a matched NuGet package set from a release commit

ObFramework publishes the runtime, CLI and Unity adapter as one versioned set because `ob init` resolves an exact runtime and adapter version from the installed CLI version. A `Release-As` Git trailer declares that shared version and requests publication. GitHub Actions tests and packs the commit at the tip of the push to `master` and creates the `v<version>` tag for that commit. Explicit versions fit the repository's existing commit history and keep the tested source revision tied to the release.

## Request a release

```sh
git commit --allow-empty -m "chore: release" -m "Release-As: 0.1.0-preview.3"
git push origin master
```

Put exactly one `Release-As` trailer at the end of the commit message. Use a three-part version, optionally with a lowercase prerelease suffix such as `-preview.3`. A suffix produces a GitHub prerelease. When squash merging, put the trailer in the final squash commit message. If one push includes several commits, its tip is the commit eligible for publication.

GitHub Actions runs the managed tests for pushes to `master` and pull requests targeting it. For a release it also gets the Unity 2022.3.58f1 references from the official Linux Editor archive, compiles the Unity adapter, and attaches the three packages to a draft Release. The Release becomes public when all three uploads succeed. Rerun a failed workflow to retry the draft. A complete published release for the same commit is left intact. An existing version tag on a different commit blocks publication.

## Maintain API compatibility

Stable versions in a `0.x` minor line preserve public API compatibility. From `1.0.0` onward, the major version defines the stable compatibility line. The first stable package in each line establishes a baseline. Subsequent stable versions compare the runtime and Unity adapter assemblies to the latest lower stable package in the same line before publication. Prereleases and changes between lines require an explicit target when updating a Unity project. Describe deliberate breaking changes and migration steps in the release notes.

## Configure NuGet.org publication

1. Sign in to [NuGet.org Trusted Publishing](https://www.nuget.org/account/trustedpublishing) and add a policy owned by the account or organization that will own the packages. Use repository owner `OBSERVATER`, repository `ObFramework`, workflow file `ci.yml`, and leave Environment blank. Allow publication of new packages and new versions for the `ObFramework*` package pattern.
2. In the GitHub repository, go to **Settings → Secrets and variables → Actions → Secrets**. Create a repository secret named `NUGET_USER` with your NuGet.org profile username. The workflow obtains a short-lived API key using [Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing).

The NuGet job uses the same packages as the GitHub Release. It publishes the runtime and Unity adapter first and waits for both versions to be available in the feed before pushing the CLI. If the NuGet job fails, rerun the failed job. Successful uploads are skipped on retry.

## Install from NuGet.org

After publication, install the CLI and run it from the Unity project root.

```powershell
dotnet tool install --global ObFramework.Cli
ob init
```

For a prerelease, add `--version 0.1.0-preview.3` with the desired version to the install command. The CLI restores the matching framework and Unity adapter automatically.

## Install from GitHub Releases

Download `ObFramework`, `ObFramework.Cli` and `ObFramework.Adapter.Unity` from the same Release into one directory. Pass the directory to both the CLI installation and Unity project initialization. The Release page is the download location. The local directory serves as the NuGet source for the three packages, and NuGet restores their third-party dependencies normally.

```powershell
$feed = 'C:\Downloads\ObFramework'
dotnet tool install --global ObFramework.Cli --version 0.1.0-preview.3 --add-source $feed
# From the Unity project root:
ob init --source $feed
```

Use `dotnet tool update` in place of `dotnet tool install` if the CLI is already installed globally.
