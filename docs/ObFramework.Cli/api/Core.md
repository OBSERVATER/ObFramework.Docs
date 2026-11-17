# ObFramework.Cli / Core

[API index](README.md)

## Core/CliModule.cs

`ObFramework.Cli/Core/CliModule.cs` (source checkout)

### ObFramework.Cli.Core.CliModule

Declared at `ObFramework.Cli/Core/CliModule.cs:7` (source checkout).

```csharp
public sealed class CliModule
    public bool ExitRequested { get; };
    public void Initialize();
    public void InjectCommand<T>(string name) where T : class, ICommand;
    public async Task<int> RunAsync(string[] args, CancellationToken cancellationToken = default);
```
