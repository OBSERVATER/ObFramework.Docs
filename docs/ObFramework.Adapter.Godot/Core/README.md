# Godot bootstrap and frame loop

Attach an `AGameLauncher` subclass to a Bootstrap scene and implement `CreateApplication()`. `_Ready()` schedules launch through a deferred callable, allowing the SceneTree to finish attaching the scene. If a runtime is already present, the launcher leaves it running.

## Minimal declarations

The following declarations use only a clock and empty Architecture. In a Godot C# project, keep each Godot script in a file/class layout accepted by the engine:

```csharp
using System.Threading;
using System.Threading.Tasks;
using ObFramework.Adapter.Godot.Core;
using ObFramework.Core;
using ObFramework.Integrated.Clock;

public partial class BootstrapLauncher : AGameLauncher
{
    protected override PluginApplication CreateApplication() => new GameApplication();
}

public sealed class GameApplication : PluginApplication
{
    protected override IPlugin[] CreatePlugins() =>
        new IPlugin[] { new ClockPlugin(1f / 30f) };

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

`ClockPlugin` is required when this application is started with the host's frame driver. The interval above produces 30 logical ticks per second; native process and physics phases remain engine-driven.

## Custom host and persistence

Override `CreateRuntimeHost()` to return a `GodotRuntimeHost` subclass. It may be detached or a Bootstrap node. Attach places/reparents it directly beneath `SceneTree.Root`, preserving it when Bootstrap is replaced. It cannot be the current scene or an ancestor of the launcher. Place application-persistent children under the host and keep the launcher outside that subtree.

Override `PrepareRuntime(IArchitecture)` for engine setup that requires an initialized Architecture. The host adds `GodotFrameLoopBridge` before this hook. Binding enables process/physics callbacks; the bridge uses minimum process priorities and a child late node with maximum process priority. Unbind disables all three dispatch paths.

## Shutdown

Resolve the active host with `GodotRuntimeHost.Find(tree)`. For a programmatic exit, await `host.StopAsync()` before calling `tree.Quit()`. The host shares its stop task, awaits `PluginApplication.StopAsync()`, restores the original `AutoAcceptQuit`, frees the UI root it owns and queues itself for deletion.

While attached, `AutoAcceptQuit` is false. A window-close notification starts the same observed shutdown and then quits the tree. `_ExitTree` also starts an observed cleanup fallback, which does not guarantee a forcibly terminated process will await all asynchronous work. Critical cleanup belongs in the explicit awaited stop path.

`UIRoot.Ensure(tree)` records a newly created root as host-owned when called after the host attaches. An existing root is borrowed and remains application-owned. Initialize UI through a plugin callback during host startup to establish the intended owner.

Sources: [launcher](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/AGameLauncher.cs), [host](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotRuntimeHost.cs), [bridge](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotFrameLoopBridge.cs). Scene-tree behavior requires a real Godot run.
