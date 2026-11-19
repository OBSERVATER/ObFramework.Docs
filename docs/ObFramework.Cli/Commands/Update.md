# Update a Unity project

Run this command from an initialized Unity project with ObFramework.Cli installed as a global .NET tool. The update checks NuGet.org for a version shared by the runtime, the Unity adapter, and the CLI.

```powershell
ob update --check
ob update
```

The first command reports the installed CLI and project versions and the available updates. The second schedules the newest compatible update. Close the target Unity Editor before scheduling an update. Use `--root <DIR>` to select another project. The updater uses the version recorded in `ProjectSettings/ProjectVersion.txt` and looks for it in Unity Hub and its install directories. For an installation outside those locations, set `UNITY_EDITOR_PATH` to the Editor executable.

Interactive use checks for updates in the background at most once a day. The cache is shared by the user's Unity projects. An explicit check requests fresh information.

## Compatibility

For stable `0.x` releases, the default update stays within the same minor version. For example, `0.1.4` can update to `0.1.5`. From `1.0.0` onward, the default update stays within the same major version. Select an exact version for a prerelease or a move between compatibility lines. Read the release notes and prepare API migrations on a project branch before scheduling such an update.

```powershell
ob update --version 0.2.0
```

The selected version must be at least as recent as both the CLI and the current project. A newer global CLI does not implicitly change another project's version. Use the matching CLI when running ordinary `ob init` on an older project.

## Completion and recovery

The command prints the path to an update log in `.obf/updates/` and exits so the global tool files can be replaced. In interactive mode this also closes the prompt. The worker downloads a staged CLI, restores the selected framework and adapter for this project, and uses the staged CLI to run Unity import and compilation. A passing Unity check allows the global CLI to advance to the same version.

The log records completion or the failing stage. A failed restore or compilation can leave project changes for diagnosis. Fix the reported problem and rerun with the same `--version`, or restore the prior project state using the project's version control. Unity compilation verifies the project's API usage. Run the project's own tests for changes in behavior.
