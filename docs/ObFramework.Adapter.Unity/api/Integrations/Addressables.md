# ObFramework.Adapter.Unity / Integrations/Addressables

[API index](../README.md)

## Integrations/Addressables/AddressablesAssetAdapter.cs

`ObFramework.Adapter.Unity/Integrations/Addressables/AddressablesAssetAdapter.cs` (source checkout)

### ObFramework.Adapter.Unity.Resource.AddressablesAssetAdapter

Declared at `ObFramework.Adapter.Unity/Integrations/Addressables/AddressablesAssetAdapter.cs:12` (source checkout).

```csharp
public sealed class AddressablesAssetAdapter : IAssetAdapter<AsyncOperationHandle>, IRawFileAdapter<AsyncOperationHandle>
    public AssetLoadResult<T, AsyncOperationHandle> Read<T>(AssetKey request) where T : class;
    public async Task<AssetLoadResult<T, AsyncOperationHandle>> ReadAsync<T>(AssetKey request, CancellationToken ct = default) where T : class;
    public async Task<AssetLoadResult<byte[], AsyncOperationHandle>> ReadRawFileAsync(string location);
    public void Release(AsyncOperationHandle handle);
```
