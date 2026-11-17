# ObFramework.Adapter.Godot / Core

[API index](README.md)

## Core/AGameLauncher.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/AGameLauncher.cs)

### ObFramework.Adapter.Godot.Core.AGameLauncher

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/AGameLauncher.cs#L8).

```csharp
public abstract partial class AGameLauncher : AViewController
    protected abstract PluginApplication CreateApplication();
    protected virtual GodotRuntimeHost CreateRuntimeHost();
    public override IArchitecture GetArchitecture();
    public Task StopAsync();
    public override void _Ready();
```

## Core/AViewController.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/AViewController.cs)

### ObFramework.Adapter.Godot.Core.AViewController

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/AViewController.cs#L6).

```csharp
public abstract partial class AViewController : Node, IController
    public abstract IArchitecture GetArchitecture();
```

## Core/GodotFrameLoopBridge.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotFrameLoopBridge.cs)

### ObFramework.Adapter.Godot.Core.GodotFrameLoopBridge

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotFrameLoopBridge.cs#L7).

```csharp
public sealed partial class GodotFrameLoopBridge : Node, IFrameLoopDriver
    public GodotFrameLoopBridge();
    public override void _Ready();
    public void Bind(ClockPlugin clock);
    public void Unbind();
    public override void _ExitTree();
    public override void _Process(double delta);
    public override void _PhysicsProcess(double delta);
```

## Core/GodotLateFrameLoopNode.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotLateFrameLoopNode.cs)

### ObFramework.Adapter.Godot.Core.GodotLateFrameLoopNode

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotLateFrameLoopNode.cs#L5).

```csharp
public sealed partial class GodotLateFrameLoopNode : Node
    public override void _Process(double delta);
```

## Core/GodotRuntimeHost.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotRuntimeHost.cs)

### ObFramework.Adapter.Godot.Core.GodotRuntimeHost

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Core/GodotRuntimeHost.cs#L10).

```csharp
public partial class GodotRuntimeHost : Node, IApplicationFrameLoopDriver
    public PluginApplication? Application { get; private set; }
    public static GodotRuntimeHost? Find(SceneTree tree);
    public Task StopAsync();
    protected virtual void PrepareRuntime(IArchitecture architecture);
    public override void _Notification(int what);
    public override void _ExitTree();
```
