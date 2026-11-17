# Configuration catalogs

[API](../api/Integrated/Config.md) · [Manager source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Config/ConfigManager.cs)

`ConfigPlugin` depends on ResourcePlugin and loads its catalog during plugin initialization. A normal catalog maps each `IConfig` reference type to one JSON-array raw resource; `Id` indexes records. Duplicate IDs or duplicate types fail loading. The plugin publishes the new map only after all entries load and cancellation is checked; a failed reload leaves the previously published map intact.

```csharp
using System.Collections.Generic;
using ObFramework.Integrated.Config;

public sealed class ItemConfig : IConfig
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}
public sealed class GameCatalog : IConfigCatalog
{
    public IReadOnlyList<ConfigEntry> Entries { get; } = new[]
    {
        new ConfigEntry(typeof(ItemConfig), "Config/Items.json")
    };
}
// Plugin list: ResourcePlugin.Create(adapter), new ConfigPlugin(new GameCatalog())
// Items.json payload: [{"Id":1,"Name":"Potion"}]
// Running plugin: config.Get<ItemConfig>(1), config.Get<ItemConfig>()
```

`Get<T>(id)` returns null for an absent type/ID; `Get<T>()` returns an empty list for an absent type and otherwise a newly built list containing shared records. Callers should treat records as configuration, not mutable business state. `LoadAllAsync(catalog, ct)` explicitly reloads a catalog. Loaded raw handles are disposed after parsing.

An `ILubanConfigCatalog` also creates typed Luban tables from named JSON arrays and provides their config map. `GetTables<T>()` requires the published tables object to match `T`. Catalog generation and JSON output are CLI build-time work; see [CLI](../../ObFramework.Cli/README.md). Runtime loading uses the supplied catalog rather than scanning generated assemblies.

Shutdown waits for a current load and clears tables/maps.
