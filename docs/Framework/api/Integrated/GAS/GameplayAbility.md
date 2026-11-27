# Framework / Integrated/GAS/GameplayAbility

[API index](../../README.md)

## Integrated/GAS/GameplayAbility/AAbilityBehaviour.cs

`Framework/Integrated/GAS/GameplayAbility/AAbilityBehaviour.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayAbility.AAbilityBehaviour

Declared at `Framework/Integrated/GAS/GameplayAbility/AAbilityBehaviour.cs:3` (source checkout).

```csharp
public abstract class AAbilityBehaviour
    public virtual bool CanActivate(AbilitySystem owner, AbilitySystem target);
    public abstract void OnActivate(ActiveAbility ability);
    public virtual void OnTick(ActiveAbility ability);
    public virtual void OnEnd(ActiveAbility ability, bool cancelled);
```

## Integrated/GAS/GameplayAbility/AbilityManager.cs

`Framework/Integrated/GAS/GameplayAbility/AbilityManager.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayAbility.AbilityManager

Declared at `Framework/Integrated/GAS/GameplayAbility/AbilityManager.cs:7` (source checkout).

```csharp
public sealed class AbilityManager
    public AbilityManager(AbilitySystem owner);
    public bool Grant(GameplayAbility definition);
    public bool Revoke(GameplayAbility definition);
    public bool Has(GameplayAbility definition);
    public bool IsActive(GameplayAbility definition);
    public bool TryActivate(GameplayAbility definition, AbilitySystem target, out ActiveAbility? active);
    public void CancelByTag(Tag tag);
    public void Tick();
```

## Integrated/GAS/GameplayAbility/ActiveAbility.cs

`Framework/Integrated/GAS/GameplayAbility/ActiveAbility.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayAbility.ActiveAbility

Declared at `Framework/Integrated/GAS/GameplayAbility/ActiveAbility.cs:6` (source checkout).

```csharp
public sealed class ActiveAbility
    public GameplayAbility Definition { get; }
    public AbilitySystem Owner { get; }
    public AbilitySystem Target { get; }
    public long ElapsedTicks { get; internal set; }
    public bool IsCommitted { get; private set; }
    public bool IsActive { get; internal set; };
    public bool TryCommit();
    public void End();
    public void Cancel();
```

## Integrated/GAS/GameplayAbility/GameplayAbility.cs

`Framework/Integrated/GAS/GameplayAbility/GameplayAbility.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayAbility.GameplayAbility

Declared at `Framework/Integrated/GAS/GameplayAbility/GameplayAbility.cs:10` (source checkout).

```csharp
public sealed class GameplayAbility
    public Effect? CommitEffect { get; }
    public GameplayAbility(Func<AAbilityBehaviour> factory, Effect? commitEffect = null, Tag[]? abilityTags = null, Tag[]? requiredTags = null, Tag[]? blockedTags = null, Tag[]? blockAbilitiesWithTags = null);
    public bool HasTag(Tag tag);
```
