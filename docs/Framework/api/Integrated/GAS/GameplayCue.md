# Framework / Integrated/GAS/GameplayCue

[API index](../../README.md)

## Integrated/GAS/GameplayCue/CueDispatcher.cs

`Framework/Integrated/GAS/GameplayCue/CueDispatcher.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayCue.CueDispatcher

Declared at `Framework/Integrated/GAS/GameplayCue/CueDispatcher.cs:7` (source checkout).

```csharp
public sealed class CueDispatcher
    public event Action<CueEvent>? Emitted;
    public CueDispatcher(AbilitySystem owner);
    public void Execute(Tag tag, AbilitySystem source, float? magnitude = null);
```

## Integrated/GAS/GameplayCue/CueEvent.cs

`Framework/Integrated/GAS/GameplayCue/CueEvent.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayCue.CueEvent

Declared at `Framework/Integrated/GAS/GameplayCue/CueEvent.cs:5` (source checkout).

```csharp
public readonly struct CueEvent
    public Tag Tag { get; }
    public ECueEventType Type { get; }
    public AbilitySystem Source { get; }
    public AbilitySystem Target { get; }
    public float? Magnitude { get; }
    public CueEvent(Tag tag, ECueEventType type, AbilitySystem source, AbilitySystem target, float? magnitude = null);
```

## Integrated/GAS/GameplayCue/ECueEventType.cs

`Framework/Integrated/GAS/GameplayCue/ECueEventType.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayCue.ECueEventType

Declared at `Framework/Integrated/GAS/GameplayCue/ECueEventType.cs:3` (source checkout).

```csharp
public enum ECueEventType
    Executed,
    Added,
    Removed,
```
