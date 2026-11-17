# Setup snapshots and runtime registrations

The Editor reads development configuration and supplies a `UISetupSection` to `UISetupSnapshotFactory.Create`. The validated snapshot contains normalized paths and resource-key rules; it is not a runtime service and performs no file IO itself.

## Build a snapshot and registry

```csharp
using System;
using ObFramework.Editor.Core;

var setup = UISetupSnapshotFactory.Create(new UISetupSection
{
    Root = "D:/UnityProjects/MyGame",
    ScriptOutputRoot = "Assets/Generated/UI",
    UIAssetRoot = "Assets/ObFramework/UI",
    ResourceKeyFormat = "UI/{view}"
});
if (!setup.Success)
    throw new InvalidOperationException("Invalid UI setup.");

var input = new UIRegistryGenerationInput { Setup = setup.Snapshot };
input.Entries.Add(new UIRegistryEntry
{
    ViewTypeFullName = "Game.UI.ShopView",
    WindowTypeFullName = "Game.UI.ShopWindow"
});
var output = UIRegistrySourceGenerator.Generate(input);
if (!output.Success || output.File == null)
    throw new InvalidOperationException("Invalid UI registry.");
Console.WriteLine(output.File.Content);
```

By default this emits `GeneratedUIRegistry.g.cs` beneath `scriptOutputRoot`, in namespace `Game.Generated.UI`, with `RegisterAll(UnityUIAdapter adapter)`. It contains explicit calls such as:

```csharp
adapter.Register<global::Game.UI.ShopView, global::Game.UI.ShopWindow>("UI/ShopView");
```

The game calls that method during its adapter/plugin setup. Runtime code does not load `setup.json` or scan assemblies. Customize `Namespace`, `ClassName`, `MethodName`, `AdapterTypeName`, or `OutputPath` when integrating with a different host. Supply `ExistingSource` to identify a no-op; registry source is replaceable output, unlike editable View/Window files.

## Paths and keys

The snapshot requires an absolute development root. Script/asset directories must be module-relative and reject absolute paths or `..` segments. Defaults match the CLI: `Assets/ObFramework/Generated/UI`, `Assets/ObFramework/UI`, and `UI/{view}`. Missing/empty directory selections use defaults; invalid supplied selections produce diagnostics.

Resource-key precedence is: per-entry `ResourceKeyOverride`, then setup `ResourceKeyOverrides` by View short type name, then `ResourceKeyFormat` with `{view}` replaced. Resource keys are engine addresses and must not resemble development-root filesystem paths. A format without `{view}` produces a warning because multiple Views could resolve to one key.

The registry generator validates names and detects duplicate View mappings, duplicate resolved keys, and unused setup overrides. Errors suppress source emission. Use the result's `Registrations` to inspect resolved mappings alongside `Diagnostics`, `Success`, and `File`; avoid treating an empty/warning-bearing registry as evidence that project Prefabs exist.

`UISetupOverridesJson.TryRead` reads only the `resourceKeyOverrides` string-to-string map from a raw Unity module section. It exists because Unity's `JsonUtility` does not handle dictionaries. It returns parsing errors rather than throwing for malformed map input; it is not a general JSON document parser.

Sources: [snapshot model/path rules](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshot.cs), [validation](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshotFactory.cs), [registry input/output](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistryGeneration.cs), [registry emitter](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistrySourceGenerator.cs), [override map reader](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupOverridesJson.cs).
