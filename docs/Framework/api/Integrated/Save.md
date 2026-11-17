# Framework / Integrated/Save

[API index](../README.md)

## Integrated/Save/ASaveData.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/ASaveData.cs)

### ObFramework.Integrated.Save.ASaveData

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/ASaveData.cs#L3).

```csharp
public abstract class ASaveData
```

## Integrated/Save/CanSaveAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/CanSaveAttribute.cs)

### ObFramework.Integrated.Save.CanSaveAttribute

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/CanSaveAttribute.cs#L5).

```csharp
[System.AttributeUsage(AttributeTargets.Class, Inherited = false)] public sealed class CanSaveAttribute : Attribute
    public CanSaveAttribute(string key);
```

## Integrated/Save/FileSaveStore.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/FileSaveStore.cs)

### ObFramework.Integrated.Save.ISaveStore

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/FileSaveStore.cs#L7).

```csharp
public interface ISaveStore
    Task WriteAsync(byte[] data, string slotId);
    Task<byte[]> TryReadAsync(string slotId);
    bool Exists(string slotId);
    void Delete(string slotId);
```

### ObFramework.Integrated.Save.FileSaveStore

Declared at [line 14](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/FileSaveStore.cs#L14).

```csharp
public class FileSaveStore : ISaveStore
    public FileSaveStore(string path);
    public void Delete(string slotId);
    public bool Exists(string slotId);
    public async Task<byte[]> TryReadAsync(string slotId);
    public async Task WriteAsync(byte[] data, string slotId);
```

## Integrated/Save/ICanSave.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/ICanSave.cs)

### ObFramework.Integrated.Save.ICanSave

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/ICanSave.cs#L5).

```csharp
public interface ICanSave
    string Key { get; }
    Type SaveDataType { get; }
    ASaveData Capture();
    void Restore(ASaveData value);
```

## Integrated/Save/ISaveMigration.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/ISaveMigration.cs)

### ObFramework.Integrated.Save.ISaveMigration

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/ISaveMigration.cs#L8).

```csharp
public interface ISaveMigration
    int FromVersion { get; }
    void Migrate(Dictionary<string, byte[]> entries);
```

## Integrated/Save/SaveFieldAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/SaveFieldAttribute.cs)

### ObFramework.Integrated.Save.SaveFieldAttribute

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/SaveFieldAttribute.cs#L5).

```csharp
[System.AttributeUsage(AttributeTargets.Field, Inherited = false)] public sealed class SaveFieldAttribute : Attribute
```

## Integrated/Save/SavePlugin.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/SavePlugin.cs)

### ObFramework.Integrated.Save.SavePlugin

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/SavePlugin.cs#L9).

```csharp
public sealed class SavePlugin : IPlugin
    public SavePlugin(ISaveStore store, int version = 1, Dictionary<int, ISaveMigration>? migrations = null);
    public void Bind(IArchitecture architecture);
    public Task UnbindAsync();
    public Task LoadAsync(string slotId);
    public Task SaveAsync(string slotId);
```
