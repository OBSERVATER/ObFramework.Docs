# Framework / Extension/Integrated

[API index](../README.md)

## Extension/Integrated/ClockExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/ClockExtension.cs)

### ObFramework.Extension.Integrated.ClockExtension

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/ClockExtension.cs#L6).

```csharp
public static class ClockExtension
    public static void Register(this IFrameTickable tickable, ICanGetPlugin self);
    public static void UnRegister(this IFrameTickable tickable, ICanGetPlugin self);
```

## Extension/Integrated/DiagnosticsExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/DiagnosticsExtension.cs)

### ObFramework.Extension.Integrated.DiagnosticsExtension

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/DiagnosticsExtension.cs#L6).

```csharp
public static class DiagnosticsExtension
    public static void I(this ILogger self, string content);
    public static void W(this ILogger self, string content);
    public static void E(this ILogger self, string content);
    public static void D(this ILogger self, string content);
```

## Extension/Integrated/PoolingExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/PoolingExtension.cs)

### ObFramework.Extension.Integrated.PoolingExtension

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/PoolingExtension.cs#L6).

```csharp
public static class PoolingExtension
    public static void ReleaseToPool<T>(this List<T> list);
    public static void ReleaseToPool<TK, TV>(this Dictionary<TK, TV> dict);
```
