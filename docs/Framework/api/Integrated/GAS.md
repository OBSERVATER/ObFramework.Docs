# Framework / Integrated/GAS

[API index](../README.md)

## Integrated/GAS/AbilitySystem.cs

`Framework/Integrated/GAS/AbilitySystem.cs` (source checkout)

### ObFramework.Integrated.GAS.AbilitySystem

Declared at `Framework/Integrated/GAS/AbilitySystem.cs:10` (source checkout).

```csharp
public sealed class AbilitySystem : IDisposable
    public AttributeSet Attributes { get; };
    public TagContainer Tags { get; };
    public EffectManager Effects { get; }
    public AbilityManager Abilities { get; }
    public CueDispatcher Cues { get; }
    public bool IsDisposed { get; private set; }
    public AbilitySystem();
    public void Reset();
    public void Dispose();
```

## Integrated/GAS/GASPlugin.cs

`Framework/Integrated/GAS/GASPlugin.cs` (source checkout)

### ObFramework.Integrated.GAS.GASPlugin

Declared at `Framework/Integrated/GAS/GASPlugin.cs:17` (source checkout).

```csharp
public sealed class GASPlugin : IPlugin, ILogicTickable
    public GASPlugin(bool loadConfig = false, IEnumerable<AttributeKey<float>>? attributes = null);
    public Task InitAsync(IPluginContext plugins, CancellationToken ct);
    public Task StopAsync(IPluginContext plugins);
    public bool Register(AbilitySystem system);
    public bool UnRegister(AbilitySystem system);
    public void LogicTick();
    public Effect GetEffect(int id);
    public Ability GetAbility(int id);
    public AttributeKey<float> GetAttribute(string name);
    public void RegisterComponent(string type, Func<JObject, AEffectComponent> factory);
    public void RegisterBehavior(string type, GASConfigLibrary.BehaviorBuilder builder);
```
