# Utilities and extensions

| Folder | Purpose | API |
| --- | --- | --- |
| Utility/Collection | Blackboard and LRUCache | [Declarations](../api/Utility/Collection.md) |
| Utility/Pipeline | Synchronous composable steps/middleware | [Declarations](../api/Utility/Pipeline.md) |
| Utility/FSM | Registered states and polled transitions | [Declarations](../api/Utility/FSM.md) |
| Utility/Singleton | Generic singleton helpers | [Declarations](../api/Utility/Singleton.md) |
| Extension | Architecture capabilities and collection/math helpers | [Declarations](../api/Extension.md) |
| Extension/Integrated | Plugin clock/logging and pooling conveniences | [Declarations](../api/Extension/Integrated.md) |

## Pipeline

`PipelineBuilder<TContext>.Add` appends an `IPipelineStep<TContext>`; `Use` appends middleware. `Build` snapshots nodes. Steps mutate a context by ref and return Continue or Stop. Middleware receives a continuation whose `Invoke(ref context)` runs the remaining nodes at most once. A struct context requires assigning `Execute(context)`'s result or using `Execute(ref context)`.

```csharp
using ObFramework.Utility.Pipeline;

public sealed class Increment : IPipelineStep<int>
{
    public StepResult Execute(ref int value) { value++; return StepResult.Continue; }
}
// In a method:
// var pipeline = new PipelineBuilder<int>().Add(new Increment()).Build();
// int result = pipeline.Execute(41); // 42
```

The pipeline has no built-in asynchronous execution or cancellation. Stateful step objects require their own concurrency policy if a built pipeline is reused concurrently. [Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/Pipeline/Pipeline.cs).

## State machine

Register `IState` instances with `FSM<T>.AddState`, add polling transition policies for every state that will be updated, then select a starting ID with `StartState` and drive Update/PhysicsUpdate from the frame owner. PollingTransitionPolicy supplies previous/next IDs and a predicate. The current `Update()` directly indexes the current state's transition list: a state with no `AddTransition` entry throws `KeyNotFoundException`, including a nominal terminal state. Account for this limitation before choosing the FSM for a graph with terminal states. A requested state change checks the target state's Condition, exits the old state and enters the new one. Initialize before updating/changing states. The current `AState<TStateId,TOwner>` has a private, empty OnEnter; implement IState directly when custom entry behavior is needed. [Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/IState.cs).

## Collection and shared state

[Blackboard](../Core/Query.md) uses typed identity keys. `LRUCache<TKey,TValue>` owns an eviction callback; inspect each removal/clear overload before using it to own disposable objects. Generic Singleton helpers have process-wide scope for each closed type; explicit application/plugin ownership is easier to shut down deterministically.

Vendored container APIs are indexed under [ThirdParty](../api/ThirdParty/VCotainer.md). Framework business code normally uses Architecture registration rather than constructing a second container.
