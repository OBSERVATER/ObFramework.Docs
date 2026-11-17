# Object pooling

[API](../api/Integrated/Pooling.md) · [Pool source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Pooling/IObjectPool.cs)

Use the shared instance through `.Pool`; `Get` and `Return` are instance methods. Earlier notes showing `ListPool<T>.Get()` without `.Pool` were obsolete.

```csharp
using ObFramework.Integrated.Pooling;

var list = ListPool<int>.Pool.Get();
try
{
    list.Add(42);
    // Consume synchronously while this scope owns the list.
}
finally
{
    ListPool<int>.Pool.Return(list);
}
```

DictionaryPool follows the same pattern. List/dictionary policies clear content on return. `ReleaseToPool()` in `ObFramework.Extension.Integrated` is a convenience for returning to the corresponding shared pool. Return exactly once and stop accessing the collection afterward. Shared default pools enable duplicate-return checks; the pool implementation is not synchronized for concurrent use.

For custom objects derive `APoolPolicy<T>` and implement `Create`, optionally `OnCreate`, `OnGet`, `OnReturn` and `DestroyObj`. Derive `AObjectPool<T>` to expose a constructor taking that policy. A false `OnReturn` result or a full retained capacity destroys the returned object. The pool rents backing arrays from `ArrayPool<T>`, grows when required and returns arrays during disposal. `maxRetained` limits retained object count, not the exact size of a rented array.

Dispose application-owned pool instances to destroy inactive objects. Checked-out objects remain the caller's responsibility; a return after disposal destroys that object through the policy. Keep shared default pools alive for their application lifetime.
