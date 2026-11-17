# Resource loading and ownership

[API](../api/Integrated/Resource.md) · [Handle API](../api/Integrated/Resource/Handle.md) · [ResourcePlugin](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/ResourcePlugin.cs)

Add `ResourcePlugin.Create(adapter)` to the application plugin list. Each host owns one resource manager/cache for that plugin; hosts do not share entries. Plain business objects receive the plugin explicitly. Architecture-attached `ICanGetPlugin` callers may use `this.GetPlugin<ResourcePlugin>()`.

The following method expects an initialized plugin and an existing location:

```csharp
using System.Threading;
using System.Threading.Tasks;
using ObFramework.Integrated.Resource;

public static class ResourceExample
{
    public static async Task<string?> ReadText(ResourcePlugin resources, CancellationToken ct)
    {
        using var handle = await resources.LoadRawFileAsync("Config/Game.json", ct);
        return handle.Get<string>();
    }
}
```

For assets, `using var handle = await resources.LoadAssetAsync<MyAsset>(location, ct)` and `handle.Get<MyAsset>()` use the same ownership rule. `MyAsset` is the application's reference type; native engine calls obey that engine's thread rules. The [managed sample](../../../examples/Documentation/Program.cs) provides an executable in-memory adapter.

## Implement a provider

Implement `IAssetAdapter<TNativeHandle>.Read<T>`, `ReadAsync<T>` and `Release`. Optionally implement `IRawFileAdapter<TNativeHandle>.ReadRawFileAsync` for raw files. Return `AssetLoadResult<T,TNativeHandle>` with a native handle when the manager must eventually release it; the asset-only constructor sets `HasNativeHandle = false`. If loading fails before ownership transfers to the manager, the adapter cleans up its native state. A provider may explicitly reject synchronous loading.

Entries are keyed by location, asset type and operation. Concurrent async requests share one underlying load but receive independent business handles. Dispose each handle; the last reference releases the native handle. Failed entries are removed so a later request can retry. A synchronous request encountering an in-progress async entry throws instead of blocking.

Caller cancellation does not cancel another caller's shared load. The current implementation awaits the underlying load, then checks each caller's token and drops the canceled caller's reference. Cancellation therefore may not end the caller's wait immediately. Raw `string` reads decode UTF-8; `byte[]` data is shared and should be treated as read-only.

## Shutdown

Stop accepting requests, request cancellation through the execution scope, wait for in-flight work, then release remaining entries. A provider that ignores cancellation must finish before shutdown completes. Close UI and release business handles before stopping resources. ResourcePlugin borrows the adapter and does not dispose it; the application disposes an owned adapter after plugin shutdown.

For Unity's optional provider, see [Addressables integration](../../ObFramework.Adapter.Unity/Integrations/Addressables.md).
