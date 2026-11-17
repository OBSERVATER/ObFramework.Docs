# Documentation tooling

Run these commands from the ObFramework.Docs root (ai/ when mounted) with .NET 10 and Python 3.10 or newer:

```powershell
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs
dotnet run --project tools/documentation/DocsTool.csproj -- "D:/src/ObFramework" docs --check
python tools/documentation/check_docs.py
```

The SDK-only generator has no NuGet dependencies. It creates product API pages and a hashed source inventory for the eight production solution projects. Its source parser includes linked Editor.Core files and deployed/optional Unity branches; it is a declaration inventory rather than a compiled ABI.

The checker validates bundle-local guide, example, skill and maintainer links. It checks source inventory freshness when a source checkout is supplied or discovered. Use --source-root "D:/src/ObFramework" explicitly for a standalone checkout; without source the checker reports a bundle-only check. Public docs and the API inventory must contain no test-project dependencies. Validation records and implementation test references belong in [maintainer material](maintainer/README.md).

Build intermediates and outputs go to ignored repository-root `artifacts/`. These tools are outside the runtime source tree, so regular `Program.cs` files can be used without entering the runtime's recursive compile glob.

When moving to a new publicly available source revision, use `capture_source_context.py --source-root <checkout> --published-ref <public-ref>` before regenerating API pages; see the maintainer workflow for the distinction between local Git refs and public source availability.
