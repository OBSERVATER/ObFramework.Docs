# Clock and frame callbacks

[API](../api/Integrated/Clock.md) · `Framework/Integrated/Clock/ClockPlugin.cs`

`new ClockPlugin(interval)` defines a finite positive logical tick interval in seconds. Include it in the application's plugin list; the engine adapter binds its frame driver during startup and unbinds it during shutdown.

## Gameplay in an engine

When running under a game engine, let the engine adapter drive the clock. Gameplay code must not call `Advance()` or `Next()`. Use `Schedule()`, `Repeat()`, and tick callbacks instead. Manually advancing an engine-driven clock adds extra logical ticks and can make timers fire early.

The following method receives the running application's ClockPlugin:

```csharp
using System;
using ObFramework.Integrated.Clock;

public static class GameplayTimers
{
    public static void Register(ClockPlugin clock)
    {
        clock.Schedule(3, () => Console.WriteLine("three ticks later"));
        clock.Repeat(2, () => Console.WriteLine("every two ticks"));
        // The engine adapter advances this clock.
    }
}
```

Delays and intervals are positive tick counts, not milliseconds. There is currently no public timer cancellation handle. Register an `IFrameTickable` implementing the relevant logic/update/fixed/late interfaces to receive callbacks from the application's frame owner. Engine-specific frame phases enter through the adapter's frame driver.

## Engine-free execution

Reserve manual advancement for a clock owned by an explicit driver outside a game engine, such as a console application or standalone simulation:

```csharp
using System;
using ObFramework.Integrated.Clock;

var clock = new ClockPlugin(0.02f);
clock.Schedule(3, () => Console.WriteLine("tick 3"));
clock.Repeat(2, () => Console.WriteLine("every two ticks"));
clock.Advance(3); // Prints "every two ticks", then "tick 3".
```

`Advance(count)` advances a nonnegative tick count. Alternatively, an engine-free driver can call `Next(dt)` with a finite nonnegative elapsed time; it accumulates time and advances as many logical ticks as needed. Neither method dispatches engine update/fixed/late phases. Both reject reentrant advancement from tick or timer callbacks. Keep one driver for a clock; manual advancement remains a usage constraint rather than an engine-binding check enforced by these public methods.
