# Godot resources and diagnostics

Register `ResourcePlugin.Create(new GodotAssetAdapter())` in the application manifest. The resource manager supplies cache keys, independent business handles and shutdown ownership; this adapter maps requests to Godot APIs.

## Read through the plugin

This method excerpt assumes `assets` is a running resource plugin:

```csharp
using System.Threading;
using System.Threading.Tasks;
using Godot;
using ObFramework.Integrated.Resource;

public static async Task ReadSceneAsync(ResourcePlugin assets, CancellationToken ct)
{
    using var handle = await assets.LoadAssetAsync<PackedScene>("res://UI/ShopView.tscn", ct);
    PackedScene scene = handle.Get<PackedScene>();
    // Use the scene while its business handle is alive.
}
```

The referenced scene must exist in the game; this method does not instantiate it. The UI adapter manages the scene handle for windows it creates.

## Native behavior and cancellation

Synchronous reads use `ResourceLoader.Load<T>` and throw `FileNotFoundException` on a null result. Async reads use `LoadThreadedRequest`, await `SceneTree.ProcessFrame` while the request is in progress, and verify loaded status/type before returning. They require a running `SceneTree`.

The current `ReadAsync` accepts a token but does not inspect it inside the Godot loading loop. Do not rely on immediate cancellation of a threaded native request. The shared resource manager still applies its caller/entry lifetime rules; shutdown can need to wait until the adapter returns.

Successful asset results carry no explicit native handle, and `Release(Resource)` is a no-op. Godot's managed/native resource lifetime remains relevant after the framework releases its entry. This differs from providers that transfer a native reference-counted operation handle.

Raw-file reads run in `Task.Run`, open `Godot.FileAccess`, read bytes and dispose the file. They expose no per-request cancellation token. Confirm the chosen paths and Godot threading behavior in the target engine before using platform-specific raw-file workflows.

## Logs

`GodotDiagnostics.Create()` creates a `DiagnosticsPlugin` with a `GodotLogSink`, logs under `OS.GetUserDataDir()/Logs`, and chooses Information for debug builds or Warning otherwise. The engine-independent diagnostics plugin owns its logging pipeline; the Godot sink maps it to native logging.

Sources: [asset adapter](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Resource/GodotAssetAdapter.cs), [diagnostics factory](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Diagnostics/GodotDiagnostics.cs), [log sink](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Diagnostics/GodotLogSink.cs). No engine-loading result is claimed by this documentation-only update.
