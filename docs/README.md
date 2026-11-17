# ObFramework documentation

ObFramework separates engine-independent application logic from engine lifecycle, native UI and resource providers. Start with a task below; load the relevant project guide before its folder API reference.

| Task | Read first |
| --- | --- |
| Run a minimal C# application | [Getting started](getting-started.md) |
| Find a type, module or source file | [Project map](architecture.md), [machine-readable inventory](api-inventory.json) |
| Compose plugins, models, commands and queries | [Framework](Framework/README.md) |
| Assign layer responsibilities and follow recommended business/UI patterns | [Architecture conventions](Framework/Core/Conventions.md) |
| Work with resources, configuration, saves or UI | [Integrated modules](Framework/README.md#integrated-modules) |
| Bootstrap Unity / Godot / UnrealSharp | [Unity](ObFramework.Adapter.Unity/README.md), [Godot](ObFramework.Adapter.Godot/README.md), [UnrealSharp](ObFramework.Adapter.UnrealSharp/README.md) |
| Initialize or diagnose a Unity project | [CLI](ObFramework.Cli/README.md) |
| Generate UI files from Editor metadata | [Editor core](ObFramework.Editor.Core/README.md) |
| Generate observable properties, commands or save code | [Business generator](ObFramework.Generator/README.md) |
| Generate Unity bindings and interpret diagnostics | [Unity generator](ObFramework.Generator.Unity/README.md) |

User guides and product API pages live here, organized by project, then source folder. Package READMEs and generated-project guides remain at their required distribution paths. API pages describe checked-in declarations; generated business members are explained in generator guides. Use the implementation to resolve disagreements with prose.

For AI agents: select a project from [architecture](architecture.md), read its guide and only the relevant API folder. Follow source links when checking a contract. Use [the documentation skill](../.agents/skills/obframework-docs/SKILL.md) when navigating or maintaining documentation.

[Source context](../SOURCE.md) distinguishes the documented working-tree snapshot from published source and package versions. Source paths refer to the framework checkout; local documentation paths refer to this repository.
