# Framework / ThirdParty/VCotainer/Annotations

[API index](../../README.md)

## ThirdParty/VCotainer/Annotations/InjectAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Annotations/InjectAttribute.cs)

### VContainer.PreserveAttribute

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Annotations/InjectAttribute.cs#L5).

```csharp
public class PreserveAttribute : Attribute
```

### VContainer.InjectAttribute

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Annotations/InjectAttribute.cs#L9).

```csharp
[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)] public class InjectAttribute : PreserveAttribute
```

### VContainer.InjectIgnoreAttribute

Declared at [line 14](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Annotations/InjectAttribute.cs#L14).

```csharp
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)] public class InjectIgnoreAttribute : Attribute
```

## ThirdParty/VCotainer/Annotations/KeyAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Annotations/KeyAttribute.cs)

### VContainer.KeyAttribute

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Annotations/KeyAttribute.cs#L5).

```csharp
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)] public class KeyAttribute : PreserveAttribute
    public object Key { get; }
    public KeyAttribute(object key = null);
```
