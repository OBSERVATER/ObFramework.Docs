# Documentation and source roots

This is the canonical ObFramework AI instruction file. Its directory is the ObFramework.Docs root; a framework checkout mounts it at ai/. Paths to docs/, tools/, examples/ and .agents/ below are relative to this documentation root, including when a source-repository entry forwards here.

For framework implementation work, use an explicitly supplied source checkout, OBFRAMEWORK_SOURCE_ROOT, or the parent directory when it contains Framework/ObFramework.csproj. This documentation repository itself does not contain the runtime/adapter projects. Run source build and reference commands in that source/project context; run documentation tooling and package examples here.

# Documentation

Maintained documentation lives in `docs/`, grouped by project and then source folder. Read [the project map](docs/architecture.md) when navigating the solution and the affected project guide before changing a public contract.

When explaining framework APIs or updating their guides/examples, use [obframework-docs](.agents/skills/obframework-docs/SKILL.md). Synchronize affected guides and the generated API inventory using the [maintainer workflow](tools/documentation/maintainer/README.md). User guides in `docs/` contain product APIs and usage without test references. Test evidence and maintenance records belong in `tools/documentation/maintainer/`.

Keep examples in this repository's `examples/` and documentation tools in `tools/documentation/`; the `Framework/` runtime project remains in the source checkout. Examples consume a chosen NuGet version. Package-required READMEs and deployed consumer guides remain at their distribution paths. In engine gameplay, let the adapter drive ClockPlugin; reserve manual `Advance`/`Next` calls for engine-free drivers.

Write maintained documentation in English. If delegating work, assign ownership by project first and by folder within that project.

When designing or changing Controllers, Systems, Models, Services or UI bindings, read [architecture conventions](docs/Framework/Core/Conventions.md). Use the current interface capabilities and documented lifetime boundaries; prefer generated UI attributes for repetitive property/command/control wiring.

Controllers listen through `ICanRegisterEvent` and must route Event publication through a business Command or its called System/Model. Keep `ICanSendEvent` off Controller types.

# System dependency workflow

Before inspecting, adding, changing, renaming or deleting a System, run the CLI `reference` command to inspect its dependencies and dependents. Include affected Models and callers when assessing a change. Locate the relevant `references.json` in the framework/application source context and run from its owning directory; the command uses the current working directory and has no `--root` option. Do not create a documentation-local graph merely because the current directory is ObFramework.Docs.

```powershell
ob reference list
ob reference find InventorySystem
ob reference reverse InventorySystem
```

Use the graph's actual node name in place of `InventorySystem`. For a new System, inspect the proposed dependency nodes and their reverse dependencies first. A missing graph/node is missing metadata, not proof of no dependencies: verify the implementation and record the gap.

After changing code, synchronize nodes and direct edges with `ob reference add <Node> [Dependencies...] --desc "..."`, `link <Node> <Dependencies...>`, `unlink <Node> <Dependency>` or `delete <Node>`, then run `find` and `reverse` again on surviving affected nodes. A rename requires adding the new node and reconnecting its dependencies/dependents before deleting the old node. For deletion, inspect reverse dependencies first and update affected callers. Pure inspection does not require a graph write when no metadata has changed.

The graph is maintained manually; there is no automatic C# scan, refresh or update action. Keep `references.json` aligned with the code as part of the same change, preserve unrelated graph entries and check command exit codes. If `ob` is unavailable, use a working repository CLI build with the same graph working directory; report any remaining tooling or metadata gap. See [the reference guide](docs/ObFramework.Cli/Templates/README.md#dependency-workflow) for supported actions and file behavior.
