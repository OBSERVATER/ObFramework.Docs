# ObFramework.Cli / Commands/Config/Modules

[API index](../../README.md)

## Commands/Config/Modules/LubanSetupConfig.cs

`ObFramework.Cli/Commands/Config/Modules/LubanSetupConfig.cs` (source checkout)

### ObFramework.Cli.Commands.Config.Modules.LubanSetupConfig

Declared at `ObFramework.Cli/Commands/Config/Modules/LubanSetupConfig.cs:3` (source checkout).

```csharp
public sealed class LubanSetupConfig
    [SetupPath] public string LubanDll { get; set; };
    [SetupPath(AllowEmpty = true)] public string AgentDll { get; set; };
    [SetupPath] public string ConfPath { get; set; };
    [SetupPath] public string CodeOutputRoot { get; set; };
    [SetupPath] public string DataOutputRoot { get; set; };
    [SetupPath(AllowEmpty = true)] public string GasConfPath { get; set; };
    public string Target { get; set; };
```

## Commands/Config/Modules/UnitySetupConfig.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/Modules/UnitySetupConfig.cs)

### ObFramework.Cli.Commands.Config.Modules.UnitySetupConfig

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/Modules/UnitySetupConfig.cs#L13).

```csharp
public sealed class UnitySetupConfig
    [SetupPath(Description = "Generated View/VC and UI registry source directory")] public string ScriptOutputRoot { get; set; };
    [SetupPath(Description = "UI Prefab directory")] public string UiAssetRoot { get; set; };
    public string ResourceKeyFormat { get; set; };
    public Dictionary<string, string>? ResourceKeyOverrides { get; set; }
```
