# Framework / Utility/Singleton

[API index](../README.md)

## Utility/Singleton/ISingleton.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Singleton/ISingleton.cs)

### ObFramework.Utility.Singleton.ISingleton

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Singleton/ISingleton.cs#L3).

```csharp
public interface ISingleton
    void OnSingletonInit();
```

## Utility/Singleton/Singleton.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Singleton/Singleton.cs)

### ObFramework.Utility.Singleton.Singleton<T>

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Singleton/Singleton.cs#L5).

```csharp
public class Singleton<T> : ISingleton, IDisposable where T : class, new()
    public static T Inst { get; }
    public virtual void OnSingletonInit();
    public virtual void Dispose();
```
