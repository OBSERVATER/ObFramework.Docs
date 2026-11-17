# Diagnostics and logging

[API](../api/Integrated/Diagnostics.md) · [Manager source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Diagnostics/DiagnosticsManager.cs)

Include `new DiagnosticsPlugin(logDirectory, LogEventLevel.Information)` in the plugin list. Once started, `plugin.GetLogger("Inventory")` returns a Serilog logger with `SourceContext = Inventory`. The internally created logger writes through an async file sink, rolls at 10 MiB and retains up to ten files. Shutdown disposes that owned logger and flushes its sinks.

```csharp
using ObFramework.Integrated.Diagnostics;

public static class LogExample
{
    public static void Report(DiagnosticsPlugin diagnostics)
        => diagnostics.GetLogger("Inventory").Information("Loaded {Count} items", 12);
}
```

The constructor accepting an external `ILogger` borrows it; shutdown does not dispose it. An extra supplied sink is also borrowed. For the external-logger constructor, the existing logger determines filtering; the passed level is not reapplied by `Initialize(ILogger)`.
