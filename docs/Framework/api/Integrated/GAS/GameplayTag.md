# Framework / Integrated/GAS/GameplayTag

[API index](../../README.md)

## Integrated/GAS/GameplayTag/GameplayTag.cs

`Framework/Integrated/GAS/GameplayTag/GameplayTag.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayTag.GameplayTag

Declared at `Framework/Integrated/GAS/GameplayTag/GameplayTag.cs:5` (source checkout).

```csharp
public readonly struct GameplayTag : IEquatable<GameplayTag>
    public string Name { get; }
    public GameplayTag(string name);
    public bool IsValid { get; };
    public bool Matches(GameplayTag other);
    public bool Equals(GameplayTag other);
    public override bool Equals(object? obj);
    public override int GetHashCode();
    public override string ToString();
```

## Integrated/GAS/GameplayTag/TagContainer.cs

`Framework/Integrated/GAS/GameplayTag/TagContainer.cs` (source checkout)

### ObFramework.Integrated.GAS.GameplayTag.TagContainer

Declared at `Framework/Integrated/GAS/GameplayTag/TagContainer.cs:6` (source checkout).

```csharp
public sealed class TagContainer
    public void Add(GameplayTag tag);
    public bool Remove(GameplayTag tag);
    public int GetCount(GameplayTag tag);
    public bool HasExact(GameplayTag tag);
    public bool Has(GameplayTag tag);
    public bool HasAny(IEnumerable<GameplayTag> tags);
    public bool HasAll(IEnumerable<GameplayTag> tags);
    public void Clear();
```
