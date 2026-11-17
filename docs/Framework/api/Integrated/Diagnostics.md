# Framework / Integrated/Diagnostics

[API index](../README.md)

## Integrated/Diagnostics/DiagnosticsOption.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Diagnostics/DiagnosticsOption.cs)

### ObFramework.Integrated.Diagnostics.DiagnosticsOption

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Diagnostics/DiagnosticsOption.cs#L3).

```csharp
public class DiagnosticsOption
    public int MaxFileSizeMB { get; set; };
    public int MaxFileCount { get; set; };
    public string OutputDirPath { get; set; };
    public Serilog.Events.LogEventLevel MinLevel { get; set; };
    public DiagnosticsOption();
    public DiagnosticsOption(string outputPath);
```

## Integrated/Diagnostics/DiagnosticsPlugin.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Diagnostics/DiagnosticsPlugin.cs)

### ObFramework.Integrated.Diagnostics.DiagnosticsPlugin

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Diagnostics/DiagnosticsPlugin.cs#L11).

```csharp
public sealed class DiagnosticsPlugin : IPlugin
    public DiagnosticsPlugin(string outputDirectory, LogEventLevel lvl, ILogEventSink? extraSink = null);
    public DiagnosticsPlugin(ILogger logger, LogEventLevel lvl);
    public ILogger GetLogger(string source);
```
