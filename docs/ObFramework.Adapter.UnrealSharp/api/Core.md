# ObFramework.Adapter.UnrealSharp / Core

[API index](README.md)

## Core/AGameLauncher.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Core/AGameLauncher.cs)

### ObFramework.Adapter.UnrealSharp.Core.AGameLauncher

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Core/AGameLauncher.cs#L8).

```csharp
[UClass(ClassFlags.Abstract)] public partial class AGameLauncher : AActor
    protected virtual PluginApplication CreateApplication();
    public override void BeginPlay();
```

## Core/UObFrameworkSubsystem.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Core/UObFrameworkSubsystem.cs)

### ObFramework.Adapter.UnrealSharp.Core.UObFrameworkSubsystem

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Core/UObFrameworkSubsystem.cs#L9).

```csharp
[UClass] public partial class UObFrameworkSubsystem : UCSGameInstanceSubsystem, IFrameLoopDriver
    public PluginApplication? Application { get; private set; }
    public override void Initialize(FSubsystemCollectionBaseRef collection);
    public Task StartAsync(PluginApplication application);
    public Task StopAsync();
    public void Bind(ClockPlugin clock);
    public void Unbind();
    public override void Tick(float deltaSeconds);
    public override void Deinitialize();
```
