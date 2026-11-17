# Framework / Utility/Collection

[API index](../README.md)

## Utility/Collection/Blackboard.cs

`Framework/Utility/Collection/Blackboard.cs` (source checkout)

### ObFramework.Utility.Collection.BlackboardKey

Declared at `Framework/Utility/Collection/Blackboard.cs:5` (source checkout).

```csharp
public abstract class BlackboardKey
    public string Name { get; }
    protected BlackboardKey(string name);
```

### ObFramework.Utility.Collection.BlackboardKey<T>

Declared at `Framework/Utility/Collection/Blackboard.cs:11` (source checkout).

```csharp
public sealed class BlackboardKey<T> : BlackboardKey
    public BlackboardKey(string name);
```

### ObFramework.Utility.Collection.Blackboard

Declared at `Framework/Utility/Collection/Blackboard.cs:16` (source checkout).

```csharp
public sealed class Blackboard
    public int Version { get; private set; }
    public T Get<T>(BlackboardKey<T> key);
    public int GetVersion<T>(BlackboardKey<T> key);
    public void Set<T>(BlackboardKey<T> key, T value);
```

## Utility/Collection/LRUCache.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Collection/LRUCache.cs)

### ObFramework.Utility.Collection.LRUCache<TKey, TValue>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Collection/LRUCache.cs#L6).

```csharp
public sealed class LRUCache<TKey, TValue> where TKey : notnull
    public int Count { get; };
    public int Capacity { get; };
    public LRUCache(int capacity, Action<TKey, TValue>? onRemoved = null);
    public bool TryGet(TKey key, out TValue value);
    public bool TryTake(TKey key, out TValue val);
    public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory);
    public void Add(TKey key, TValue value);
    public bool Remove(TKey key, bool trigger = true);
    public void Clear();
```
