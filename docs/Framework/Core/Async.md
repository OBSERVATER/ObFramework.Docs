# Async work and reusable lifetimes

[API](../api/Core/Async.md) · [Scope source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObExecutionScope.cs)

`ObTask`/`ObTask<T>` wrap `ValueTask`/`ValueTask<T>` and supply custom async method builders. They are awaitable task-like values; they do not introduce a scheduler, force execution onto an engine thread or make business objects thread-safe. Follow ValueTask consumption constraints for the underlying operation.

`ObExecutionScope.Run(Func<CancellationToken,Task>)` registers work and supplies the scope token. Await or otherwise observe the returned task. `StopAsync` closes admission, requests cooperative cancellation, waits for registered work and reports collected business/cancellation-callback failures. All stop callers share a task. Work that ignores cancellation still has to finish; the scope does not collect resource handles for you.

```csharp
using System.Threading.Tasks;
using ObFramework.Core.Async;

public static class ScopeExample
{
    public static async Task RunAsync()
    {
        var scope = new ObExecutionScope();
        Task work = scope.Run(async ct => { await Task.Yield(); ct.ThrowIfCancellationRequested(); });
        try { await work; }
        finally { await scope.StopAsync(); }
    }
}
```

`ObLifetime` addresses a different issue: an async continuation may still reference an object that has been recycled for a new logical owner. Capture its version before awaiting and check `IsCurrent(version)` before applying results. `Invalidate()` ends the current lifetime; `Renew()` requires an inactive lifetime and advances the version. It does not cancel tasks or atomically guard a subsequent engine operation. Run object access on the engine's required context and coordinate destruction there.

Architecture deinitialization is synchronous; use application/plugin scopes for asynchronous ownership rather than assuming every Architecture operation is automatically tracked.
