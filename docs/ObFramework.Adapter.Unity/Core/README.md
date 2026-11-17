# Unity bootstrap and lifetime

Attach a concrete `AGameLauncher` to a Bootstrap scene object. It constructs one `PluginApplication`; `UnityRuntimeHost` owns application shutdown and persistent roots, while `UnityFrameLoopBridge` connects the framework clock to Unity's PlayerLoop.

## Minimal launcher

This complete launcher/application declaration starts an application with a clock and an empty Architecture. It intentionally creates no gameplay scene or UI:

```csharp
using System.Threading;
using System.Threading.Tasks;
using ObFramework.Adapter.Unity.Core;
using ObFramework.Core;
using ObFramework.Integrated.Clock;

public sealed class BootstrapLauncher : AGameLauncher
{
    protected override PluginApplication CreateApplication() => new GameApplication();
}

public sealed class GameApplication : PluginApplication
{
    protected override IPlugin[] CreatePlugins() => new IPlugin[] { new ClockPlugin(1f / 30f) };

    protected override IArchitecture CreateArchitecture(IPluginContext plugins)
    {
        GameArchitecture.InitInterface(plugins);
        return GameArchitecture.Interface;
    }

    protected override Task EnterGameAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
}

public sealed class GameArchitecture : AArchitecture<GameArchitecture>
{
    protected override void Init() { }
}
```

Use this declaration in a Unity project where the runtime and adapter packages are installed. `ClockPlugin` is needed because application startup binds the supplied frame driver to that plugin. Each `PluginApplication` starts once; create a new instance for a new application lifetime.

## Persistent roots

By default the launcher reserves a new root named `OBF Runtime`. Override `GetGameManager()` to supply an existing active root in the Bootstrap scene. Override `GetPersistentRoots()` to include additional roots. Selected roots must be root GameObjects from Bootstrap and must sit outside the launcher hierarchy. A second active runtime is rejected.

The runtime also collects Bootstrap `UIRoot` roots, calls their `Init()`, moves selected roots to `DontDestroyOnLoad` and scans initial roots for generated bindings. A `UIRoot` can move itself during `Awake`; the runtime remembers its original scene when recognizing it. Keep the launcher outside the manager subtree. Host shutdown destroys selected persistent roots; only return objects whose lifetime belongs to the application.

## Frame dispatch

`UnityFrameLoopBridge` inserts its own callbacks immediately before Unity's script Update, FixedUpdate and LateUpdate callbacks. It removes only callbacks owned by that bridge on unbind. A duplicate installed OBF bridge is rejected.

View binding preparation runs after Architecture creation and before game entry. Enabled bound Views that implement framework tick interfaces register with the clock; disabling or releasing their controller removes those registrations. Views implementing the enabled lifecycle can defer their first tick until the following frame, preventing a deferred native destruction from receiving a tick in the activation frame.

## Stop and quit

Await `UnityRuntimeHost.Current.ShutdownAsync()` or the launcher's `ShutdownAsync()` before an application-directed exit or replacing the runtime. The application's shared stop task handles repeated stop requests. Cleanup unbinds the frame driver, requests startup/work cancellation, drains tracked work, releases View bindings, invokes game shutdown/deinitializes Architecture, stops plugins and releases application-owned adapters. A failing cleanup stage is collected so later cleanup can continue.

The host intercepts `Application.wantsToQuit`, awaits shutdown, yields once and then requests quit again after removing its handler. `OnDestroy` starts an observed cleanup fallback, but asynchronous completion during forced engine/process teardown still needs engine-level acceptance testing.

For a component-owned subscription, `DisposeWhenDestroyed(owner)` registers disposal against `destroyCancellationToken`. `ObExecutionScope.StopWhenDestroyed(owner)` requests scope shutdown and logs failures. Destruction callbacks start asynchronous scope draining; they do not make Unity await its completion. Use the application's awaited shutdown path for work that must finish before dependencies disappear.

## Related guidance

- [Launcher](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Core/AGameLauncher.cs), [host](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Core/UnityRuntimeHost.cs), [frame bridge](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Core/UnityFrameLoopBridge.cs).

- Real scene transitions, quit handling and PlayerLoop placement require the consuming Unity project.
