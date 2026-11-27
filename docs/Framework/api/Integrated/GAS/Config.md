# Framework / Integrated/GAS/Config

[API index](../../README.md)

## Integrated/GAS/Config/AbilityConfig.cs

`Framework/Integrated/GAS/Config/AbilityConfig.cs` (source checkout)

### ObFramework.Integrated.GAS.Config.AbilityConfig

Declared at `Framework/Integrated/GAS/Config/AbilityConfig.cs:8` (source checkout).

```csharp
public sealed class AbilityConfig : IConfig
    [JsonProperty(Required = Required.Always)] public int Id { get; set; }
    public string Name { get; set; };
    [JsonProperty(Required = Required.Always)] public string Behavior { get; set; };
    public JObject Parameters { get; set; };
    public int? CommitEffectId { get; set; }
    public string[] AbilityTags { get; set; };
    public string[] RequiredTags { get; set; };
    public string[] BlockedTags { get; set; };
    public string[] BlockAbilitiesWithTags { get; set; };
```

## Integrated/GAS/Config/EffectConfig.cs

`Framework/Integrated/GAS/Config/EffectConfig.cs` (source checkout)

### ObFramework.Integrated.GAS.Config.EffectConfig

Declared at `Framework/Integrated/GAS/Config/EffectConfig.cs:9` (source checkout).

```csharp
public sealed class EffectConfig : IConfig
    [JsonProperty(Required = Required.Always)] public int Id { get; set; }
    public string Name { get; set; };
    [JsonProperty(Required = Required.Always)] public EEffectDurationType DurationType { get; set; }
    public int DurationTicks { get; set; }
    public int PeriodTicks { get; set; }
    public bool ExecuteOnApplication { get; set; }
    public int MaxStacks { get; set; }
    public string[] EffectTags { get; set; };
    [JsonProperty(Required = Required.Always)] public JObject[] Components { get; set; };
```

## Integrated/GAS/Config/GASConfigLibrary.cs

`Framework/Integrated/GAS/Config/GASConfigLibrary.cs` (source checkout)

### ObFramework.Integrated.GAS.Config.GASConfigLibrary

Declared at `Framework/Integrated/GAS/Config/GASConfigLibrary.cs:15` (source checkout).

```csharp
public sealed class GASConfigLibrary
    public GASConfigLibrary();
    public void RegisterAttribute(AttributeKey<float> key);
    public AttributeKey<float> GetAttribute(string name);
    public void RegisterComponent(string type, Func<JObject, AEffectComponent> factory);
    public void RegisterBehavior(string type, BehaviorBuilder builder);
    public void Load(IEnumerable<EffectConfig> effects, IEnumerable<AbilityConfig> abilities);
    public Effect GetEffect(int id);
    public Ability GetAbility(int id);
```

### ObFramework.Integrated.GAS.Config.GASConfigLibrary.BehaviorBuilder

Declared at `Framework/Integrated/GAS/Config/GASConfigLibrary.cs:17` (source checkout).

```csharp
public delegate Func<AAbilityBehaviour> BehaviorBuilder(JObject parameters, Func<int, Effect> getEffect);
```
