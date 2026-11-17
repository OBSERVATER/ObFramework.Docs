# ObFramework.Adapter.Unity / Extension

[API index](README.md)

## Extension/CoreExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Extension/CoreExtension.cs)

### ObFramework.Adapter.Unity.Extension.CoreExtension

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Extension/CoreExtension.cs#L7).

```csharp
public static class CoreExtension
    public static IDisposable DisposeWhenDestroyed(this IDisposable subscription, MonoBehaviour owner);
```

## Extension/ObExecutionScopeExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Extension/ObExecutionScopeExtension.cs)

### ObFramework.Adapter.Unity.Extension.ObExecutionScopeExtension

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Extension/ObExecutionScopeExtension.cs#L8).

```csharp
public static class ObExecutionScopeExtension
    public static void StopWhenDestroyed(this ObExecutionScope scope, MonoBehaviour owner);
```
