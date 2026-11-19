---
status: accepted
---

# Scaffold engine-independent tests during Unity initialization

New Unity projects should have a standard way to verify business code from the .NET SDK as soon as they run `ob init`. The CLI owns the initial project structure and the framework dependency settings. Each game owns its test cases and any scenario or replay drivers.

Unity initialization uses the fixed business source directory `Assets/Scripts/Core/` and creates a `Game.Core.asmdef` that sets `noEngineReferences` to `true`. The .NET library at `Tests/Game.Core/Game.Core.csproj` compiles the same C# files as that Unity assembly and includes the engine-neutral ObFramework source generator. It targets `netstandard2.1`. The .NET 8 test project at `Tests/Game.Core.Tests/Game.Core.Tests.csproj` references the library. A minimal business entry point and a startup and shutdown test verify that the test command printed by initialization runs after setup.

The .NET projects restore packages with the configured NuGet sources and the exact ObFramework version selected for Unity. This lets CI exercise the same business code with the .NET SDK. Re-running initialization preserves existing business source, projects, and tests. Its generated dependency settings track later changes to the selected framework version.
