# Managed ObFramework example

This .NET 8 console application consumes the ObFramework NuGet package. It has no ProjectReference to the framework source and can be copied to an independent examples repository.

From the ObFramework.Docs root (ai/ when mounted), choose a version available on your package feed:

```powershell
dotnet run --project examples/Documentation/DocumentationExamples.csproj -p:ObFrameworkVersion=0.1.0-preview.1
```

The version above is an example selection, not an assertion that it is available on a public feed. For a local/private feed, restore explicitly and reuse the same version when running:

```powershell
dotnet restore examples/Documentation/DocumentationExamples.csproj --source "D:/LocalFeed" --source "https://api.nuget.org/v3/index.json" -p:ObFrameworkVersion=0.1.0-preview.1
dotnet run --project examples/Documentation/DocumentationExamples.csproj --no-restore -p:ObFrameworkVersion=0.1.0-preview.1
```

[Program.cs](Program.cs) demonstrates current-value Query/Blackboard reads, shared resource entries and final-handle release, clear-on-return pooling, struct pipelines, and engine-free clock advancement. A successful run prints `Documentation examples passed.` The clock in this console example has no engine driver; gameplay in an engine uses its adapter-owned frame loop.

Use [the user guides](../../docs/README.md) for the full API contracts. Intermediate/output files go to ignored repository-root `artifacts/` and are not part of the runtime project. If copying the example to another repository, update the guide link and select a package version matching those guides.
