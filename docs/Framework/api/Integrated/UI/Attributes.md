# Framework / Integrated/UI/Attributes

[API index](../../README.md)

## Integrated/UI/Attributes/UIViewAttributes.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/Attributes/UIViewAttributes.cs)

### ObFramework.Integrated.UI.ObservablePropertyAttribute

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/Attributes/UIViewAttributes.cs#L10).

```csharp
[AttributeUsage(AttributeTargets.Field)] public sealed class ObservablePropertyAttribute : Attribute
```

### ObFramework.Integrated.UI.RelayCommandAttribute

Declared at [line 20](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/Attributes/UIViewAttributes.cs#L20).

```csharp
[AttributeUsage(AttributeTargets.Method, Inherited = false)] public sealed class RelayCommandAttribute : Attribute
    public bool AllowConcurrentExecutions { get; set; }
    public bool CancelOnClose { get; set; };
    public string? CanExecute { get; set; }
```

### ObFramework.Integrated.UI.NotifyCanExecuteChangedForAttribute

Declared at [line 37](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/Attributes/UIViewAttributes.cs#L37).

```csharp
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)] public sealed class NotifyCanExecuteChangedForAttribute : Attribute
    public NotifyCanExecuteChangedForAttribute(string commandName);
    public string CommandName { get; }
```

### ObFramework.Integrated.UI.NotifyPropertyChangedForAttribute

Declared at [line 52](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/Attributes/UIViewAttributes.cs#L52).

```csharp
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)] public sealed class NotifyPropertyChangedForAttribute : Attribute
    public NotifyPropertyChangedForAttribute(string propertyName);
    public string PropertyName { get; }
```
