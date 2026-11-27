# Framework / Integrated/Clock

[API index](../README.md)

## Integrated/Clock/ClockPlugin.cs

`Framework/Integrated/Clock/ClockPlugin.cs` (source checkout)

### ObFramework.Integrated.Clock.ClockPlugin

Declared at `Framework/Integrated/Clock/ClockPlugin.cs:9` (source checkout).

```csharp
public class ClockPlugin : IPlugin
    public int Tick { get; private set; }
    public double Time { get; };
    public float TickInterval { get; }
    public ClockPlugin(float interval);
    public Task StopAsync(IPluginContext plugins);
    public void RegisterTickable(IFrameTickable tickable);
    public void UnRegisterTickable(IFrameTickable tickable);
    public void Next(float dt);
    public void Advance(int tick);
    public void Repeat(int tick, Action action);
    public void Schedule(int tick, Action action);
```

## Integrated/Clock/FrameDispatcher.cs

`Framework/Integrated/Clock/FrameDispatcher.cs` (source checkout)

### ObFramework.Integrated.Clock.FrameDispatcher

Declared at `Framework/Integrated/Clock/FrameDispatcher.cs:6` (source checkout).

```csharp
public class FrameDispatcher
    public void Register(IFrameTickable instance);
    public void UnRegister(IFrameTickable instance);
    public void Dispatch(Func<bool>? canContinue = null);
    public void DispatchFixed(Func<bool>? canContinue = null);
    public void DispatchLate(Func<bool>? canContinue = null);
    public void DispatchLogic(Func<bool>? canContinue = null);
```

## Integrated/Clock/IApplicationFrameLoopDriver.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/IApplicationFrameLoopDriver.cs)

### ObFramework.Integrated.Clock.IApplicationFrameLoopDriver

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/IApplicationFrameLoopDriver.cs#L7).

```csharp
public interface IApplicationFrameLoopDriver : IFrameLoopDriver
    void BeginStartup(CancellationToken stopping);
    void Prepare(IArchitecture architecture);
    Task ReleaseAsync();
```

## Integrated/Clock/IFrameLoopDriver.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/IFrameLoopDriver.cs)

### ObFramework.Integrated.Clock.IFrameLoopDriver

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/IFrameLoopDriver.cs#L3).

```csharp
public interface IFrameLoopDriver
    void Bind(ClockPlugin clock);
    void Unbind();
```

## Integrated/Clock/ITickable.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/ITickable.cs)

### ObFramework.Integrated.Clock.IFrameTickable

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/ITickable.cs#L3).

```csharp
public interface IFrameTickable
```

### ObFramework.Integrated.Clock.ITickable

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/ITickable.cs#L5).

```csharp
public interface ITickable : IFrameTickable
    void Tick();
```

### ObFramework.Integrated.Clock.IFixedTickable

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/ITickable.cs#L10).

```csharp
public interface IFixedTickable : IFrameTickable
    void FixedTick();
```

### ObFramework.Integrated.Clock.ILateTickable

Declared at [line 15](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/ITickable.cs#L15).

```csharp
public interface ILateTickable : IFrameTickable
    void LateTick();
```

### ObFramework.Integrated.Clock.ILogicTickable

Declared at [line 20](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Clock/ITickable.cs#L20).

```csharp
public interface ILogicTickable : IFrameTickable
    void LogicTick();
```
