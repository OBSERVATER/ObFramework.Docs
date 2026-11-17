# Framework / Core/Async

[API index](../README.md)

## Core/Async/ObExecutionScope.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObExecutionScope.cs)

### ObFramework.Core.Async.ObExecutionScope

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObExecutionScope.cs#L9).

```csharp
public class ObExecutionScope
    public Task Run([NotNull] Func<CancellationToken, Task> work);
    public Task StopAsync();
```

## Core/Async/ObLifetime.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObLifetime.cs)

### ObFramework.Core.Async.ObLifetime

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObLifetime.cs#L5).

```csharp
public class ObLifetime
    public int Capture();
    public bool IsCurrent(int version);
    public void Invalidate();
    public void Renew();
```

## Core/Async/ObTask.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObTask.cs)

### ObFramework.Core.Async.ObTask<T>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObTask.cs#L6).

```csharp
[AsyncMethodBuilder(typeof(ObTaskMethodBuilder<>))] public readonly struct ObTask<T>
    public ObTask(Task<T> task);
    public ObTask(ValueTask<T> inner);
    public ValueTaskAwaiter<T> GetAwaiter();
```

### ObFramework.Core.Async.ObTask

Declared at [line 28](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObTask.cs#L28).

```csharp
[AsyncMethodBuilder(typeof(ObTaskMethodBuilder))] public readonly struct ObTask
    public ObTask(Task task);
    public ObTask(ValueTask inner);
    public ValueTaskAwaiter GetAwaiter();
```

## Core/Async/ObTaskMethodBuilder.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObTaskMethodBuilder.cs)

### ObFramework.Core.Async.ObTaskMethodBuilder<T>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObTaskMethodBuilder.cs#L6).

```csharp
public struct ObTaskMethodBuilder<T>
    public static ObTaskMethodBuilder<T> Create();
    public ObTask<T> Task { get; }
    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine;
    public void SetResult(T result);
    public void SetException(Exception exception);
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine;
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine;
    public void SetStateMachine(IAsyncStateMachine stateMachine);
```

### ObFramework.Core.Async.ObTaskMethodBuilder

Declared at [line 68](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Async/ObTaskMethodBuilder.cs#L68).

```csharp
public struct ObTaskMethodBuilder
    public static ObTaskMethodBuilder Create();
    public ObTask Task { get; }
    public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine;
    public void SetResult();
    public void SetException(Exception exception);
    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine;
    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine;
    public void SetStateMachine(IAsyncStateMachine stateMachine);
```
