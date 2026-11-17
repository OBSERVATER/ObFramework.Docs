# UnrealSharp launcher and subsystem

Place an `AGameLauncher` subclass in the Bootstrap level and override `CreateApplication()`. `BeginPlay()` obtains `UObFrameworkSubsystem` from the GameInstance and starts the application. Startup failures are observed through `ObFLog`; a stop-triggered cancellation is handled as expected shutdown.

## Launcher example

These are application declarations for an UnrealSharp project with generated UClass glue enabled. The empty application supplies the clock required by the subsystem's frame driver:

```csharp
using System.Threading;
using System.Threading.Tasks;
using ObFramework.Adapter.UnrealSharp.Core;
using ObFramework.Core;
using ObFramework.Integrated.Clock;
using UnrealSharp.Attributes;

[UClass]
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

The subsystem starts at most one application and cannot restart after stop. A second launcher/start request is rejected; account for that when levels contain Bootstrap actors. `Application` is available after construction/start has been accepted; its Architecture is available only after application initialization.

## Native frame phases

The subsystem implements `IFrameLoopDriver`. Binding stores the clock and a continuation guard; unbinding clears both. Each Unreal tick dispatches fixed phases through a 0.02-second accumulator, up to four fixed steps per frame. Fixed-phase elapsed time is clamped to that catch-up budget. Update receives the original `deltaSeconds`, followed by Late if the clock remains bound and the subsystem has not stopped.

The clock's logical interval is separately configured by `ClockPlugin`. The example's 30 Hz logic interval does not change the subsystem's 50 Hz fixed-step constant. Native frame ordering and application tick behavior should be evaluated with real Unreal frames.

## Shutdown

For an application-controlled exit, obtain the subsystem through the owning Unreal object and **await `StopAsync()` before removing dependencies or exiting**. It marks the subsystem closed immediately and delegates to the application's shared shutdown task, suppressing further frame dispatch.

`Deinitialize()` disables ticking and begins observed async cleanup, then calls base deinitialization. This native teardown callback does not synchronously await the cleanup task. Use an explicit awaited stop when work/resources must drain before GameInstance destruction. Put game cleanup in `PluginApplication.StopGameAsync()` and external adapter cleanup in `ReleaseAdapters()` with their actual ownership boundaries.

Sources: [launcher](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Core/AGameLauncher.cs), [subsystem](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Core/UObFrameworkSubsystem.cs), [engine-independent application](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Core/Plugins/PluginApplication.cs). Real map/subsystem teardown requires engine validation.
