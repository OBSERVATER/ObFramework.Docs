# Templates and reference graphs

## Create code from templates

```powershell
ob list
ob create Player Game.Models "D:/UnityProjects/MyGame/Assets/Scripts" model
```

The positional arguments are `<name> <namespace> <path> <template>`. The embedded templates are `model`, `system`, and `command`; `ob list` currently lists all embedded resources, including these `.tpl` files and other tooling payloads. The output directory must already exist. Output is named from the requested name plus the title-cased template, such as `PlayerModel.cs`. The template renderer receives the requested name/namespace and a creation timestamp; inspect the produced class declaration before use. An existing destination file is replaced by the current implementation, so choose a new output name or preserve the file first.

Sources: [CreateCommand](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/CreateCommand.cs), [template generation](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Utility/TemplateGenerateHelper.cs), [templates](https://github.com/OBSERVATER/ObFramework/tree/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Templates).

## Record model/system dependencies

The `reference` command manages a developer-maintained graph in `references.json` in the working directory. It does not inspect C# references or enforce runtime dependency injection rules.

```powershell
ob reference add PlayerModel --desc "Current player state"
ob reference add CombatSystem PlayerModel --desc "Resolves combat"
ob reference find CombatSystem
ob reference reverse PlayerModel
ob reference list
ob reference unlink CombatSystem PlayerModel
```

Supported actions are `add`, `link`, `unlink`, `find`, `reverse`, `list`, and `delete`. `add` requires a name and nonempty `--desc` (`-d`) description; optional trailing names are referenced nodes. `link` requires at least one reference; `unlink` takes exactly one. `delete` removes the node and incoming references. `find` and `reverse` report direct/indirect dependencies as JSON. Cycles introduced by adding/linking nodes are rejected and rolled back before saving. Reference names may exist before their nodes are defined.

The helper loads its graph once into static process state. Use one working directory for an interactive session; one-shot invocations reload the file on each process start. There is no concurrent-writer merge protocol.

## Dependency workflow

Inspect the graph before reading or changing a System so both its dependencies and callers are visible. Run from the directory that owns its `references.json`; `reference` has no `--root` option. Use recorded node names, such as `CombatSystem`:

```powershell
ob reference list
ob reference find CombatSystem
ob reference reverse CombatSystem
```

`find` follows outgoing edges: what this node depends on. `reverse` follows incoming edges: what depends on this node. Both include direct and indirect nodes. For a new System, inspect its proposed dependency nodes first; a missing node/graph does not imply an absence of dependencies. Compare the recorded graph with the source when establishing or changing it.

After implementation changes, synchronize the graph using the supported actions:

| Change | Action |
| --- | --- |
| Add a System/Model node | `ob reference add <Node> [Dependencies...] --desc "Purpose"` |
| Add direct dependencies | `ob reference link <Node> <Dependency...>` |
| Remove one direct dependency | `ob reference unlink <Node> <Dependency>` |
| Remove a node after updating its callers | `ob reference delete <Node>` |
| Rename a node | Add the new node, reconnect outgoing/incoming edges, then delete the old node |

`add` does not replace an existing node. `delete` also removes incoming references, so inspect reverse dependencies before using it and preserve edges during a rename. Run `find` and `reverse` again on surviving affected nodes after updates. With a pure read, leave the graph unchanged unless correcting verified metadata. Check exit codes and review the resulting file alongside the code change.

There is no automatic source scan or refresh/update action: re-running `find` reads the recorded graph and does not discover new dependencies. Use fresh one-shot invocations when another process has updated the file, and keep writers coordinated.

Sources: [ReferenceCommand](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Reference/ReferenceCommand.cs), [ReferenceNodeHelper](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Utility/ReferenceNodeHelper.cs).
