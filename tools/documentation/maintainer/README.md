# Documentation maintenance

User-facing guides and product API pages live under repository-root `docs/<project>/<source-folder>/`. Keep runnable package consumers under `examples/` and documentation tools under `tools/documentation/`. Framework/ contains runtime source and its package entry point. Maintainer guides, test citations and validation records belong here, outside public docs.

## Commands

From the ObFramework.Docs root (ai/ when mounted), use a .NET 10 SDK and Python 3.10 or newer:

```powershell
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs --check
python tools/documentation/check_docs.py --source-root "D:/src/ObFramework"
dotnet run --project examples/Documentation/DocumentationExamples.csproj -p:ObFrameworkVersion=0.1.0-preview.1
```

Choose an example package version available on your feed. The console example targets .NET 8 and uses PackageReference rather than a local ProjectReference. See [its setup guide](../../../examples/Documentation/README.md) for explicit restore commands.

The generator parses checked-in public/protected declarations from eight production projects, including implicit interface members, enum values, overloads, attributes, constants and generic constraints. It enables Unity editor symbols. It does not evaluate the full MSBuild Compile graph or expand inherited, synthesized or generated business members. Editor.Core's linked source root is handled explicitly. The testing project is excluded from public pages and source inventory.

The JSON inventory records source path, project/module, SHA-256, public types and declaration line numbers. The checker catches missing bundle-local links/headings and changed/removed/new source files when source is available. It also rejects test-project citations in public docs. API `--check` compares expected signatures and generated line locations without rewriting pages. The Python checker does not verify external URL line anchors. Remove obsolete generated pages when deleting source folders. These checks do not prove prose semantics or native engine behavior.

## Updating a public contract

1. Read the affected project's guide, implementation and relevant behavioral tests when the source checkout is available.
2. Update the user guide with prerequisites, complete examples, expected behavior, ownership and failure cases. Describe the contract directly; keep test evidence here rather than linking it from public docs.
3. Update the package-based example when its behavior is affected. Regenerate API pages and run both documentation checks.
4. Run affected examples/tests and record only observed results. Engine bindings need real engine acceptance in addition to managed checks.

For ClockPlugin, engine gameplay registers timers and callbacks while the adapter advances time. Put manual Advance/Next examples under an explicit engine-free heading. This documentation rule does not add runtime enforcement to the public methods.

If delegating, assign a production project first and source folders within it when needed. Give common indexes/tooling and final checks to one owner. Preserve existing user changes and report implementation discrepancies instead of silently changing behavior to fit a guide.

## Internal references

- [Test project guide](Testing.md)
- [Business generator maintenance](ObFramework.Generator.md)
- [Unity generator maintenance](ObFramework.Generator.Unity.md)
- [Validation record](verification-2026-11-17.md)
- [Historical source notes](history/README.md)
- [Documentation skill](../../../.agents/skills/obframework-docs/SKILL.md)

## Submodule ownership and publishing

The former Framework/samples/Documentation project is now [examples/Documentation](../../../examples/Documentation/README.md); Framework/scripts/docs is now [tools/documentation](../README.md). Public docs no longer include the test guide, fixture API pages, authoring workflow or validation logs. The two compiler diagnostic tables remain public because consumers need them to correct source declarations.

The previously centralized runtime/module guides remain under [docs](../../../docs/README.md); package-required READMEs, deployed consumer guides and analyzer release metadata retain their distribution paths. Original exploratory notes are preserved in history.

This repository is published as OBSERVATER/ObFramework.Docs and is mounted at ai/ in the framework repository. Runtime/adapter/CLI/generator source and implementation tests remain in ObFramework. The framework keeps short forwarding AGENTS/skill entries and a documentation landing page; this repository owns the full rules, guides and package examples.

The documentation root and source root are separate. The checker auto-discovers a parent source checkout when mounted; standalone use accepts --source-root or OBFRAMEWORK_SOURCE_ROOT. Without source it validates bundle-local links/policy only and reports the skipped source check. The generator always requires an explicit source root.

Source links use source-context.json's public revision and per-file hashes. A changed/unpublished source file is rendered as a source-checkout path, preventing an invented public permalink. Review source-context.json when adopting a new published source revision; do not label a working-tree snapshot as a released NuGet API contract.

Commit and push documentation changes first. In the framework checkout, update the ai gitlink and commit the forwarding/integration changes without including unrelated implementation edits. A plain clone needs git submodule update --init --recursive ai. Keep implementation regressions with their source projects.

To adopt a new source revision, first verify that it is publicly available, then capture its matching product-source hashes and regenerate the API:

```powershell
python tools/documentation/capture_source_context.py --source-root "D:/src/ObFramework" --published-ref origin/master
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs
```

The context capture reads local Git metadata and does not publish source or validate the remote ref over the network. Review pinned source links in authored guides when changing the documented revision.
