# Framework / Integrated/GAS/GameplayEffect

[API index](../../README.md)

## Integrated/GAS/GameplayEffect/ActiveEffect.cs

`Framework/Integrated/GAS/GameplayEffect/ActiveEffect.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.ActiveEffect

Declared at `Framework/Integrated/GAS/GameplayEffect/ActiveEffect.cs:6` (source checkout).

```csharp
public sealed class ActiveEffect
    public GameplayEffect Definition { get; }
    public EffectContext Context { get; }
    public int Stacks { get; internal set; };
    public int RemainingTicks { get; internal set; }
    public int RemainingPeriodTicks { get; internal set; }
```

## Integrated/GAS/GameplayEffect/EEffectDurationType.cs

`Framework/Integrated/GAS/GameplayEffect/EEffectDurationType.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.EEffectDurationType

Declared at `Framework/Integrated/GAS/GameplayEffect/EEffectDurationType.cs:3` (source checkout).

```csharp
public enum EEffectDurationType
    Instant,
    Duration,
    Infinite,
```

## Integrated/GAS/GameplayEffect/EffectContext.cs

`Framework/Integrated/GAS/GameplayEffect/EffectContext.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.EffectContext

Declared at `Framework/Integrated/GAS/GameplayEffect/EffectContext.cs:6` (source checkout).

```csharp
public readonly struct EffectContext
    public AbilitySystem Source { get; }
    public AbilitySystem Target { get; }
    public EffectContext(AbilitySystem source, AbilitySystem target);
```

## Integrated/GAS/GameplayEffect/EffectManager.cs

`Framework/Integrated/GAS/GameplayEffect/EffectManager.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.EffectManager

Declared at `Framework/Integrated/GAS/GameplayEffect/EffectManager.cs:6` (source checkout).

```csharp
public sealed class EffectManager
    public EffectManager(AbilitySystem abilitySystem);
    public bool TryApply(GameplayEffect definition, AbilitySystem source, out ActiveEffect? effect);
    public bool Remove(ActiveEffect effect);
    public void Tick();
    public bool CanApply(GameplayEffect definition, AbilitySystem source);
    public void Clear();
```

## Integrated/GAS/GameplayEffect/GameplayEffect.cs

`Framework/Integrated/GAS/GameplayEffect/GameplayEffect.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.GameplayEffect

Declared at `Framework/Integrated/GAS/GameplayEffect/GameplayEffect.cs:10` (source checkout).

```csharp
public sealed class GameplayEffect
    public EEffectDurationType DurationType { get; }
    public int DurationTicks { get; }
    public int PeriodTicks { get; }
    public bool ExecuteOnApplication { get; }
    public int MaxStacks { get; }
    public GameplayEffect(EEffectDurationType durationType, AEffectComponent[] components, int durationTicks = 0, int periodTicks = 0, bool executeOnApplication = false, int maxStacks = 0, Tag[]? effectTags = null);
    public void OnStackChanged(EffectContext context, int previous, int now);
    public bool HasTag(Tag tag);
```
