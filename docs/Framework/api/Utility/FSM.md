# Framework / Utility/FSM

[API index](../README.md)

## Utility/FSM/FSM.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/FSM.cs)

### ObFramework.Utility.FSM.FSM<T>

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/FSM.cs#L7).

```csharp
public class FSM<T>
    public T CurStateId { get; };
    public IState CurState { get; };
    public T PreviousStateId { get; };
    public event Action<T, T> OnStateChange;
    public void AddState(T stateId, IState state);
    public void AddTransition([NotNull] PollingTransitionPolicy<T> policy);
    public void StartState(T state);
    public bool ChangeState(T state);
    public void Update(float dt);
    public void PhysicsUpdate(float dt);
    public void Clear();
```

## Utility/FSM/IState.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/IState.cs)

### ObFramework.Utility.FSM.IState

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/IState.cs#L3).

```csharp
public interface IState
    bool Condition();
    void Enter();
    void Exit();
    void Update(float dt);
    void PhysicsUpdate(float dt);
```

### ObFramework.Utility.FSM.AState<TStateId, TOwner>

Declared at [line 15](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/IState.cs#L15).

```csharp
public abstract class AState<TStateId, TOwner> : IState
    protected TOwner Owner;
    protected FSM<TStateId> FSM;
    protected AState(TOwner owner, FSM<TStateId> fSM);
    protected virtual void OnExit();
    protected virtual bool OnCondition();
    protected virtual void OnUpdate(float dt);
    protected virtual void OnPhysicsUpdate(float dt);
```

## Utility/FSM/PollingTransitionPolicy.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/PollingTransitionPolicy.cs)

### ObFramework.Utility.FSM.PollingTransitionPolicy<T>

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Utility/FSM/PollingTransitionPolicy.cs#L5).

```csharp
public readonly struct PollingTransitionPolicy<T>
    public T PreviousId { get; }
    public T NextId { get; }
    public Func<bool> TransitionCond { get; }
    public PollingTransitionPolicy(T previousId, T nextId, Func<bool> transitionCond);
```
