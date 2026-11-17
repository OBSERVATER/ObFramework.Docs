# Query and Blackboard

`Framework/Core/IQuery.cs` · `Framework/Utility/Collection/Blackboard.cs` · [Core API](../api/Core.md)

A Query reads, selects or combines current business data. Reading does not refresh a data source, cache a result, create a snapshot or copy referenced objects. Keep mutations in commands or the data's owner.

```csharp
using ObFramework.Core;
using ObFramework.Utility.Collection;

public sealed class ReadValue<T> : AQuery<T>
{
    private readonly Blackboard _board;
    private readonly BlackboardKey<T> _key;
    public ReadValue(Blackboard board, BlackboardKey<T> key) { _board = board; _key = key; }
    public override T Q() => _board.Get(_key);
}
public static class HealthReader
{
    private static readonly BlackboardKey<int> Health = new BlackboardKey<int>("Health");
    public static int Read(IArchitecture architecture)
    {
        var board = new Blackboard();
        board.Set(Health, 100);
        return architecture.SendQuery(new ReadValue<int>(board, Health));
    }
}
```

Use `board.Get(key)` directly for a single read; a Query is useful for a business selection across sources. Constructor parameters supply filters. `SendQuery<TQuery,TResult>()` creates a fresh parameterless query each time. The instance overload infers `TResult`. `ICanQuery` callers use `this.Query(query)` after Architecture attachment. A directly created `AQuery` throws on `GetArchitecture()` until attached.

Blackboard writes take effect immediately. Keys use object identity; use the same key instance, conventionally `static readonly`. `Name` is a label. `Get` throws for an absent key; `GetVersion` returns zero for one. Changed assignments increment both key and board versions; equal assignments do neither. Reading a reference returns the same object: in-place mutation does not advance a version. Blackboard provides no concurrent read/write guarantee.

UI may read any source. `IUIData` still carries window parameters and reusable child data; Query does not replace View notification or native bindings.
