# Framework / Integrated/Config

[API index](../README.md)

## Integrated/Config/ConfigEntry.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/ConfigEntry.cs)

### ObFramework.Integrated.Config.ConfigEntry

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/ConfigEntry.cs#L5).

```csharp
public readonly struct ConfigEntry
    public Type Type { get; }
    public string Location { get; }
    public string? Name { get; }
    public ConfigEntry(Type type, string location, string? name = null);
```

## Integrated/Config/ConfigPlugin.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/ConfigPlugin.cs)

### ObFramework.Integrated.Config.ConfigPlugin

Declared at [line 10](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/ConfigPlugin.cs#L10).

```csharp
public sealed class ConfigPlugin : IPlugin
    public ConfigPlugin(IConfigCatalog catalog);
    public Task LoadAllAsync(IConfigCatalog catalog, CancellationToken ct = default);
    public T GetTables<T>() where T : class;
    public IReadOnlyList<T> Get<T>() where T : class, IConfig;
    public T? Get<T>(int id) where T : class, IConfig;
```

## Integrated/Config/IConfig.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/IConfig.cs)

### ObFramework.Integrated.Config.IConfig

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/IConfig.cs#L3).

```csharp
public interface IConfig
    int Id { get; }
```

## Integrated/Config/IConfigCatalog.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/IConfigCatalog.cs)

### ObFramework.Integrated.Config.IConfigCatalog

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/IConfigCatalog.cs#L5).

```csharp
public interface IConfigCatalog
    IReadOnlyList<ConfigEntry> Entries { get; }
```

## Integrated/Config/ILubanConfigCatalog.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/ILubanConfigCatalog.cs)

### ObFramework.Integrated.Config.ILubanConfigCatalog

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/ILubanConfigCatalog.cs#L7).

```csharp
public interface ILubanConfigCatalog : IConfigCatalog
    object CreateTables(Func<string, JArray> loader);
    IReadOnlyDictionary<Type, IReadOnlyDictionary<int, IConfig>> CreateConfigMap(object tables);
```
