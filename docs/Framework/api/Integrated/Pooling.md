# Framework / Integrated/Pooling

[API index](../README.md)

## Integrated/Pooling/DictionaryPool.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/DictionaryPool.cs)

### ObFramework.Integrated.Pooling.DictionaryPoolPolicy<TKey, TValue>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/DictionaryPool.cs#L6).

```csharp
public class DictionaryPoolPolicy<TKey, TValue> : APoolPolicy<Dictionary<TKey, TValue>>
    public override Dictionary<TKey, TValue> Create();
    public override bool OnReturn(Dictionary<TKey, TValue> obj);
```

### ObFramework.Integrated.Pooling.DictionaryPool<TKey, TValue>

Declared at [line 15](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/DictionaryPool.cs#L15).

```csharp
public class DictionaryPool<TKey, TValue> : AObjectPool<Dictionary<TKey, TValue>>
    public static DictionaryPool<TKey, TValue> Pool { get; };
    public DictionaryPool(int capacity, int prewarmCount, IPoolPolicy<Dictionary<TKey, TValue>> policy, int maxRetained = 1024, bool checkIfRepeat = false);
```

## Integrated/Pooling/IObjectPool.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/IObjectPool.cs)

### ObFramework.Integrated.Pooling.IObjectPool<T>

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/IObjectPool.cs#L7).

```csharp
public interface IObjectPool<T> where T : class
    T Get();
    void Return(T obj);
    int InactiveCount { get; }
    IPoolPolicy<T> Policy { get; }
```

### ObFramework.Integrated.Pooling.IPoolPolicy<T>

Declared at [line 16](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/IObjectPool.cs#L16).

```csharp
public interface IPoolPolicy<T> where T : class
    T Create();
    void OnCreate(T obj);
    void OnGet(T obj);
    bool OnReturn(T obj);
    void DestroyObj(T obj);
```

### ObFramework.Integrated.Pooling.APoolPolicy<T>

Declared at [line 24](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/IObjectPool.cs#L24).

```csharp
public abstract class APoolPolicy<T> : IPoolPolicy<T> where T : class
    public abstract T Create();
    public virtual void OnCreate(T obj);
    public virtual void OnGet(T obj);
    public virtual bool OnReturn(T obj);
    public virtual void DestroyObj(T obj);
```

### ObFramework.Integrated.Pooling.AObjectPool<T>

Declared at [line 32](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/IObjectPool.cs#L32).

```csharp
public abstract class AObjectPool<T> : IObjectPool<T>, IDisposable where T : class
    public int InactiveCount { get; };
    public int Capacity { get; };
    public IPoolPolicy<T> Policy { get; };
    protected AObjectPool(int capacity, int prewarmCount, IPoolPolicy<T> policy, int maxRetained = 1024, bool checkIfRepeat = false);
    public virtual T Get();
    public virtual void Return(T obj);
    protected virtual void Grow();
    public void Dispose();
```

## Integrated/Pooling/ListPool.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/ListPool.cs)

### ObFramework.Integrated.Pooling.ListPoolPolicy<T>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/ListPool.cs#L6).

```csharp
public class ListPoolPolicy<T> : APoolPolicy<List<T>>
    public override List<T> Create();
    public override bool OnReturn(List<T> obj);
```

### ObFramework.Integrated.Pooling.ListPool<T>

Declared at [line 15](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/ListPool.cs#L15).

```csharp
public class ListPool<T> : AObjectPool<List<T>>
    public ListPool(int capacity, int prewarmCount, IPoolPolicy<List<T>> policy, int maxRetained = 1024, bool checkIfRepeat = false);
    public static ListPool<T> Pool { get; };
```
