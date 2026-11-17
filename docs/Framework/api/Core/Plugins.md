# Framework / Core/Plugins

[API index](../README.md)

## Core/Plugins/IPlugin.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/IPlugin.cs)

### ObFramework.Core.IPlugin

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/IPlugin.cs#L6).

```csharp
public interface IPlugin
    void Register(PluginRegistration registration);
    Task InitAsync(IPluginContext plugins, CancellationToken ct);
    Task StopAsync(IPluginContext plugins);
```

### ObFramework.Core.IPluginContext

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/IPlugin.cs#L13).

```csharp
public interface IPluginContext
    T Get<T>() where T : class, IPlugin;
```

## Core/Plugins/PluginApplication.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginApplication.cs)

### ObFramework.Core.PluginApplication

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginApplication.cs#L10).

```csharp
public abstract class PluginApplication
    public IArchitecture Architecture { get; };
    protected abstract IPlugin[] CreatePlugins();
    protected abstract IArchitecture CreateArchitecture(IPluginContext plugins);
    protected abstract Task EnterGameAsync(CancellationToken ct);
    protected virtual Task StopGameAsync();
    protected virtual void ReleaseAdapters();
    public Task StartAsync(IFrameLoopDriver? driver = null);
    public Task RunAsync(Func<CancellationToken, Task> work);
    public Task StopAsync();
```

## Core/Plugins/PluginHost.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginHost.cs)

### ObFramework.Core.PluginHost

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginHost.cs#L8).

```csharp
public sealed class PluginHost
    public IPluginContext Context { get; }
    public PluginHost(params IPlugin[] plugins);
    public Task StartAsync(CancellationToken ct = default);
    public Task StopAsync();
```

## Core/Plugins/PluginRegistration.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginRegistration.cs)

### ObFramework.Core.PluginRegistration

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginRegistration.cs#L6).

```csharp
public sealed class PluginRegistration
    public void Require<T>() where T : class, IPlugin;
```
