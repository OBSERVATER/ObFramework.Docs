# Gameplay Ability System

Use one `AbilitySystem` per combatant. It owns the combatant's attributes, granted abilities, active effects, tags and cue subscriptions. The host's `GASPlugin` provides shared definitions and ticks registered combatants through `ClockPlugin`.

## Cast the starter attack

Generate the starter workbooks' C# and JSON as described in [GAS Excel starter](../../ObFramework.Cli/Commands/Table.md#gas-excel-starter). Compile the generated classes into your game and expose the JSON through its resource adapter. Create and start the host with the game's resource adapter:

```csharp
using ObFramework.Core;
using ObFramework.Integrated.Clock;
using ObFramework.Integrated.Config;
using ObFramework.Integrated.GAS;
using ObFramework.Integrated.Resource;

// adapter is the game's asset adapter with raw-file loading support.
var host = new PluginHost(
    ResourcePlugin.Create(adapter),
    new ClockPlugin(0.1f),
    new ConfigPlugin(new GasConfigCatalog()),
    new GASPlugin(loadConfig: true, attributes: CombatAttributes.All));
await host.StartAsync();
```

After startup, the following example grants the starter attack to a caster and targets a second combatant:

```csharp
using ObFramework.Extension.Integrated;
using ObFramework.Integrated.GAS;
using ObFramework.Integrated.GAS.Attributes.Core;

// host is the application's started PluginHost.
var gas = host.Context.Get<GASPlugin>();
using var caster = new AbilitySystem();
using var target = new AbilitySystem();
caster.Attributes.Register(CombatAttributes.Mana, new Attribute<FormulaFloat, float>(30f, default));
target.Attributes.Register(CombatAttributes.Health, new Attribute<FormulaFloat, float>(100f, default));
gas.Register(caster);
gas.Register(target);
try
{
    var attack = gas.GetAbility(2001);
    caster.Abilities.Grant(attack);
    bool activated = caster.TryActivateAbility(attack, target, out _);
    float mana = caster.GetValue(CombatAttributes.Mana);
    float health = target.GetValue(CombatAttributes.Health);
    // With the starter data: activated == true, mana == 20, health == 80.
}
finally
{
    gas.UnRegister(target);
    gas.UnRegister(caster);
}
```

Register each actor's initial values using the generated keys. Passing those keys to the plugin lets it resolve attribute names when loading the effect definitions. An actor with insufficient Mana fails the starter attack's commit cost. Check the activation result before reporting a successful cast.

## Ticks, cues and actor lifetime

Keep each system registered for its actor's lifetime so duration effects, periodic effects and active abilities advance. On each logic tick the plugin updates effects across registered actors before updating abilities. In engine gameplay the adapter advances the clock.

Subscribe to the affected actor's `Cues.Emitted` event to drive presentation for effects that emit cues. Unregister an actor when it leaves play. For pooled actors, `Reset()` clears abilities, effects, tags and cue subscriptions while retaining registered attributes. Restore the desired attribute values before reuse. `Dispose()` also clears the attribute set and ends the system's lifetime.
