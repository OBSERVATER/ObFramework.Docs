# Command host

`CliModule` wraps a Spectre.Console.Cli `CommandApp` and owns one session state. `Initialize()` registers built-in help, exit, clear, list, create, reference, and doctor commands. Configuration, initialization, tables, and Unity deployment are explicit modules registered by `Program.Main`.

```csharp
using ObFramework.Cli.Commands.Config;
using ObFramework.Cli.Core;
using ObFramework.Cli.Deployment;

var cli = new CliModule();
cli.Initialize();
ConfigCliModule.Register(cli);
UnityDeployCliModule.Register(cli);
int exitCode = await cli.RunAsync(new[] { "config", "--list" });
```

This is an embedding fragment in a .NET 8 executable referencing the CLI project/assembly. Internal registration modules used by the actual executable are not exposed as a general public host API. Refer to `Program.cs` for the tool's complete composition.

`Initialize()` can run once. `InjectCommand<T>(name)` is allowed after initialization and before the first `RunAsync`; subsequent registration attempts fail. The command type implements Spectre's `ICommand`. There is no command assembly scanning.

`RunAsync(args, cancellationToken)` returns the command status and forwards cancellation. A `help` request is redirected to Spectre's native `--help` path. `ExitRequested` reports the session's exit flag; it does not terminate the process by itself. The console input loop belongs to the executable, not `CliModule`.

Sources: `ObFramework.Cli/Core/CliModule.cs`, [CliSessionState](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Core/CliSessionState.cs), `ObFramework.Cli/Program.cs`.
