# ObFramework.Generator.Unity

Unity-specific incremental generator for the connection between a native controller and its business View. It emits View construction, reserved Unity callbacks, data subscriptions and command handlers from source attributes. It targets .NET Standard 2.0 and builds against Roslyn 4.3.1, the project's Unity 2022.3 compiler baseline.

| Task | Read |
|---|---|
| Construct a business object from a Unity controller | [View binding](ViewBinding/view-binding.md) |
| Bind Text, InputField, Toggle, Slider or Button fields | [Control bindings](UIBinding/control-bindings.md) |
| Interpret a compiler diagnostic | [Compiler diagnostics](diagnostics.md) |
| Find public generator signatures | [Generated API](api/README.md) |
| Generate observable properties and relay command types | [Engine-independent generator](../ObFramework.Generator/README.md) |

The [Unity adapter](../ObFramework.Adapter.Unity/README.md) provides the attributes and runtime lifecycle bridge. The [CLI](../ObFramework.Cli/README.md) deploys both analyzer assemblies and their Unity metadata during `ob init`. Application authors normally edit attributed source rather than invoking `ViewBindingGenerator` themselves.

## Responsibility split

`ObFramework.Generator` emits `Title`, `TitleChanged` and nested `SubmitCommand` from a business View's source. `ObFramework.Generator.Unity` reads that same source convention to emit native control bindings. The generators run independently in the same compiler pass; neither assumes that it can read the other's newly generated output.

The shared naming implementation is [CdeSourceRules.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/CDE/CdeSourceRules.cs), compiled separately into both assemblies. Binding generation uses source declarations in the current compilation. Merely referencing a compiled business View assembly does not provide the source convention required for control bindings.

## Starting a Unity project

With the Editor closed:

```powershell
ob init --engine unity --root "D:/UnityProjects/MyGame"
ob doctor --root "D:/UnityProjects/MyGame" --json
```

Use a configured package feed or the CLI's `--source` option as described in the CLI guide. In Unity, check that both deployed analyzers have their RoslynAnalyzer metadata and that no stale second copy is imported. Create the business View and controller using the [control binding example](UIBinding/control-bindings.md), then assign serialized control references on the Prefab. Generation does not create a scene, Prefab hierarchy or resource catalog.

## Implementation map

| File | Role |
|---|---|
| [ViewBindingGenerator.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/ViewBindingGenerator.cs) | Incremental entry point; controller, contract and constructor validation; native callbacks |
| [UIBindingSupport.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/UIBindingSupport.cs) | Source convention model, control/mode/conversion validation and binding plan |
| [UIBindingEmitter.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/UIBindingEmitter.cs) | Strongly typed subscriptions, data refresh and command dispatch |
| [GenerationContext.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator.Unity/GenerationContext.cs) | Internal generation context support |
