# Plugin composition and application lifetime

[API](../api/Core/Plugins.md) · [Application source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginApplication.cs) · [Host source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginHost.cs)

`IPlugin.Register` declares `registration.Require<TPlugin>()`. The host validates dependencies and initializes them before dependents. Plugin operations belong to the running phase. A plugin receives a context for its declared dependencies; missing, duplicate or cyclic dependencies fail rather than producing a partially usable host. Shutdown visits entered plugins in reverse order and aggregates cleanup failures.

```csharp
using System.Threading;
using System.Threading.Tasks;
using ObFramework.Core;
using ObFramework.Integrated.Clock;

public sealed class Game : AArchitecture<Game>
{
    protected override void Init() { }
}
public sealed class GameApplication : PluginApplication
{
    protected override IPlugin[] CreatePlugins() => new IPlugin[] { new ClockPlugin(0.02f) };
    protected override IArchitecture CreateArchitecture(IPluginContext plugins)
    {
        Game.InitInterface(plugins);
        return Game.Interface;
    }
    protected override Task EnterGameAsync(CancellationToken ct) => Task.CompletedTask;
}
// In an async method:
// var app = new GameApplication();
// try { await app.StartAsync(); } finally { await app.StopAsync(); }
```

Create one application per run; `StartAsync` can be called once. `RunAsync` admits work only while running and tracks it for shutdown. `StopAsync` is shared across callers. With an engine driver, startup prepares and binds it before entering game logic. See [architecture](../../architecture.md) for cleanup order.

Dependency declarations matter for resources: `ConfigPlugin` declares `ResourcePlugin`; `UIPlugin(Func<ResourcePlugin,IUIAdapter>)` also declares it and disposes factory-created adapters that implement `IDisposable`. The overload taking an already-created `IUIAdapter` borrows it and declares no Resource dependency. The application must arrange ownership and ordering for borrowed adapters. `ResourcePlugin` borrows its adapter; release application-owned adapters after plugin shutdown.

`SavePlugin.Bind(Architecture)` belongs after Architecture creation and before save/load. Unbind while the Architecture is still active during business shutdown. An engine launcher creates the application and its runtime host owns final stop; repeated native callbacks must share that stop operation.
