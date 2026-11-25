# ObFramework.Adapter.Unity / ViewBinding

[API index](README.md)

## ViewBinding/BindViewAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/BindViewAttribute.cs)

### ObFramework.Adapter.Unity.ViewBinding.BindViewAttribute

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/BindViewAttribute.cs#L5).

```csharp
[AttributeUsage(AttributeTargets.Class, Inherited = false)] public sealed class BindViewAttribute : Attribute
    public BindViewAttribute(Type viewType, Type contractType);
```

## ViewBinding/GeneratedUIBindingRuntime.cs

`ObFramework.Adapter.Unity/ViewBinding/GeneratedUIBindingRuntime.cs` (source checkout)

### ObFramework.Adapter.Unity.ViewBinding.GeneratedUIBindingRuntime

Declared at `ObFramework.Adapter.Unity/ViewBinding/GeneratedUIBindingRuntime.cs:14` (source checkout).

```csharp
public static class GeneratedUIBindingRuntime
    public static void Send(MonoBehaviour context, AUIView view, ACommand command);
    public static void SendAsync(MonoBehaviour context, AUIView view, AAsyncCommand command);
    public static void Report(MonoBehaviour context, Exception error);
```

## ViewBinding/GeneratedViewBinding.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/GeneratedViewBinding.cs)

### ObFramework.Adapter.Unity.ViewBinding.IGeneratedViewBinding

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/GeneratedViewBinding.cs#L8).

```csharp
[EditorBrowsable(EditorBrowsableState.Never)] public interface IGeneratedViewBinding
    Type ControllerType { get; }
    object CreateView(IArchitecture architecture);
```

### ObFramework.Adapter.Unity.ViewBinding.GeneratedViewBinding

Declared at [line 15](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/GeneratedViewBinding.cs#L15).

```csharp
[EditorBrowsable(EditorBrowsableState.Never)] public static class GeneratedViewBinding
    public static void OnEnable(MonoBehaviour controller);
    public static void OnDisable(MonoBehaviour controller);
    public static void OnDestroy(MonoBehaviour controller);
    public static void Refresh(MonoBehaviour controller);
```

## ViewBinding/IGeneratedUIBinding.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/IGeneratedUIBinding.cs)

### ObFramework.Adapter.Unity.ViewBinding.IGeneratedUIBinding

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/IGeneratedUIBinding.cs#L9).

```csharp
public interface IGeneratedUIBinding
    void RefreshBindings();
```

## ViewBinding/ObUnity.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/ObUnity.cs)

### ObFramework.Adapter.Unity.ViewBinding.ObUnity

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/ObUnity.cs#L7).

```csharp
public static class ObUnity
    public static T Instantiate<T>(T original, Transform? parent = null, bool instantiateInWorldSpace = false) where T : Object;
```

## ViewBinding/UIBindCommandAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/UIBindCommandAttribute.cs)

### ObFramework.Adapter.Unity.ViewBinding.UIBindCommandAttribute

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/UIBindCommandAttribute.cs#L10).

```csharp
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)] public sealed class UIBindCommandAttribute : Attribute
    public UIBindCommandAttribute(string commandTypeName);
    public string CommandTypeName { get; }
    public string? CanExecute { get; set; }
```

## ViewBinding/UIBindDataAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/UIBindDataAttribute.cs)

### ObFramework.Adapter.Unity.ViewBinding.UIBindDataAttribute

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/UIBindDataAttribute.cs#L10).

```csharp
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)] public sealed class UIBindDataAttribute : Attribute
    public UIBindDataAttribute(string viewProperty);
    public string ViewProperty { get; }
    public UIBindMode Mode { get; set; };
    public string? Format { get; set; }
```

## ViewBinding/UIBindMode.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/UIBindMode.cs)

### ObFramework.Adapter.Unity.ViewBinding.UIBindMode

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/ViewBinding/UIBindMode.cs#L6).

```csharp
public enum UIBindMode
    OneWay = 0,
    TwoWay = 1,
```
