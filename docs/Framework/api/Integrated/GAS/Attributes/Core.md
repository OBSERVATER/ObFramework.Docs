# Framework / Integrated/GAS/Attributes/Core

[API index](../../../README.md)

## Integrated/GAS/Attributes/Core/Attribute.cs

`Framework/Integrated/GAS/Attributes/Core/Attribute.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Core.IAttribute<T>

Declared at `Framework/Integrated/GAS/Attributes/Core/Attribute.cs:6` (source checkout).

```csharp
public interface IAttribute<T> : IDisposable where T : struct
    T Base { get; }
    T Value { get; }
    int SlotCount { get; }
    void SetBase(T value);
    T GetSlot(int index);
    void SetSlot(int index, T value);
```

### ObFramework.Integrated.GAS.Attributes.Core.Attribute<TFormula, TValue>

Declared at `Framework/Integrated/GAS/Attributes/Core/Attribute.cs:23` (source checkout).

```csharp
public class Attribute<TFormula, TValue> : IAttribute<TValue> where TFormula : struct, IFormula<TValue> where TValue : struct
    public int SlotCount { get; };
    public Attribute(TValue @base, TFormula formula);
    public TValue Value { get; }
    public TValue Base { get; };
    public void SetBase(TValue value);
    public TValue this[int index] { get; set; }
    public TValue GetSlot(int slotIndex);
    public void SetSlot(int slotIndex, TValue value);
    public void ClearSlot(int slotIndex);
    public void ClearSlots();
    public void Dispose();
```

## Integrated/GAS/Attributes/Core/AttributeKey.cs

`Framework/Integrated/GAS/Attributes/Core/AttributeKey.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Core.AAttributeKey

Declared at `Framework/Integrated/GAS/Attributes/Core/AttributeKey.cs:3` (source checkout).

```csharp
public abstract class AAttributeKey
    public string Name { get; }
    protected AAttributeKey(string name);
```

### ObFramework.Integrated.GAS.Attributes.Core.AttributeKey<T>

Declared at `Framework/Integrated/GAS/Attributes/Core/AttributeKey.cs:13` (source checkout).

```csharp
public sealed class AttributeKey<T> : AAttributeKey where T : struct
    public AttributeKey(string name);
```

## Integrated/GAS/Attributes/Core/AttributeSet.cs

`Framework/Integrated/GAS/Attributes/Core/AttributeSet.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Core.AttributeSet

Declared at `Framework/Integrated/GAS/Attributes/Core/AttributeSet.cs:6` (source checkout).

```csharp
public sealed class AttributeSet : IDisposable
    public void Register<T>(AttributeKey<T> key, IAttribute<T> attribute) where T : struct;
    public IAttribute<T> Get<T>(AttributeKey<T> key) where T : struct;
    public bool Has(AAttributeKey key);
    public bool Remove(AAttributeKey key);
    public void Dispose();
    public void Clear();
```

## Integrated/GAS/Attributes/Core/FormulaDouble.cs

`Framework/Integrated/GAS/Attributes/Core/FormulaDouble.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Core.FormulaDouble

Declared at `Framework/Integrated/GAS/Attributes/Core/FormulaDouble.cs:5` (source checkout).

```csharp
public readonly struct FormulaDouble : IFormula<double>
    public int SlotCount { get; }
    public double Calculate(double baseValue, ReadOnlySpan<double> slots);
```

## Integrated/GAS/Attributes/Core/FormulaFloat.cs

`Framework/Integrated/GAS/Attributes/Core/FormulaFloat.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Core.FormulaFloat

Declared at `Framework/Integrated/GAS/Attributes/Core/FormulaFloat.cs:5` (source checkout).

```csharp
public readonly struct FormulaFloat : IFormula<float>
    public int SlotCount { get; }
    public float Calculate(float baseValue, ReadOnlySpan<float> slots);
```
