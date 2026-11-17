# Framework / Integrated/Resource/Handle

[API index](../../README.md)

## Integrated/Resource/Handle/AssetHandle.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/Handle/AssetHandle.cs)

### ObFramework.Integrated.Resource.Handle.AssetHandle

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/Handle/AssetHandle.cs#L3).

```csharp
public class AssetHandle : AHandle
    public override T? Get<T>() where T : class;
    protected override void Release();
```

## Integrated/Resource/Handle/IHandle.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/Handle/IHandle.cs)

### ObFramework.Integrated.Resource.Handle.IHandle

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/Handle/IHandle.cs#L6).

```csharp
public interface IHandle : IDisposable
    string Location { get; }
    T? Get<T>() where T : class;
```

### ObFramework.Integrated.Resource.Handle.AHandle

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/Handle/IHandle.cs#L11).

```csharp
public abstract class AHandle : IHandle
    public string Location { get; protected set; }
    protected int _disposed;
    protected void ThrowIfDisposed();
    public void Dispose();
    public abstract T? Get<T>() where T : class;
    protected abstract void Release();
```

## Integrated/Resource/Handle/RawFileHandle.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/Handle/RawFileHandle.cs)

### ObFramework.Integrated.Resource.Handle.RawFileHandle

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Resource/Handle/RawFileHandle.cs#L6).

```csharp
public class RawFileHandle : AHandle
    public override T? Get<T>() where T : class;
    protected override void Release();
```
