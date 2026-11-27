# Framework / Integrated/GAS/Attributes/Creational

[API index](../../../README.md)

## Integrated/GAS/Attributes/Creational/FormulaBuilderDouble.cs

`Framework/Integrated/GAS/Attributes/Creational/FormulaBuilderDouble.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Creational.FormulaBuilderDouble

Declared at `Framework/Integrated/GAS/Attributes/Creational/FormulaBuilderDouble.cs:5` (source checkout).

```csharp
public ref struct FormulaBuilderDouble
    public FormulaDouble Build();
    public void LoadBase();
    public void LoadSlot(int slotIndex);
    public void LoadConstant(double value);
    public void Add();
    public void Subtract();
    public void Multiply();
    public void Divide();
    public void Min();
    public void Max();
    public void Clamp();
    public void Negate();
    public void Dispose();
```

## Integrated/GAS/Attributes/Creational/FormulaBuilderFloat.cs

`Framework/Integrated/GAS/Attributes/Creational/FormulaBuilderFloat.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Creational.FormulaBuilderFloat

Declared at `Framework/Integrated/GAS/Attributes/Creational/FormulaBuilderFloat.cs:5` (source checkout).

```csharp
public ref struct FormulaBuilderFloat
    public FormulaFloat Build();
    public void LoadBase();
    public void LoadSlot(int slotIndex);
    public void LoadConstant(float value);
    public void Add();
    public void Subtract();
    public void Multiply();
    public void Divide();
    public void Min();
    public void Max();
    public void Clamp();
    public void Negate();
    public void Dispose();
```

## Integrated/GAS/Attributes/Creational/FormulaPresets.cs

`Framework/Integrated/GAS/Attributes/Creational/FormulaPresets.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Creational.FormulaPresets

Declared at `Framework/Integrated/GAS/Attributes/Creational/FormulaPresets.cs:5` (source checkout).

```csharp
public static partial class FormulaPresets
```

### ObFramework.Integrated.GAS.Attributes.Creational.FormulaPresets.Float

Declared at `Framework/Integrated/GAS/Attributes/Creational/FormulaPresets.cs:7` (source checkout).

```csharp
public static partial class Float
    public static FormulaFloat Default();
    public static FormulaFloat Wow();
```

### ObFramework.Integrated.GAS.Attributes.Creational.FormulaPresets.Double

Declared at `Framework/Integrated/GAS/Attributes/Creational/FormulaPresets.cs:50` (source checkout).

```csharp
public static partial class Double
    public static FormulaDouble Default();
    public static FormulaDouble Wow();
```

## Integrated/GAS/Attributes/Creational/Formulas.cs

`Framework/Integrated/GAS/Attributes/Creational/Formulas.cs` (source checkout)

### ObFramework.Integrated.GAS.Attributes.Creational.Formulas

Declared at `Framework/Integrated/GAS/Attributes/Creational/Formulas.cs:3` (source checkout).

```csharp
public static partial class Formulas
    public static FormulaBuilderFloat CreateFloat(int size = DefaultSize);
    public static FormulaBuilderDouble CreateDouble(int size = DefaultSize);
```
