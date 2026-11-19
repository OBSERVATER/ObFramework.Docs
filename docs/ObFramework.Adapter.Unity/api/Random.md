# ObFramework.Adapter.Unity / Random

[API index](README.md)

## Random/UnityRandomSource.cs

`ObFramework.Adapter.Unity/Random/UnityRandomSource.cs` (source checkout)

### ObFramework.Adapter.Unity.Random.UnityRandomSource

Declared at `ObFramework.Adapter.Unity/Random/UnityRandomSource.cs:8` (source checkout).

```csharp
public class UnityRandomSource : ARandomSource
    public UnityRandomSource(int seed);
    public override int Range(int minInclude, int maxInclude);
    public override float Range(float minInclude, float maxInclude);
    public override float RandomValue01 { get; }
```
