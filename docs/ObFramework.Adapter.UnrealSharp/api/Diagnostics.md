# ObFramework.Adapter.UnrealSharp / Diagnostics

[API index](README.md)

## Diagnostics/UnrealDiagnostics.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Diagnostics/UnrealDiagnostics.cs)

### ObFramework.Adapter.UnrealSharp.Diagnostics.UnrealDiagnostics

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Diagnostics/UnrealDiagnostics.cs#L7).

```csharp
public static class UnrealDiagnostics
    public static DiagnosticsPlugin Create();
```

## Diagnostics/UnrealLogSink.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Diagnostics/UnrealLogSink.cs)

### ObFramework.Adapter.UnrealSharp.Diagnostics.ObFLog

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Diagnostics/UnrealLogSink.cs#L7).

```csharp
[CustomLog] public static partial class ObFLog
```

### ObFramework.Adapter.UnrealSharp.Diagnostics.UnrealLogSink

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Diagnostics/UnrealLogSink.cs#L11).

```csharp
public sealed class UnrealLogSink : ILogEventSink
    public void Emit(LogEvent logEvent);
```
