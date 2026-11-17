# ObFramework.Generator

Compiler-time generation for engine-independent business Views and save snapshots. The generated code runs against `ObFramework`; the generator assembly itself belongs in the compiler's analyzer input.

| Source folder | Responsibility | Guide |
|---|---|---|
| `Integrated/CDE` | Observable properties and nested UI command types | [Observable state](Integrated/CDE/observable-state.md), [relay commands](Integrated/CDE/relay-commands.md) |
| `Integrated/Save` | `ICanSave` implementation and MessagePack snapshot DTO | [Save generation](Integrated/Save/save-generation.md) |
| Project root | Rejected source declarations and corrections | [Compiler diagnostics](diagnostics.md) |

Use the [generated API reference](api/README.md) for generator entry-point signatures. Application authors normally use the attributes in [Framework/Integrated/UI](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/Attributes/UIViewAttributes.cs) and [Framework/Integrated/Save](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/CanSaveAttribute.cs), rather than calling generator APIs.

## Add the generator

For a source checkout, add the runtime and analyzer references to the consuming project. Paths below assume a project beside `Framework/` and `ObFramework.Generator/`:

```xml
<ItemGroup>
  <ProjectReference Include="../Framework/ObFramework.csproj" />
  <ProjectReference Include="../ObFramework.Generator/ObFramework.Generator.csproj"
                    OutputItemType="Analyzer"
                    ReferenceOutputAssembly="false" />
</ItemGroup>
```

For a Unity project initialized by `ob init`, the CLI deploys this generator and `ObFramework.Generator.Unity` with their analyzer metadata. Follow the [CLI guide](../ObFramework.Cli/README.md) instead of adding a second copy. Business View generation and Unity control binding are separate responsibilities; see [Unity generator documentation](../ObFramework.Generator.Unity/README.md).

## Minimal business View

This is a complete source type. It requires the runtime and analyzer references above.

```csharp
using ObFramework.Integrated.UI;

namespace Example
{
    public sealed partial class CounterView : AUIView
    {
        [ObservableProperty] private int _count;

        [RelayCommand]
        private void Increment() => Count++;
    }
}
```

The generator adds `Count`, `CountChanged` and the nested `CounterView.IncrementCommand` type. For an engine-independent exercise:

```csharp
var view = new Example.CounterView();
new Example.CounterView.IncrementCommand(view).Execute();
System.Console.WriteLine(view.Count); // 1
view.Dispose();
```

In a running application, let the UI manager and engine binding layer own the View lifecycle. Constructing a View directly, as above, does not initialize an Architecture or open a native window.

Generated partial classes are compiler output. Edit their attributed source and rebuild the consuming project. Use the [diagnostic guide](diagnostics.md) to correct rejected declarations.
