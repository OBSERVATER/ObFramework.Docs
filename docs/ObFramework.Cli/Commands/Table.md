# Luban tables

The `table` command runs configured Luban tools through `dotnet` from the development root. It uses the nearest upward-discovered `setup.json` unless `--file <FILE>` is supplied.

```powershell
ob table validate --file "D:/UnityProjects/MyGame/setup.json"
ob table generate --file "D:/UnityProjects/MyGame/setup.json"
ob table generate -c --file "D:/UnityProjects/MyGame/setup.json"
ob table generate -d --file "D:/UnityProjects/MyGame/setup.json"
```

| Form | Behavior |
|---|---|
| `table validate` | Uses an existing configured Luban.Agent if available, otherwise Luban with strict validation and `outputSaver=null`. |
| `table generate` | Generates C# code and JSON data. |
| `table generate -c` | Generates code only. |
| `table generate -d` | Generates data only. |

The actual switches are `-c` and `-d`. Combining them is an error, and they cannot be used with `validate`. Unknown actions fail. The runner requires the configured `Luban.dll`, `luban.conf`, a non-empty target, and paths contained by the root. Initialization installs tools; the game project supplies its own table schema/configuration/input data.

Code generation uses the CLI's embedded `cs-newtonsoft-json` template via a temporary custom-template directory. Data uses Luban's `json` output. Both output roots and the target come from `modules.luban`; inspect or change them using [configuration](Config.md). Validation does not intentionally emit generated code/data.

The CLI forwards stdout/stderr and returns the tool's process exit code. Cancellation kills the child process tree, waits for exit, and reports failure. A missing Agent falls back to validation through Luban; a configured but absent Luban DLL or configuration file is an error.

Sources: [TableCommand](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Table/TableCommand.cs), [LubanRunner](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Table/LubanRunner.cs), [embedded table template](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Resources/LubanTemplates/cs-newtonsoft-json/tables.sbn). This guide describes source behavior; successful table generation still depends on the actual tools and project input files.

## GAS Excel starter

Run `ob init --gas` to add the starter. Edit the three workbooks in the directory printed by init (normally `Configs/GAS`). Keep each sheet's field headers.

| Workbook | Contents |
|---|---|
| `Attribute.xlsx` | Stable attribute key and comment. The starter defines Health, Mana, Attack and Defense. |
| `Effect.xlsx` | Effects defines lifetime and period. Components links a Base or Cost row to its effect ID and attribute key. Multiple rows may share an effect ID. |
| `Ability.xlsx` | The built-in ApplyEffect behavior uses effectId for its target and commitEffectId for its optional owner cost (zero means no cost). |

Validation runs Luban on the workbooks and checks the GAS references. A full export writes CombatAttributes.cs and GasConfigCatalog.cs to the configured code directory and GasEffects.json and GasAbilities.json to the data directory. Use `-c` for code or `-d` for data. Regenerate both when attribute keys change. The GAS schema uses its client target independently of any primary project schema.

Compile the generated C# in the game and publish both JSON files through its resource adapter under `Config/` (or pass a different resource root to GasConfigCatalog). Register these plugins with the host alongside its resource and clock plugins:

```csharp
new ConfigPlugin(new GasConfigCatalog());
new GASPlugin(loadConfig: true, attributes: CombatAttributes.All);
```

Give the caster a Mana attribute and the target a Health attribute. Grant and activate ability 2001. With the supplied values the caster spends 10 Mana and the target loses 20 Health. Ability 2002 applies regeneration for 6 ticks. The generated catalog describes the two GAS runtime configuration types; projects with a custom catalog can use its entries when building their own configuration mapping.

See [Cast the starter attack](../../Framework/Integrated/GAS.md#cast-the-starter-attack) for actor setup, activation and cleanup.
