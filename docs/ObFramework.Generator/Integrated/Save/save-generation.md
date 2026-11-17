# Save generation

`[CanSave("key")]` marks a class for an `ICanSave` implementation. `[SaveField]` selects its directly declared fields for a MessagePack snapshot. Both attributes come from `ObFramework.Integrated.Save` in the runtime assembly.

## Current supported example

Use a public, top-level, non-generic partial class with writable instance fields. This complete example can be a model registered with an Architecture:

```csharp
using ObFramework.Core;
using ObFramework.Integrated.Save;

namespace Example.Models
{
    public interface IPlayerModel : IModel, ICanSave
    {
        int Level { get; set; }
    }

    [CanSave("player")]
    public partial class PlayerModel : AModel, IPlayerModel
    {
        [SaveField] private int _level = 1;

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        protected override void OnInit() { }
    }
}
```

The generator adds the following contract to `PlayerModel`:

| Member | Meaning |
|---|---|
| `string Key` | `"player"`, the identity used to pair save bytes with a model |
| `Type SaveDataType` | The nested `PlayerModelSaveData` DTO type |
| `ASaveData Capture()` | Copies each selected field into a new DTO |
| `void Restore(ASaveData value)` | Casts to that DTO and copies its fields back |

```csharp
var model = new Example.Models.PlayerModel();
model.Level = 12;
var snapshot = model.Capture();
model.Level = 1;
model.Restore(snapshot);
System.Console.WriteLine(model.Level); // 12
```

This exercise checks the object snapshot contract. Application persistence additionally requires an initialized `SavePlugin`, a registered model, and a bind to the initialized Architecture. See the [runtime project guide](../../../Framework/README.md) and [SavePlugin source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/SavePlugin.cs) for that lifecycle.

## Serialized shape and compatibility

The generated nested DTO derives from `ASaveData`, carries `[MessagePackObject]`, and has a public field for each selected field. Each member uses its original source field name as the MessagePack string key: `[SaveField] private int _level;` becomes `[Key("_level")] public int _level;` in the DTO. `[SaveField]` takes no numeric key argument.

Changing a selected field name changes its serialized key. Changing the CanSave key changes how a model is located in a save slot. Treat either change as a persistence schema change and supply the application's required migration. `Capture` copies field values; it is a shallow copy for referenced objects. The emitter does not clone referenced data or validate that the selected types are serializable.

Only fields declared directly on the attributed type are collected. Properties and inherited fields are not collected by `SaveGenerator`. `Restore` expects the precise generated DTO type and performs a cast; it does not validate or convert another snapshot type.

## Emitter limits

Unlike CDE generation, the current Save generator has no dedicated OBF diagnostic descriptors. Invalid input can surface as ordinary compiler errors or generator failures. Keep these implementation limits in view:

- The emitted declaration is `public partial class <Name>` and does not reproduce containing types or generic parameters. Use the supported type shape shown above.
- The emitter assigns selected fields during restore. Use writable instance fields; readonly, const and static fields are not a supported save model shape.
- The emitted file hint is `<ClassName>.Save.g.cs`, without a namespace. Two attributed classes with the same simple name can collide even when they have different namespaces.
- Keys are embedded directly into generated string source. Prefer simple stable keys without quotes, backslashes or control characters.
- The source type must leave `Key`, `SaveDataType`, `Capture`, `Restore` and `<ClassName>SaveData` available for generation.

These constraints describe current source behavior; they are not a promise of explicit diagnostic coverage.

## Related guidance

Implementation: [SaveGenerator.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/Save/SaveGenerator.cs), [SaveEmitter.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Generator/Integrated/Save/SaveEmitter.cs). The runtime contract is [ICanSave.cs](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Integrated/Save/ICanSave.cs).

Compile the generated example in the consuming project and verify persistence round-trips before changing a save schema. Confirm generated keys and MessagePack DTO fields against existing save data.
