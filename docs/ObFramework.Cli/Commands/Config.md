# Development configuration

`ob config` edits registered development modules in `setup.json`. It is separate from the runtime `ConfigPlugin`: runtime UI/resource loading uses explicit registrations rather than scanning this file.

## Inspect, edit, validate

```powershell
ob config --list
ob config unity --file "D:/UnityProjects/MyGame/setup.json"
ob config unity --file "D:/UnityProjects/MyGame/setup.json" --set scriptOutputRoot=Assets/Generated/UI
ob config unity --file "D:/UnityProjects/MyGame/setup.json" --set "resourceKeyFormat=UI/{view}"
ob config unity --file "D:/UnityProjects/MyGame/setup.json" --unset scriptOutputRoot
ob config unity --file "D:/UnityProjects/MyGame/setup.json" --validate
```

Without `--file`, discovery walks upward from the working directory to the nearest `setup.json`. `--root` changes the declared absolute development root; it does not choose the configuration file. When creating a configuration, pass both explicitly:

```powershell
ob config unity --create --file "D:/UnityProjects/MyGame/setup.json" --root "D:/UnityProjects/MyGame"
```

| Argument/option | Meaning |
|---|---|
| `[MODULE]` | `unity`, `luban`, or `diagnostics`; omit to show the overview. |
| `--file <FILE>` | Explicit configuration path, resolved relative to the working directory if necessary. |
| `--root <DIR>` | Fully qualified root declaration; required with creation. |
| `--create` | Permit an absent file to be created when a module is selected. An overview does not save a file. |
| `--set <KEY=VALUE>` | Repeatable assignment. Property names are case-insensitive and accept camelCase or CLR casing. |
| `--unset <KEY>` | Repeatable reset to a fresh module instance's default. Resets are applied before assignments. |
| `--validate` | Validate the stored selected module and its paths without saving; does not preview `--set` or `--unset`. |
| `--list` | Show the overview even if a module name was supplied. |

Unknown module names or options are errors. Supported scalar values include strings, booleans, defined enums, and numeric values using invariant culture. String lists use comma-separated values. Dictionary options such as `resourceKeyOverrides` require editing JSON directly.

## Shape and path rules

```json
{
  "root": "D:/UnityProjects/MyGame",
  "modules": {
    "unity": {
      "scriptOutputRoot": "Assets/ObFramework/Generated/UI",
      "uiAssetRoot": "Assets/ObFramework/UI",
      "resourceKeyFormat": "UI/{view}"
    },
    "luban": {
      "lubanDll": "Tools/Luban/Luban.dll",
      "agentDll": "Tools/Luban.Agent/Luban.Agent.dll",
      "confPath": "Configs/luban.conf",
      "codeOutputRoot": "Generated/Config/Code",
      "dataOutputRoot": "Generated/Config/Data",
      "target": "client"
    }
  }
}
```

`root` must be absolute. Members marked `[SetupPath]` are resolved relative to it, normalized to forward slashes, and rejected if they escape the root. An absolute module path is accepted only within that root and is saved relative to it. These checks validate path declarations, not directory/file existence. `agentDll` may be empty to disable the optional Agent. Resource keys such as `UI/{view}` are engine keys, not filesystem paths.

Editing one module preserves unrelated module sections, unknown top-level/module properties, and existing configuration values outside the requested changes. Relocating a project requires explicitly updating its root rather than relying on the current working directory.

## Extend the catalog

`ConfigCliModule.CreateDefaultRegistry()` explicitly registers the three built-in module POCO types. A new module contributes a registration and marks filesystem-path properties with `[SetupPath]`; it does not rely on assembly scanning. `SetupJsonDocument`, `ModuleConfigRegistry`, `SetupConfigRegistration`, `SetupConfigPaths`, and `SetupConfigValues` are the public seams for hosts/tools.

Sources: [ConfigCommand](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ConfigCommand.cs), [catalog](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ConfigCliModule.cs), [Unity defaults](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/Modules/UnitySetupConfig.cs), [Luban defaults](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/Modules/LubanSetupConfig.cs), [path rules](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigPaths.cs), [value conversion](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigValues.cs).
