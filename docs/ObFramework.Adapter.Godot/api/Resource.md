# ObFramework.Adapter.Godot / Resource

[API index](README.md)

## Resource/GodotAssetAdapter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Resource/GodotAssetAdapter.cs)

### ObFramework.Adapter.Godot.Resource.GodotAssetAdapter

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/Resource/GodotAssetAdapter.cs#L11).

```csharp
public class GodotAssetAdapter : IAssetAdapter<global::Godot.Resource>, IRawFileAdapter<global::Godot.Resource>
    public AssetLoadResult<T, global::Godot.Resource> Read<T>(AssetKey request) where T : class;
    public async Task<AssetLoadResult<T, global::Godot.Resource>> ReadAsync<T>(AssetKey request, CancellationToken ct = default) where T : class;
    public void Release(global::Godot.Resource handle);
    public Task<AssetLoadResult<byte[], global::Godot.Resource>> ReadRawFileAsync(string location);
```
