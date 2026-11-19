# Getting started

The runtime targets .NET Standard 2.1. The CLI and managed example target .NET 8; the current UnrealSharp project targets .NET 10 and references a local engine installation.

## Run the managed example

From the ObFramework.Docs root (ai/ when mounted), choose a framework version available on your configured NuGet feeds and run:

```powershell
dotnet run --project examples/Documentation/DocumentationExamples.csproj -p:ObFrameworkVersion=0.1.0-preview.1
```

Replace the example version with the package version you use. The [example project](../examples/Documentation/README.md) references ObFramework through NuGet and has no dependency on a local framework project. Its [source](../examples/Documentation/Program.cs) exercises plugin startup/shutdown, Query/Blackboard, resource handles, pooling, a pipeline and a logic clock. Its expected final output is `Documentation examples passed.` It needs no engine. Dependencies must be available from NuGet or the local package cache.

The smallest Architecture has an empty registration phase:

```csharp
using ObFramework.Core;

public sealed class GameArchitecture : AArchitecture<GameArchitecture>
{
    protected override void Init() { }
}

// Execute inside your application's startup/shutdown flow:
IArchitecture architecture = GameArchitecture.Interface;
try
{
    // Register models/systems in Init(), then access them through IArchitecture.
}
finally
{
    architecture.Deinit();
}
```

Use [PluginApplication](Framework/Core/Plugins.md) when the application owns asynchronous work or integrated plugins. Models and systems receive their Architecture during registration. Plain objects receive explicit constructor dependencies; capability extension methods require the corresponding `ICan*` interfaces.

## Initialize a Unity consumer

Close the Unity Editor and run with your chosen package feed:

```powershell
dotnet tool install --global ObFramework.Cli --add-source "D:/LocalFeed"
ob init --engine unity --root "D:/UnityProjects/MyGame" --source "D:/LocalFeed"
ob doctor --root "D:/UnityProjects/MyGame" --json
```

`init` installs framework packages, tooling and a consumer guide. It does not create gameplay scenes or resource catalogs. Build a launcher and register your Views using the [Unity guide](ObFramework.Adapter.Unity/README.md). The optional [Addressables integration](ObFramework.Adapter.Unity/Integrations/Addressables.md) is selected in `setup.json`. For repository packaging, see [Unity packaging](ObFramework.Adapter.Unity/packaging.md).

## Choose a reading path

1. [Architecture conventions](Framework/Core/Conventions.md) explains layer responsibilities, communication rules and System-owned Services.
2. [Core architecture](Framework/Core/Architecture.md) explains registration, command/query boundaries and event subscriptions.
3. [Plugins](Framework/Core/Plugins.md) explains startup dependencies and borrowed adapters.
4. [Async work](Framework/Core/Async.md) explains cancellation, draining and stale object lifetimes.
5. [Resources](Framework/Integrated/Resource.md) and [UI](Framework/Integrated/UI.md) explain handle and View ownership, including the recommended attribute workflow.
6. [Generators](ObFramework.Generator/README.md) explains which source declarations produce business members.
