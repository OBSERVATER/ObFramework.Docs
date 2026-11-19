# ObFramework.Cli

The `ob` .NET tool initializes development projects, edits `setup.json`, runs Luban, deploys Unity Editor tools, and checks Unity installations. It targets .NET 8. The runtime application does not execute this tool or read its development configuration.

## Start here

Install a version available from your configured package feed. Close the target Unity Editor before initialization.

```powershell
dotnet tool install --global ObFramework.Cli
ob init --root "D:/UnityProjects/MyGame" --engine unity
ob doctor --root "D:/UnityProjects/MyGame" --json
ob doctor --root "D:/UnityProjects/MyGame" --unity --unity-editor "D:/Unity/Editor/Unity.exe"
```

For a local/private package feed, add `--add-source <FEED>` to tool installation and `--source <FEED>` to `ob init`. Use `--package-version <VERSION>` when the runtime packages should differ from the installed CLI version. These commands write to the selected game project; they are examples, not a report of commands run during documentation work.

## Task map

| Task | Guide | Implementation |
|---|---|---|
| Install packages, tooling, and optional resources | [Initialization](Commands/Init.md) | `Commands/Init/`, `Unity/` |
| Upgrade the CLI and a Unity project together | [Updates](Commands/Update.md) | `Commands/Update/` |
| Inspect, edit, or validate development configuration | [Configuration](Commands/Config.md) | `Commands/Config/` |
| Validate tables or generate config code/data | [Luban tables](Commands/Table.md) | `Commands/Table/` |
| Inspect Unity setup and import/compile it | [Doctor](Unity/Doctor.md) | `Unity/UnityDoctor.cs` |
| Deploy/update/remove Editor files | [Deployment](Deployment/README.md) | `Deployment/` |
| Extend or embed the command host | [CLI host](Core/README.md) | `Core/` |
| Generate model/system/command files or record dependencies | [Templates and references](Templates/README.md) | `Templates/`, `Utility/`, `Commands/Reference/` |

The source-derived [API inventory](api/README.md) covers public declarations. Command settings include internal types, so the command-line options in these guides are also part of the user-facing reference.

## Modes and exit codes

`ob <command> ...` executes once and returns that command's status. `ob` without arguments starts the interactive `<3` prompt; `help`, `help init`, and `exit` work there. The interactive tokenizer supports double-quoted arguments and rejects unclosed quotes. It is not a shell and does not implement shell expansion. Interactive mode returns `0` when the session ends; scripts should use one-shot commands to observe failures.

Most commands use `0` for success and `1` for validation, cancellation, or operational failure. Table generation propagates Luban's actual process exit code. Use `ob <command> --help` for the options exposed by the installed tool.

## Evidence and limits

`ObFramework.Cli/Program.cs` registers configuration, initialization, table, and Unity deployment modules around `ObFramework.Cli/Core/CliModule.cs`. A successful static doctor run can still contain a pending Unity compilation check. Use the actual package feed, real Unity import and the application's bootstrap scene to complete setup validation.

See the [documentation home](../README.md) for other projects.
