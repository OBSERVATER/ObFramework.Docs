# ObFramework.Adapter.UnrealSharp / Resource

[API index](README.md)

## Resource/UnrealAssetAdapter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Resource/UnrealAssetAdapter.cs)

### ObFramework.Adapter.UnrealSharp.Resource.UnrealAssetAdapter

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/Resource/UnrealAssetAdapter.cs#L7).

```csharp
public sealed class UnrealAssetAdapter : IAssetAdapter<TStrongObjectPtr<UObject>>, IRawFileAdapter<TStrongObjectPtr<UObject>>
    public AssetLoadResult<T, TStrongObjectPtr<UObject>> Read<T>(AssetKey request) where T : class;
    public async Task<AssetLoadResult<T, TStrongObjectPtr<UObject>>> ReadAsync<T>(AssetKey request, CancellationToken ct = default) where T : class;
    public void Release(TStrongObjectPtr<UObject> handle);
    public async Task<AssetLoadResult<byte[], TStrongObjectPtr<UObject>>> ReadRawFileAsync(string location);
```
