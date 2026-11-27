# Framework / Integrated/UI

[API index](../README.md)

## Integrated/UI/AUIView.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/AUIView.cs)

### ObFramework.Integrated.UI.AUIView

Declared at [line 15](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/AUIView.cs#L15).

```csharp
public abstract class AUIView : IController, ICanSetArchitecture, IDisposable, IViewEnabled, IViewDisabled
    public bool IsInitialized { get; };
    public IUIData? Data { get; };
    public IArchitecture GetArchitecture();
    public void Bind(IUIData data);
    protected virtual void OnInit(IUIData? data);
    protected virtual void OnOpen(IUIData? data);
    protected virtual void OnClose();
    protected virtual void OnBind(IUIData data);
    protected virtual void OnDestroy();
    public virtual void OnViewEnabled();
    public virtual void OnViewDisabled();
    public event Action<string>? PropertyChanged;
    public event Action<string>? CommandStateChanged;
    protected void NotifyPropertyChanged(string propertyName);
    protected void NotifyCommandStateChanged(string commandName);
    public bool IsCommandRunning(string commandName);
    public void Dispose();
```

## Integrated/UI/EUILevel.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/EUILevel.cs)

### ObFramework.Integrated.UI.EUILevel

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/EUILevel.cs#L3).

```csharp
public enum EUILevel
    Scene,
    Common,
    Overlay,
```

## Integrated/UI/EUIOpenMode.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/EUIOpenMode.cs)

### ObFramework.Integrated.UI.EUIOpenMode

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/EUIOpenMode.cs#L3).

```csharp
public enum EUIOpenMode
    Single,
    Multiple,
```

## Integrated/UI/IUIAdapter.cs

`Framework/Integrated/UI/IUIAdapter.cs` (source checkout)

### ObFramework.Integrated.UI.IUIAdapter

Declared at `Framework/Integrated/UI/IUIAdapter.cs:12` (source checkout).

```csharp
public interface IUIAdapter
    Task<UIViewInstance?> CreateAsync(Type viewType, CancellationToken ct = default);
    void Open(UIViewInstance instance, IUIData? data = null);
    void Close(UIViewInstance instance);
    void Release(UIViewInstance instance);
    bool TryGetView(IUIWindow window, out AUIView view);
```

## Integrated/UI/IUIData.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/IUIData.cs)

### ObFramework.Integrated.UI.IUIData

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/IUIData.cs#L3).

```csharp
public interface IUIData
```

### ObFramework.Integrated.UI.UIPanelData

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/IUIData.cs#L7).

```csharp
public abstract class UIPanelData : IUIData
```

## Integrated/UI/IUIWindow.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/IUIWindow.cs)

### ObFramework.Integrated.UI.IUIWindow

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/IUIWindow.cs#L10).

```csharp
public interface IUIWindow : IController
    EUILevel Level { get; }
```

## Integrated/UI/UIBindingDispatch.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIBindingDispatch.cs)

### ObFramework.Integrated.UI.UIBindingDispatch

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIBindingDispatch.cs#L12).

```csharp
public static class UIBindingDispatch
    public static bool IsMainThread { get; };
    public static void ConfigureMainThread(int mainThreadId, Action<Action>? post);
    public static void Post(Action action);
```

## Integrated/UI/UICommand.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UICommand.cs)

### ObFramework.Integrated.UI.AUICommand

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UICommand.cs#L13).

```csharp
public abstract class AUICommand : ACommand
    protected AUICommand(AUIView view, string commandName, object? parameter = null);
    public AUIView View { get; }
    public string CommandName { get; }
    public object? Parameter { get; }
    public bool IsRunning { get; };
    protected virtual bool CanExecuteCore();
    protected virtual bool CanExecuteCore(object? parameter);
    protected abstract void ExecuteCore();
    public bool CanExecute();
    public bool CanExecute(object? parameter);
    public sealed override void Execute();
```

### ObFramework.Integrated.UI.AUIAsyncCommand

Declared at [line 65](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UICommand.cs#L65).

```csharp
public abstract class AUIAsyncCommand : AAsyncCommand
    protected AUIAsyncCommand(AUIView view, string commandName, object? parameter = null);
    public AUIView View { get; }
    public string CommandName { get; }
    public object? Parameter { get; }
    public bool AllowConcurrentExecutions { get; protected set; }
    public bool CancelOnClose { get; protected set; };
    public bool IsRunning { get; };
    protected virtual bool CanExecuteCore();
    protected virtual bool CanExecuteCore(object? parameter);
    protected abstract ValueTask ExecuteCore(CancellationToken ct);
    public bool CanExecute();
    public bool CanExecute(object? parameter);
    public sealed override async ValueTask Execute(CancellationToken ct);
```

## Integrated/UI/UIPlugin.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIPlugin.cs)

### ObFramework.Integrated.UI.UIPlugin

Declared at [line 14](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIPlugin.cs#L14).

```csharp
public sealed class UIPlugin : IPlugin
    public UIPlugin(IUIAdapter adapter, int cacheCapacity = 10, Func<IUIAdapter, CancellationToken, Task>? initialize = null, Func<IUIAdapter, Task>? shutdown = null);
    public UIPlugin(Func<ResourcePlugin, IUIAdapter> createAdapter, int cacheCapacity = 10, Func<IUIAdapter, CancellationToken, Task>? initialize = null, Func<IUIAdapter, Task>? shutdown = null);
    public Task<T?> OpenAsync<T>(EUIOpenMode mode = EUIOpenMode.Single, IUIData? data = null, CancellationToken cancellationToken = default) where T : AUIView;
    public bool TryGet<T>(out T? view) where T : AUIView;
    public bool Close(AUIView? view);
    public bool Close<T>() where T : AUIView;
    public int CloseAll<T>() where T : AUIView;
    public void Push<T>(T view) where T : AUIView;
    public Task<T?> Pop<T>() where T : AUIView;
```

## Integrated/UI/UIViewInstance.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIViewInstance.cs)

### ObFramework.Integrated.UI.UIViewInstance

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIViewInstance.cs#L10).

```csharp
public readonly struct UIViewInstance : IEquatable<UIViewInstance>
    public IUIWindow Window { get; }
    public AUIView View { get; }
    public EUILevel Level { get; };
    public UIViewInstance(IUIWindow window, AUIView view);
    public bool IsValid { get; };
    public bool Equals(UIViewInstance other);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public static bool operator ==(UIViewInstance left, UIViewInstance right);
    public static bool operator !=(UIViewInstance left, UIViewInstance right);
```

## Integrated/UI/UIViewRegistry.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIViewRegistry.cs)

### ObFramework.Integrated.UI.UIViewRegistration

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIViewRegistry.cs#L11).

```csharp
public readonly struct UIViewRegistration
    public Type ViewType { get; }
    public Type WindowType { get; }
    public string ResourceKey { get; }
    public Func<IArchitecture, AUIView>? Factory { get; }
    public UIViewRegistration(Type viewType, Type windowType, string resourceKey, Func<IArchitecture, AUIView>? factory);
```

### ObFramework.Integrated.UI.UIViewRegistry

Declared at [line 39](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/UI/UIViewRegistry.cs#L39).

```csharp
public sealed class UIViewRegistry
    public IReadOnlyCollection<UIViewRegistration> All { get; };
    public bool TryGet(Type viewType, out UIViewRegistration registration);
    public void Register<TView, TWindow>(string resourceKey, Func<IArchitecture, TView>? factory = null) where TView : AUIView where TWindow : class, IUIWindow;
    public void Register(Type viewType, Type windowType, string resourceKey, Func<IArchitecture, AUIView>? factory = null);
```
