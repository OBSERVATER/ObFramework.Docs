---
name: obframework-docs
description: Navigate and maintain ObFramework's centralized user guides, source-backed API reference and package-based examples. Use when explaining framework APIs, adding a module guide or synchronizing documentation after a public contract change.
---

# ObFramework documentation

Resolve the documentation root as three directories above this skill folder. It contains docs/, tools/ and examples/ but not the framework implementation. When mounted as a submodule it is the source checkout's ai/ directory. Locate source separately from an explicit path, OBFRAMEWORK_SOURCE_ROOT, or a parent containing Framework/ObFramework.csproj.

User guides live under docs/<project>/<source-folder>/. The runtime's source project key is Framework. Examples consume NuGet packages and run independently of a source checkout. [Source context](../../../SOURCE.md) explains pinned public source links and working-tree source paths.

## Find a contract

Read [the project map](../../../docs/architecture.md) and select the owning project. Read that project's README, then the relevant source-folder guide/API page. Search `docs/api-inventory.json` by type name or source path to find declarations without loading unrelated projects.

When assigning responsibilities to Controllers, Systems, Models or Services, read [architecture conventions](../../../docs/Framework/Core/Conventions.md). For UI state/input wiring, read the [attribute workflow](../../../docs/Framework/Integrated/UI.md#prefer-attributes-for-ui-state-and-input). Keep the distinction between current interface capabilities, recommended business boundaries and actual lifetime ownership.

When inspecting or changing a System, follow the required `reference` workflow in [AGENTS.md](../../../AGENTS.md). The [CLI guide](../../../docs/ObFramework.Cli/Templates/README.md#dependency-workflow) describes manual node/edge updates; this command does not infer dependencies from source. Controllers can listen to Events, but publish business Events through Commands.

Use the linked implementation to settle ownership, cancellation, lifecycle or generated-code details when a source checkout is available. API pages inventory checked-in declarations rather than a compiled ABI. Editor.Core compiles files physically owned by the Unity Adapter's `EditorDeploy/Core`; source inventory includes deployed/optional branches. Generated View/command members belong to the generator guides, rather than being missing runtime declarations.

## Maintain a contract

Keep English user guides and API pages in `docs/<project>/<source-folder>/`. Use the [maintainer workflow](../../../tools/documentation/maintainer/README.md) for layout, commands and migration policy. User-facing `docs/` contains product APIs and application guidance. Keep test projects, fixture APIs, test citations and validation logs in maintainer material outside `docs/`. Maintenance can still use behavioral tests as evidence without requiring readers to navigate them.

Keep examples and build helpers outside `Framework/`; examples consume a chosen NuGet version instead of a local runtime ProjectReference. In engine gameplay, ClockPlugin is advanced by the adapter; manual `Advance`/`Next` examples belong in explicitly engine-free contexts. Leave package-required/deployed READMEs at their distribution paths and keep one maintained explanation per contract.

Document setup, a minimal example, expected result, failure behavior and ownership where relevant. Mark app-specific types and engine prerequisites in excerpts. Preserve the distinction between source-checked snippets, executed managed examples and real engine acceptance.

For a changed public contract, update its guide and relevant runnable example, then regenerate API pages and source inventory. From the ObFramework.Docs root (ai/ when mounted), with an available source checkout:

```powershell
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs --check
python tools/documentation/check_docs.py --source-root "D:/src/ObFramework"
dotnet run --project examples/Documentation/DocumentationExamples.csproj -p:ObFrameworkVersion=0.1.0-preview.1
```

With no source checkout, run the checker without --source-root for bundle-only validation and report that source verification was skipped. The mounted layout auto-discovers the parent source checkout; replace the example source path when passing it explicitly. Select an example package version available on the intended feed. The API helper requires .NET 10; the console example targets .NET 8. Consult the internal [test guide](../../../tools/documentation/maintainer/Testing.md) for implementation validation. Report commands actually run and distinguish managed results from native engine acceptance.

When delegating, assign tasks by project first; within a project split by source folder if needed. Give each task exclusive document paths. One integrating owner updates common indexes and runs final link checks. Preserve existing worktree edits and resolve discrepancies in documentation without silently changing production behavior.

Done means affected guides and indexes are reachable, declarations/inventory match current source, public docs have no test dependencies, local checks pass, and validation claims describe observed evidence. Keep source-context metadata and documentation/package versions aligned. The framework repository keeps only forwarding AGENTS/skill entries; update the submodule commit after publishing changes here.
