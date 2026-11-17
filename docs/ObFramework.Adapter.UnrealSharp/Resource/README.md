# UnrealSharp resources and diagnostics

`UnrealAssetAdapter` maps the shared resource plugin to Unreal object loading. Register `ResourcePlugin.Create(new UnrealAssetAdapter())` for application asset/raw-file requests. UMG's `UnrealUIAdapter` loads widget classes separately through Unreal and does not consume this resource plugin.

## UObject loading

Only requested types derived from `UObject` are supported. Synchronous reads call `StaticLoadObject`; async reads call `AsyncStaticLoadObject` and resume with `ConfigureWithUnrealContext()`. Failed/null/destroyed loads and incompatible result types throw before ownership is transferred.

Example method excerpt for a running resource plugin; replace the asset path with cooked content from your application:

```csharp
using System.Threading;
using System.Threading.Tasks;
using ObFramework.Integrated.Resource;
using UnrealSharp.CoreUObject;

public static async Task ReadAssetAsync(ResourcePlugin resources, CancellationToken ct)
{
    using var handle = await resources.LoadAssetAsync<UObject>(
        "/Game/Data/MyAsset.MyAsset", ct);
    UObject asset = handle.Get<UObject>();
    // Keep the business handle alive while using its asset.
}
```

A successful result creates a `TStrongObjectPtr<UObject>` and transfers it to the manager. Last business-handle disposal invokes the adapter's `Release`, which disposes that strong reference. Do not separately dispose the transferred native handle. Caller cancellation is checked before and after native loading; it does not guarantee the native operation stops immediately.

## Raw files

`ReadRawFileAsync` calls `File.ReadAllBytesAsync(location)` and returns bytes with no native handle. Locations are filesystem paths understood by .NET, not Unreal `/Game/...` object paths. Ensure files are deployed/accessibly mounted in packaged builds. The adapter's raw-file method has no token parameter, so shutdown/caller cancellation may wait for the filesystem read to return.

## Logs

`UnrealDiagnostics.Create()` uses `UnrealLogSink` and Information minimum level. Its current path calculation appends `Logs` after resolving `PluginsDirectory/../Saved/Logs`, yielding a nested `Saved/Logs/Logs` location; inspect this source when deciding your application's logging directory. The sink maps warnings/errors to `ObFLog` and includes exception text. `ObFLog` relies on UnrealSharp's `[CustomLog]` source generation.

Sources: [asset adapter](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Resource/UnrealAssetAdapter.cs), [diagnostics factory](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Diagnostics/UnrealDiagnostics.cs), [sink](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Diagnostics/UnrealLogSink.cs). UObject load/release and raw-file accessibility require verification in the selected UnrealSharp/packaged environment.
