# Framework / Integrated/GAS/GameplayEffect/Components

[API index](../../../README.md)

## Integrated/GAS/GameplayEffect/Components/AEffectComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/AEffectComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.AEffectComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/AEffectComponent.cs:3` (source checkout).

```csharp
public abstract class AEffectComponent
    public virtual void Validate(GameplayEffect effect);
    public virtual void OnApply(EffectContext context);
    public virtual void OnRemove(EffectContext context);
    public virtual void OnPeriodic(EffectContext context, int stacks);
    public virtual void OnStackChanged(EffectContext context, int previous, int current);
    public virtual bool CanApply(EffectContext context);
    public virtual bool BlockIncoming(GameplayEffect eff, EffectContext incomingContext);
```

## Integrated/GAS/GameplayEffect/Components/BaseComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/BaseComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.BaseComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/BaseComponent.cs:6` (source checkout).

```csharp
public sealed class BaseComponent : AEffectComponent
    public BaseComponent(AttributeKey<float> key, float value, bool periodic = false);
    public override void Validate(GameplayEffect effect);
    public override void OnApply(EffectContext context);
    public override void OnPeriodic(EffectContext context, int stacks);
```

## Integrated/GAS/GameplayEffect/Components/CancelAbilityComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/CancelAbilityComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.CancelAbilityComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/CancelAbilityComponent.cs:8` (source checkout).

```csharp
public sealed class CancelAbilityComponent : AEffectComponent
    public CancelAbilityComponent(Tag tag);
    public override void OnApply(EffectContext context);
```

## Integrated/GAS/GameplayEffect/Components/CostComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/CostComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.CostComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/CostComponent.cs:9` (source checkout).

```csharp
public sealed class CostComponent : AEffectComponent
    public CostComponent(AttributeKey<float> key, float cost);
    public override bool CanApply(EffectContext context);
    public override void OnApply(EffectContext context);
```

## Integrated/GAS/GameplayEffect/Components/CueComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/CueComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.CueComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/CueComponent.cs:6` (source checkout).

```csharp
public sealed class CueComponent : AEffectComponent
    public CueComponent(Tag tag, ECueTrigger trigger = ECueTrigger.OnApply);
    public override void Validate(GameplayEffect effect);
    public override void OnApply(EffectContext context);
    public override void OnPeriodic(EffectContext context, int stacks);
    public override void OnRemove(EffectContext context);
```

## Integrated/GAS/GameplayEffect/Components/ECueTrigger.cs

`Framework/Integrated/GAS/GameplayEffect/Components/ECueTrigger.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.ECueTrigger

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/ECueTrigger.cs:3` (source checkout).

```csharp
public enum ECueTrigger
    OnApply,
    Periodic,
    Persistent,
```

## Integrated/GAS/GameplayEffect/Components/ImmunityComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/ImmunityComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.ImmunityComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/ImmunityComponent.cs:9` (source checkout).

```csharp
public sealed class ImmunityComponent : AEffectComponent
    public ImmunityComponent(Tag blockedTag);
    public override void Validate(GameplayEffect effect);
    public override bool BlockIncoming(GameplayEffect incoming, EffectContext incomingContext);
```

## Integrated/GAS/GameplayEffect/Components/SlotComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/SlotComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.SlotComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/SlotComponent.cs:6` (source checkout).

```csharp
public sealed class SlotComponent : AEffectComponent
    public SlotComponent(AttributeKey<float> key, int slot, float value);
    public override void Validate(GameplayEffect effect);
    public override void OnStackChanged(EffectContext context, int previous, int current);
```

## Integrated/GAS/GameplayEffect/Components/TagComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/TagComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.TagComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/TagComponent.cs:6` (source checkout).

```csharp
public sealed class TagComponent : AEffectComponent
    public TagComponent(Tag tag);
    public override void Validate(GameplayEffect effect);
    public override void OnApply(EffectContext context);
    public override void OnRemove(EffectContext context);
```

## Integrated/GAS/GameplayEffect/Components/TagRequirementComponent.cs

`Framework/Integrated/GAS/GameplayEffect/Components/TagRequirementComponent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayEffect.Components.TagRequirementComponent

Declared at `Framework/Integrated/GAS/GameplayEffect/Components/TagRequirementComponent.cs:9` (source checkout).

```csharp
public sealed class TagRequirementComponent : AEffectComponent
    public TagRequirementComponent(Tag[]? required = null, Tag[]? blocked = null);
    public override bool CanApply(EffectContext context);
```
