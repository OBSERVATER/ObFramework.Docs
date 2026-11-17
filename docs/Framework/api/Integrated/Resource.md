# Framework / Integrated/Resource

[API index](../README.md)

## Integrated/Resource/AssetEntry.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/AssetEntry.cs)

### ObFramework.Integrated.Resource.AssetEntry

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/AssetEntry.cs#L5).

```csharp
public class AssetEntry
    public AssetKey Key;
    public object Asset;
    public int RefCount;
```

## Integrated/Resource/AssetKey.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/AssetKey.cs)

### ObFramework.Integrated.Resource.AssetKey

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/AssetKey.cs#L6).

```csharp
public readonly struct AssetKey : IEquatable<AssetKey>
    public readonly string Location;
    public readonly Type Type;
    public readonly string Operation;
    public AssetKey(string location, Type type, string operation = "Asset");
    public bool Equals(AssetKey other);
    public override bool Equals(object obj);
    public override int GetHashCode();
```

## Integrated/Resource/AssetLoadResult.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/AssetLoadResult.cs)

### ObFramework.Integrated.Resource.AssetLoadResult<TAsset, TNativeHandle>

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/AssetLoadResult.cs#L3).

```csharp
public readonly struct AssetLoadResult<TAsset, TNativeHandle> where TAsset : class
    public readonly TAsset Asset;
    public readonly TNativeHandle? NativeHandle;
    public readonly bool HasNativeHandle;
    public AssetLoadResult(TAsset asset, TNativeHandle nativeHandle);
    public AssetLoadResult(TAsset asset);
```

## Integrated/Resource/IAssetAdapter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/IAssetAdapter.cs)

### ObFramework.Integrated.Resource.IAssetAdapter<TNativeHandle>

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/IAssetAdapter.cs#L6).

```csharp
public interface IAssetAdapter<TNativeHandle>
    AssetLoadResult<T, TNativeHandle> Read<T>(AssetKey request) where T : class;
    Task<AssetLoadResult<T, TNativeHandle>> ReadAsync<T>(AssetKey request, CancellationToken ct = default) where T : class;
    void Release(TNativeHandle handle);
```

### ObFramework.Integrated.Resource.IRawFileAdapter<TNativeHandle>

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/IAssetAdapter.cs#L13).

```csharp
public interface IRawFileAdapter<TNativeHandle>
    Task<AssetLoadResult<byte[], TNativeHandle>> ReadRawFileAsync(string location);
```

## Integrated/Resource/ResourcePlugin.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/ResourcePlugin.cs)

### ObFramework.Integrated.Resource.ResourcePlugin

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/ResourcePlugin.cs#L9).

```csharp
public sealed class ResourcePlugin : IPlugin
    public static ResourcePlugin Create<TNativeHandle>(IAssetAdapter<TNativeHandle> adapter);
    public IHandle LoadAsset<T>(string location) where T : class;
    public Task<IHandle> LoadAssetAsync<T>(string location, CancellationToken ct = default) where T : class;
    public Task<RawFileHandle> LoadRawFileAsync(string location, CancellationToken ct = default);
```
