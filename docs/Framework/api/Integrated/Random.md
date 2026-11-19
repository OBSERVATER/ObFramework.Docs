# Framework / Integrated/Random

[API index](../README.md)

## Integrated/Random/DeterministicRandomStream.cs

`Framework/Integrated/Random/DeterministicRandomStream.cs` (source checkout)

### ObFramework.Integrated.Random.DeterministicRandomStream

Declared at `Framework/Integrated/Random/DeterministicRandomStream.cs:6` (source checkout).

```csharp
public sealed class DeterministicRandomStream
    public ulong NextUInt64();
    public int NextInt(int exclusiveMax);
```

## Integrated/Random/IRandomSource.cs

`Framework/Integrated/Random/IRandomSource.cs` (source checkout)

### ObFramework.Integrated.Random.IRandomSource

Declared at `Framework/Integrated/Random/IRandomSource.cs:3` (source checkout).

```csharp
public interface IRandomSource
    int Range(int minInclude, int maxInclude);
    float Range(float minInclude, float maxInclude);
    float RandomValue01 { get; }
    int Seed { get; }
```

### ObFramework.Integrated.Random.ARandomSource

Declared at `Framework/Integrated/Random/IRandomSource.cs:11` (source checkout).

```csharp
public abstract class ARandomSource : IRandomSource
    public abstract int Range(int minInclude, int maxInclude);
    public abstract float Range(float minInclude, float maxInclude);
    public abstract float RandomValue01 { get; }
    public int Seed { get; }
    protected ARandomSource(int seed);
```

## Integrated/Random/RandomPlugin.cs

`Framework/Integrated/Random/RandomPlugin.cs` (source checkout)

### ObFramework.Integrated.Random.RandomPlugin

Declared at `Framework/Integrated/Random/RandomPlugin.cs:9` (source checkout).

```csharp
public class RandomPlugin : IPlugin
    public RandomPlugin(IRandomSource source);
    public int Seed { get; };
    public static DeterministicRandomStream CreateStream(ulong seed, params ulong[] keys);
    public void Register(PluginRegistration registration);
    public Task InitAsync(IPluginContext plugins, CancellationToken ct);
    public Task StopAsync(IPluginContext plugins);
    public int Range(int minInclude, int maxInclude);
    public float Range(float minInclude, float maxInclude);
    public float Value01 { get; };
    public T GetRandomItem<T>(List<T> origin);
    public List<T> Shuffle<T>(List<T> origin);
    public T GetByWeight<T>(Dictionary<T, float>? map) where T : notnull;
```

## Integrated/Random/SystemRandomSource.cs

`Framework/Integrated/Random/SystemRandomSource.cs` (source checkout)

### ObFramework.Integrated.Random.SystemRandomSource

Declared at `Framework/Integrated/Random/SystemRandomSource.cs:3` (source checkout).

```csharp
public class SystemRandomSource : ARandomSource
    public SystemRandomSource(int seed);
    public override int Range(int minInclude, int maxInclude);
    public override float Range(float minInclude, float maxInclude);
    public override float RandomValue01 { get; };
```
