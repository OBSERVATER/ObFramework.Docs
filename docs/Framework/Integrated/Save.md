# Save slots and migration

[API](../api/Integrated/Save.md) · [Manager source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/SaveManager.cs)

Construct `SavePlugin(new FileSaveStore(applicationDataDirectory), version: 1)`, include it in the plugin list, then call `Bind(architecture)` after Architecture initialization. Bind discovers registered models implementing `ICanSave`; their `Key` values must be unique. Save/load operations require that binding and an active Architecture. `UnbindAsync` drains an active save/load and clears references. Bind again explicitly after changing the model set.

An `ICanSave` model exposes `Key`, `SaveDataType`, `Capture()` and `Restore(ASaveData)`. [Save generation](../../ObFramework.Generator/Integrated/Save/save-generation.md) can generate this contract from attributed partial model classes. Captured DTOs must satisfy MessagePack serialization requirements.

```csharp
using System.Threading.Tasks;
using ObFramework.Core;
using ObFramework.Integrated.Save;

public static class SaveExample
{
    public static async Task WriteAsync(SavePlugin saves, IArchitecture architecture)
    {
        saves.Bind(architecture); // plugin and Architecture must already be initialized
        try { await saves.SaveAsync("slot-01"); }
        finally { await saves.UnbindAsync(); }
    }
}
```

FileSaveStore writes `<applicationDataDirectory>/Saves/<slotId>.sav` using a temporary file and replacement/move. Choose application-controlled slot IDs; the current store directly combines the supplied ID with the save directory. There is no automatic encryption or cloud synchronization.

Slots contain a schema version and key-to-bytes map. Loading rejects future versions. For an older slot, the migration dictionary must have a migration for every version step; each `ISaveMigration.Migrate` transforms the stored map before deserialization. Unknown saved model keys are skipped. The manager deserializes all matched records before restoring models; user-defined Restore methods can still partially change state if one throws.

Save/load calls are serialized with a semaphore. The current facade exposes no per-call cancellation token. SavePlugin borrows its store; store lifetime belongs to the application. Stop business save work and unbind before Architecture teardown.
