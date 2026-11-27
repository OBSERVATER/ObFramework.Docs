# Framework / Integrated/ViewBinding

[API index](../README.md)

## Integrated/ViewBinding/BindViewAttribute.cs

`Framework/Integrated/ViewBinding/BindViewAttribute.cs` (source checkout)

### ObFramework.Integrated.ViewBinding.BindViewAttribute

Declared at `Framework/Integrated/ViewBinding/BindViewAttribute.cs:5` (source checkout).

```csharp
[AttributeUsage(AttributeTargets.Class, Inherited = false)] public sealed class BindViewAttribute : Attribute
    public BindViewAttribute(Type viewType, Type contractType);
```

## Integrated/ViewBinding/GeneratedViewBinding.cs

`Framework/Integrated/ViewBinding/GeneratedViewBinding.cs` (source checkout)

### ObFramework.Integrated.ViewBinding.IGeneratedViewBinding<out TView>

Declared at `Framework/Integrated/ViewBinding/GeneratedViewBinding.cs:7` (source checkout).

```csharp
[EditorBrowsable(EditorBrowsableState.Never)] public interface IGeneratedViewBinding<out TView> where TView : class, IController
    Type ControllerType { get; }
    TView CreateView(IArchitecture architecture);
```
