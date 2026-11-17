# ObFramework.Docs

User guides, source-derived API reference, AI agent instructions, a documentation skill, and package-based examples for ObFramework.

- [Documentation home](docs/README.md)
- [Architecture conventions](docs/Framework/Core/Conventions.md)
- [Agent instructions](AGENTS.md) and [documentation skill](.agents/skills/obframework-docs/SKILL.md)
- [Business glossary](GLOSSARY.md)
- [Managed NuGet example](examples/Documentation/README.md)
- [Maintainer workflow](tools/documentation/maintainer/README.md)

## Use this repository

It works as a standalone documentation checkout. It can also be mounted in the framework source repository:

```powershell
git submodule add https://github.com/OBSERVATER/ObFramework.Docs.git ai
git submodule update --init --recursive ai
```

Existing framework checkouts should use `git submodule update --init --recursive ai`; a fresh clone can use `git clone --recurse-submodules <framework-repository-url>`. The source repository retains small AGENTS/skill entry files that forward to this submodule. The canonical instructions and skill live here.

## Check or update documentation

From this repository's root (`ai/` when mounted):

```powershell
python tools/documentation/check_docs.py
```

This checks the bundle's local links and public-document boundaries. When mounted next to the framework projects it also discovers the source checkout and checks source hashes. Standalone readers can explicitly supply one:

```powershell
python tools/documentation/check_docs.py --source-root "D:/src/ObFramework"
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs --check
```

Python 3.10+ is sufficient for link checks; API generation uses .NET 10. The .NET 8 example consumes a selected ObFramework NuGet version, independently of the source checkout. Build outputs remain in ignored `artifacts/`.

## Source and release context

Read [source context](SOURCE.md) before treating the API inventory as a released package contract. This first snapshot includes local framework implementation changes; publishing this repository does not publish those implementation changes or a NuGet release. Public source links are pinned only where the source matches the recorded public revision. Other source locations are displayed as paths for a matching local checkout.

Keep documentation revisions aligned with the source commit/package version they describe. Commit and push submodule changes first, then update the framework repository's `ai` gitlink to the resulting commit.
