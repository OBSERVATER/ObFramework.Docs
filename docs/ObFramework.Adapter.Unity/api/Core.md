# ObFramework.Adapter.Unity / Core

[API index](README.md)

## Core/AGameLauncher.cs

`ObFramework.Adapter.Unity/Core/AGameLauncher.cs` (source checkout)

### ObFramework.Adapter.Unity.Core.AGameLauncher

Declared at `ObFramework.Adapter.Unity/Core/AGameLauncher.cs:10` (source checkout).

```csharp
public abstract class AGameLauncher : AViewController
    protected abstract PluginApplication CreateApplication();
    protected virtual string? UIRootResourceKey { get; };
    protected virtual Task StartGameAsync(CancellationToken cancellationToken);
    public override IArchitecture GetArchitecture();
    public abstract Task StartGameAsync();
```

## Core/UnityFrameLoopBridge.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Core/UnityFrameLoopBridge.cs)

### ObFramework.Adapter.Unity.Core.UnityFrameLoopBridge

Declared at [line 17](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Core/UnityFrameLoopBridge.cs#L17).

```csharp
public sealed class UnityFrameLoopBridge : IApplicationFrameLoopDriver
    public UnityFrameLoopBridge();
    public void Bind(ClockPlugin clock);
    public void Unbind();
```

## Core/UnityRuntimeHost.cs

`ObFramework.Adapter.Unity/Core/UnityRuntimeHost.cs` (source checkout)

### ObFramework.Adapter.Unity.Core.UnityRuntimeHost

Declared at `ObFramework.Adapter.Unity/Core/UnityRuntimeHost.cs:14` (source checkout).

```csharp
[DisallowMultipleComponent] public sealed class UnityRuntimeHost : MonoBehaviour
    public static UnityRuntimeHost? Current { get; private set; }
    public PluginApplication? Application { get; private set; }
    public UIRoot? RuntimeUIRoot { get; private set; }
    public async Task ShutdownAsync();
```

## Core/ViewController.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Core/ViewController.cs)

### ObFramework.Adapter.Unity.UI.AViewController

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/Core/ViewController.cs#L6).

```csharp
public abstract class AViewController : MonoBehaviour, IController
    public abstract IArchitecture GetArchitecture();
```
