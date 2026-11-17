# Framework / Core

[API index](README.md)

## Core/IAdapter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IAdapter.cs)

### ObFramework.Core.IReadAdapter<out TResult>

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IAdapter.cs#L10).

```csharp
public interface IReadAdapter<out TResult>
    TResult Read();
```

### ObFramework.Core.IReadAdapter<out TResult, in TRequest>

Declared at [line 17](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IAdapter.cs#L17).

```csharp
public interface IReadAdapter<out TResult, in TRequest>
    TResult Read(TRequest request);
```

### ObFramework.Core.IAsyncReadAdapter<TResult, in TRequest>

Declared at [line 21](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IAdapter.cs#L21).

```csharp
public interface IAsyncReadAdapter<TResult, in TRequest>
    Task<TResult> ReadAsync(TRequest request, CancellationToken ct = default);
```

### ObFramework.Core.IWriteAdapter<in TValue>

Declared at [line 28](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IAdapter.cs#L28).

```csharp
public interface IWriteAdapter<in TValue>
    void Write(TValue value);
```

### ObFramework.Core.IAdapter<out TResult, in TValue>

Declared at [line 36](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IAdapter.cs#L36).

```csharp
public interface IAdapter<out TResult, in TValue> : IReadAdapter<TResult>, IWriteAdapter<TValue>
```

### ObFramework.Core.IAdapter<out TResult, in TRequest, in TValue>

Declared at [line 43](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IAdapter.cs#L43).

```csharp
public interface IAdapter<out TResult, in TRequest, in TValue> : IReadAdapter<TResult, TRequest>, IWriteAdapter<TValue>
```

## Core/IArchitecture.cs

`Framework/Core/IArchitecture.cs` (source checkout)

### ObFramework.Core.IArchitecture

Declared at `Framework/Core/IArchitecture.cs:13` (source checkout).

```csharp
public interface IArchitecture
    void RegisterSystem<I, T>() where I : ISystem where T : class, I, new();
    void RegisterModel<I, T>() where I : IModel where T : class, I, new();
    void RegisterLocalService<I, T>(ICanRegisterService self) where I : IService where T : class, I;
    IDisposable RegisterEvent<T>(Action<T> onEvent) where T : struct;
    IDisposable RegisterAsyncEvent<T>(Func<T, CancellationToken, ValueTask> func) where T : struct;
    T GetSystem<T>() where T : class, ISystem;
    T GetModel<T>() where T : class, IModel;
    T GetLocalService<T>(ICanRegisterService self) where T : class, IService;
    T GetPlugin<T>() where T : class, IPlugin;
    TResult SendQuery<TQuery, TResult>() where TQuery : class, IQuery<TResult>, new();
    TResult SendQuery<TResult>(IQuery<TResult> query);
    void SendCommand<T>(T command) where T : ICommand;
    TResult SendCommand<TResult>(ICommand<TResult> command);
    ValueTask SendCommandAsync<T>(T command, CancellationToken ct = default) where T : IAsyncCommand;
    ValueTask<TResult> SendCommandAsync<TResult>(IAsyncCommand<TResult> command, CancellationToken cts = default);
    void SendEvent<T>() where T : struct;
    void SendEvent<T>(T e) where T : struct;
    ValueTask SendEventAsync<T>(T msg, AsyncPublishStrategy strategy = AsyncPublishStrategy.Parallel, CancellationToken cts = default);
    void Deinit();
```

### ObFramework.Core.AArchitecture<T>

Declared at `Framework/Core/IArchitecture.cs:38` (source checkout).

```csharp
public abstract class AArchitecture<T> : IArchitecture, IModelSource where T : AArchitecture<T>, new()
    protected static T Architecture;
    protected IObjectResolver Resolver;
    public IPluginContext Plugins { get; };
    public bool Initialized { get; };
    public static IArchitecture Interface { get; }
    public static void InitInterface();
    public static void InitInterface(IPluginContext plugins);
    protected abstract void Init();
    public void Deinit();
    public IDisposable RegisterEvent<T1>(Action<T1> onEvent) where T1 : struct;
    public IDisposable RegisterAsyncEvent<T1>(Func<T1, CancellationToken, ValueTask> func) where T1 : struct;
    public void RegisterModel<I, T1>() where I : IModel where T1 : class, I, new();
    public void RegisterSystem<I, T1>() where I : ISystem where T1 : class, I, new();
    public TResult SendCommand<TResult>(ICommand<TResult> command);
    public void SendCommand<T1>(T1 command) where T1 : ICommand;
    public async ValueTask SendCommandAsync<T1>(T1 command, CancellationToken ct = default) where T1 : IAsyncCommand;
    public async ValueTask<TResult> SendCommandAsync<TResult>(IAsyncCommand<TResult> command, CancellationToken ct = default);
    public void SendEvent<T1>() where T1 : struct;
    public void SendEvent<T1>(T1 e) where T1 : struct;
    public async ValueTask SendEventAsync<T1>(T1 msg, AsyncPublishStrategy strategy = AsyncPublishStrategy.Parallel, CancellationToken cts = default);
    public TResult SendQuery<TQuery, TResult>() where TQuery : class, IQuery<TResult>, new();
    public TResult SendQuery<TResult>(IQuery<TResult> query);
    protected void RegisterPubService<I, T1>() where I : IService where T1 : class, I;
    public void RegisterLocalService<I, T1>(ICanRegisterService self) where I : IService where T1 : class, I;
    public IEnumerable<IModel> GetModelInsts();
```

## Core/IBindableProperty.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IBindableProperty.cs)

### ObFramework.Core.IReadonlyBindableProperty<T>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IBindableProperty.cs#L6).

```csharp
public interface IReadonlyBindableProperty<T>
    T Value { get; }
    void Bind(Action<T> onEvent, bool trigger = false);
    void UnBind(Action<T> onEvent);
```

### ObFramework.Core.IBindableProperty<T>

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IBindableProperty.cs#L12).

```csharp
public interface IBindableProperty<T> : IReadonlyBindableProperty<T>
    new T Value { get; set; }
    void SetWithoutEvent(T val);
```

### ObFramework.Core.BindableProperty<T>

Declared at [line 17](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IBindableProperty.cs#L17).

```csharp
public class BindableProperty<T> : IBindableProperty<T>
    protected T _val;
    public static Func<T, T, bool> Comparer { get; set; };
    public T Value { get; set; }
    public BindableProperty(T value = default);
    public BindableProperty<T> WithComparer(Func<T, T, bool> comparer);
    public void Bind(Action<T> onEvent, bool trigger = false);
    protected virtual T Get();
    protected virtual void Set(T val);
    public void SetWithoutEvent(T val);
    public void UnBind(Action<T> onEvent);
```

## Core/ICanRule.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs)

### ObFramework.Core.IBelongToArchitecture

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L3).

```csharp
public interface IBelongToArchitecture
    IArchitecture GetArchitecture();
```

### ObFramework.Core.ICanSetArchitecture

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L8).

```csharp
public interface ICanSetArchitecture
    void SetArchitecture(IArchitecture architecture);
```

### ObFramework.Core.ICanGetModel

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L13).

```csharp
public interface ICanGetModel : IBelongToArchitecture
```

### ObFramework.Core.ICanGetSystem

Declared at [line 17](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L17).

```csharp
public interface ICanGetSystem : IBelongToArchitecture
```

### ObFramework.Core.ICanRegisterEvent

Declared at [line 21](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L21).

```csharp
public interface ICanRegisterEvent : IBelongToArchitecture
```

### ObFramework.Core.ICanSendCommand

Declared at [line 25](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L25).

```csharp
public interface ICanSendCommand : IBelongToArchitecture
```

### ObFramework.Core.ICanSendEvent

Declared at [line 29](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L29).

```csharp
public interface ICanSendEvent : IBelongToArchitecture
```

### ObFramework.Core.ICanRegisterService

Declared at [line 32](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L32).

```csharp
public interface ICanRegisterService : IBelongToArchitecture
```

### ObFramework.Core.ICanQuery

Declared at [line 35](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L35).

```csharp
public interface ICanQuery : IBelongToArchitecture
```

### ObFramework.Core.ICanGetPlugin

Declared at [line 38](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L38).

```csharp
public interface ICanGetPlugin : IBelongToArchitecture
```

### ObFramework.Core.ICanInit

Declared at [line 41](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICanRule.cs#L41).

```csharp
public interface ICanInit
    bool Initialized { get; set; }
    void Init();
    void Deinit();
```

## Core/ICommand.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs)

### ObFramework.Core.ICommand<out TResult>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L6).

```csharp
public interface ICommand<out TResult> : ICanGetPlugin, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanSendEvent
    TResult Execute();
```

### ObFramework.Core.ICommand

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L10).

```csharp
public interface ICommand : ICanGetPlugin, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanSendEvent
    void Execute();
```

### ObFramework.Core.IAsyncCommand<TResult>

Declared at [line 14](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L14).

```csharp
public interface IAsyncCommand<TResult> : ICanGetPlugin, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanSendEvent
    ValueTask<TResult> Execute(CancellationToken ct);
```

### ObFramework.Core.IAsyncCommand

Declared at [line 18](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L18).

```csharp
public interface IAsyncCommand : ICanGetPlugin, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanSendEvent
    ValueTask Execute(CancellationToken ct);
```

### ObFramework.Core.ACommand

Declared at [line 22](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L22).

```csharp
public abstract class ACommand : ICommand
    public abstract void Execute();
    public IArchitecture GetArchitecture();
    public void SetArchitecture(IArchitecture architecture);
```

### ObFramework.Core.ACommand<T>

Declared at [line 29](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L29).

```csharp
public abstract class ACommand<T> : ICommand<T>
    public abstract T Execute();
    public IArchitecture GetArchitecture();
    public void SetArchitecture(IArchitecture architecture);
```

### ObFramework.Core.AAsyncCommand<T>

Declared at [line 36](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L36).

```csharp
public abstract class AAsyncCommand<T> : IAsyncCommand<T>
    public abstract ValueTask<T> Execute(CancellationToken ct);
    public IArchitecture GetArchitecture();
    public void SetArchitecture(IArchitecture architecture);
```

### ObFramework.Core.AAsyncCommand

Declared at [line 43](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ICommand.cs#L43).

```csharp
public abstract class AAsyncCommand : IAsyncCommand
    public abstract ValueTask Execute(CancellationToken ct);
    public IArchitecture GetArchitecture();
    public void SetArchitecture(IArchitecture architecture);
```

## Core/IController.cs

`Framework/Core/IController.cs` (source checkout)

### ObFramework.Core.IController

Declared at `Framework/Core/IController.cs:3` (source checkout).

```csharp
public interface IController : ICanGetPlugin, IBelongToArchitecture, ICanSendCommand, ICanGetSystem, ICanGetModel, ICanRegisterEvent, ICanQuery
```

## Core/IModel.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IModel.cs)

### ObFramework.Core.IModel

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IModel.cs#L5).

```csharp
public interface IModel : ICanGetPlugin, ICanGetModel, ICanGetSystem, ICanInit, ICanSetArchitecture, ICanRegisterEvent, ICanSendEvent, ICanQuery
```

### ObFramework.Core.AModel

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IModel.cs#L13).

```csharp
public abstract class AModel : IModel
    public bool Initialized { get; set; }
    protected abstract void OnInit();
    protected virtual void OnDeinit();
    public void SetArchitecture(IArchitecture architecture);
    public IArchitecture GetArchitecture();
```

## Core/IQuery.cs

`Framework/Core/IQuery.cs` (source checkout)

### ObFramework.Core.IQuery<out TResult>

Declared at `Framework/Core/IQuery.cs:6` (source checkout).

```csharp
public interface IQuery<out TResult> : ICanGetPlugin, IBelongToArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel, ICanQuery
    TResult Q();
```

### ObFramework.Core.AQuery<TResult>

Declared at `Framework/Core/IQuery.cs:15` (source checkout).

```csharp
public abstract class AQuery<TResult> : IQuery<TResult>
    public void SetArchitecture(IArchitecture architecture);
    public IArchitecture GetArchitecture();
    public abstract TResult Q();
```

## Core/IService.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IService.cs)

### ObFramework.Core.IService

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IService.cs#L3).

```csharp
public interface IService
```

## Core/ISystem.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ISystem.cs)

### ObFramework.Core.ISystem

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ISystem.cs#L3).

```csharp
public interface ISystem : ICanGetPlugin, ICanGetModel, ICanGetSystem, ICanInit, ICanSetArchitecture, ICanRegisterService, ICanRegisterEvent, ICanSendEvent, ICanQuery
```

### ObFramework.Core.ASystem

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/ISystem.cs#L6).

```csharp
public abstract class ASystem : ISystem
    public bool Initialized { get; set; }
    protected abstract void OnInit();
    protected virtual void OnDeinit();
    public void SetArchitecture(IArchitecture architecture);
    public IArchitecture GetArchitecture();
```

## Core/IViewLifecycle.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IViewLifecycle.cs)

### ObFramework.Core.IViewEnabled

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IViewLifecycle.cs#L3).

```csharp
public interface IViewEnabled
    void OnViewEnabled();
```

### ObFramework.Core.IViewDisabled

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/IViewLifecycle.cs#L8).

```csharp
public interface IViewDisabled
    void OnViewDisabled();
```
