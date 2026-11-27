# Framework / Integrated/GAS/Attributes

[API index](../../README.md)

## Integrated/GAS/Attributes/Gameplay.Attributes.Contract.cs

`Framework/Integrated/GAS/Attributes/Gameplay.Attributes.Contract.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.OpCode

Declared at `Framework/Integrated/GAS/Attributes/Gameplay.Attributes.Contract.cs:8` (source checkout).

```csharp
public enum OpCode : byte
    LoadBase = 0,
    LoadSlot = 1,
    LoadConstant = 2,
    Add = 3,
    Multiply = 4,
    Subtract = 5,
    Divide = 6,
    Min = 7,
    Max = 8,
    Clamp = 9,
    Negate = 10,
```

### ObFramework.Integrated.GAS.Attributes.IFormula<T>

Declared at `Framework/Integrated/GAS/Attributes/Gameplay.Attributes.Contract.cs:36` (source checkout).

```csharp
public interface IFormula<T> where T : struct
    int SlotCount { get; }
    T Calculate(T baseValue, ReadOnlySpan<T> slots);
```
