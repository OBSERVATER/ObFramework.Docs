# Documentation boundary verification

Verified on 2026-11-17 against the current working tree. The earlier check record is preserved verbatim in [history](history/verification-initial.original.txt).

| Check | Observed result |
| --- | --- |
| Product API generation | Eight production projects; 282 source-file entries including 16 linked Editor.Core files; 300 public/protected type entries |
| Generated API freshness | Regeneration succeeds and --check matches the current source |
| Public document boundaries | No test-project links, NUnit references, test commands or fixture API inventory in docs/ |
| Links and source inventory | Public guides, examples, skill and internal maintainer links resolve; source hashes match |
| Checker negative probe | Correctly rejects an otherwise valid test-project link in a public guide; probe removed |
| Package-based example | Local ObFramework NuGet package restored and consumed without any runtime ProjectReference; application prints Documentation examples passed. |
| Skill validation | Bundled skill-creator frontmatter/name validator passes |

The example verifies current-value Query/Blackboard behavior, resource-entry sharing and final release, cleared pooled lists, struct pipeline updates and engine-free logical timing. Clock gameplay documentation registers timers without manual Advance/Next calls. Manual advancement is documented separately for a clock owned by an engine-free driver. Production runtime behavior was not changed.

## Local package validation

From Framework/, the package was built with:

```powershell
dotnet pack ObFramework.csproj --no-restore -c Debug -o artifacts/package-validation -p:PackageVersion=0.0.0-docs-validation
```

Before the directory migration, the staged example was restored and run with that exact package version, the local package output and the existing dependency cache as feeds. The installed .NET 8 reference/runtime version was selected explicitly as 8.0.7 and UseAppHost=false. The dependency cache and staging output were kept under ignored Framework/artifacts. These paths/options describe this machine; normal consumers should follow [the example guide](../../../examples/Documentation/README.md) with their chosen published version and configured feeds.

The runtime package build emitted existing nullable-reference and unused-field warnings. No NuGet upload, new remote repository, Git submodule, full NUnit run or native Unity/Godot/UnrealSharp execution was performed for this documentation change. Unit/regression tests remain with the implementation in the main repository.
