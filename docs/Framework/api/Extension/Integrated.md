# Framework / Extension/Integrated

[API index](../README.md)

## Extension/Integrated/ClockExtension.cs

`Framework/Extension/Integrated/ClockExtension.cs` (source checkout)

### ObFramework.Extension.Integrated.ClockExtension

Declared at `Framework/Extension/Integrated/ClockExtension.cs:7` (source checkout).

```csharp
public static class ClockExtension
    public static void Register(this IFrameTickable tickable, ICanGetPlugin self);
    public static void UnRegister(this IFrameTickable tickable, ICanGetPlugin self);
    public static float Elapsed(this ClockPlugin clock, int startTick);
    public static float Remaining(this ClockPlugin clock, int endTick);
    public static int ToTicks(this ClockPlugin clock, float seconds);
```

## Extension/Integrated/DiagnosticsExtension.cs

`Framework/Extension/Integrated/DiagnosticsExtension.cs` (source checkout)

### ObFramework.Extension.Integrated.DiagnosticsExtension

Declared at `Framework/Extension/Integrated/DiagnosticsExtension.cs:5` (source checkout).

```csharp
public static class DiagnosticsExtension
    public static void I(this ILogger self, string content);
    public static void W(this ILogger self, string content);
    public static void E(this ILogger self, string content);
    public static void D(this ILogger self, string content);
```

## Extension/Integrated/GASExtension.cs

`Framework/Extension/Integrated/GASExtension.cs` (source checkout)

### ObFramework.Extension.Integrated.GASExtension

Declared at `Framework/Extension/Integrated/GASExtension.cs:11` (source checkout).

```csharp
public static class GASExtension
    public static T GetValue<T>(this AbilitySystem gas, AttributeKey<T> key) where T : struct;
    public static T GetBase<T>(this AbilitySystem gas, AttributeKey<T> key) where T : struct;
    public static void SetBase<T>(this AbilitySystem gas, AttributeKey<T> key, T value) where T : struct;
    public static bool TryApplyEffect(this AbilitySystem source, AbilitySystem target, Effect definition, out ActiveEffect? effect);
    public static bool TryApplyEffect(this AbilitySystem gas, Effect definition, out ActiveEffect? effect);
    public static bool TryActivateAbility(this AbilitySystem gas, Ability definition, AbilitySystem target, out ActiveAbility? active);
    public static bool TryActivateAbility(this AbilitySystem gas, Ability definition, out ActiveAbility? active);
```

## Extension/Integrated/PoolingExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/PoolingExtension.cs)

### ObFramework.Extension.Integrated.PoolingExtension

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/Integrated/PoolingExtension.cs#L6).

```csharp
public static class PoolingExtension
    public static void ReleaseToPool<T>(this List<T> list);
    public static void ReleaseToPool<TK, TV>(this Dictionary<TK, TV> dict);
```
