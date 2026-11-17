# ObFramework.Cli / Commands/Config

[API index](../README.md)

## Commands/Config/ConfigCliModule.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ConfigCliModule.cs)

### ObFramework.Cli.Commands.Config.ConfigCliModule

Declared at [line 16](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ConfigCliModule.cs#L16).

```csharp
public static class ConfigCliModule
    public const string CommandName = "config";
    public static ModuleConfigRegistry CreateDefaultRegistry();
    public static void RegisterBuiltInModules(ModuleConfigRegistry registry);
    public static void Register(CliModule cli);
```

## Commands/Config/ISetupConfigModule.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ISetupConfigModule.cs)

### ObFramework.Cli.Commands.Config.ISetupConfigModule

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ISetupConfigModule.cs#L13).

```csharp
public interface ISetupConfigModule
    string ModuleName { get; }
    string SectionName { get; }
    Type ConfigType { get; }
```

### ObFramework.Cli.Commands.Config.SetupConfigModule<TConfig>

Declared at [line 30](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ISetupConfigModule.cs#L30).

```csharp
public sealed class SetupConfigModule<TConfig> : ISetupConfigModule where TConfig : class, new()
    public SetupConfigModule(string moduleName, string sectionName);
    public string ModuleName { get; }
    public string SectionName { get; }
    public Type ConfigType { get; };
```

## Commands/Config/ModuleConfigRegistry.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ModuleConfigRegistry.cs)

### ObFramework.Cli.Commands.Config.ModuleConfigRegistry

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/ModuleConfigRegistry.cs#L11).

```csharp
public sealed class ModuleConfigRegistry
    public IReadOnlyList<SetupConfigRegistration> Modules { get; };
    public SetupConfigRegistration Register<TConfig>(string moduleName, string sectionName) where TConfig : class, new();
    public SetupConfigRegistration Register(Type configType, string moduleName, string sectionName);
    public SetupConfigRegistration Register(ISetupConfigModule module);
    public bool TryGet(string moduleOrSectionName, out SetupConfigRegistration registration);
    public SetupConfigRegistration Get(string moduleOrSectionName);
    public IReadOnlyList<string> GetModuleNames();
```

## Commands/Config/SetupConfigException.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigException.cs)

### ObFramework.Cli.Commands.Config.SetupConfigException

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigException.cs#L11).

```csharp
public sealed class SetupConfigException : Exception
    public SetupConfigException(string message);
    public SetupConfigException(string message, Exception innerException);
```

## Commands/Config/SetupConfigPaths.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigPaths.cs)

### ObFramework.Cli.Commands.Config.SetupConfigPaths

Declared at [line 11](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigPaths.cs#L11).

```csharp
public static class SetupConfigPaths
    public static string ValidateAbsoluteRoot(string? root);
    public static bool IsPathWithinRoot(string absoluteRoot, string fullPath);
    public static string NormalizeModulePath(string absoluteRoot, string modulePath);
    public static string GetFullModulePath(string absoluteRoot, string modulePath);
```

## Commands/Config/SetupConfigRegistration.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigRegistration.cs)

### ObFramework.Cli.Commands.Config.SetupConfigRegistration

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigRegistration.cs#L9).

```csharp
public sealed class SetupConfigRegistration
    public SetupConfigRegistration(string moduleName, string sectionName, Type configType);
    public string ModuleName { get; }
    public string SectionName { get; }
    public Type ConfigType { get; }
    public IReadOnlyList<PropertyInfo> PathProperties { get; }
    public object CreateDefault();
    public SetupPathNormalizationResult NormalizePaths(string absoluteRoot, object config);
    public IReadOnlyList<SetupPathValue> ReadPathValues(object config);
```

### ObFramework.Cli.Commands.Config.SetupPathValue

Declared at [line 215](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigRegistration.cs#L215).

```csharp
public readonly struct SetupPathValue
    public SetupPathValue(string memberName, string originalValue, string normalizedValue);
    public string MemberName { get; }
    public string OriginalValue { get; }
    public string NormalizedValue { get; }
```

### ObFramework.Cli.Commands.Config.SetupPathNormalizationResult

Declared at [line 235](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigRegistration.cs#L235).

```csharp
public sealed class SetupPathNormalizationResult
    public SetupPathNormalizationResult(IReadOnlyList<string> errors, IReadOnlyList<SetupPathValue> paths);
    public IReadOnlyList<string> Errors { get; }
    public IReadOnlyList<SetupPathValue> Paths { get; }
    public bool IsValid { get; };
```

## Commands/Config/SetupConfigValues.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigValues.cs)

### ObFramework.Cli.Commands.Config.SetupConfigValues

Declared at [line 16](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigValues.cs#L16).

```csharp
public static class SetupConfigValues
    public static bool TrySet(object config, string key, string value, out string? error);
    public static bool TryUnset(object config, string key, out string? error);
    public static string FormatValue(object? value);
    public static IReadOnlyList<SetupConfigPropertyValue> Describe(object config);
```

### ObFramework.Cli.Commands.Config.SetupConfigPropertyValue

Declared at [line 255](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupConfigValues.cs#L255).

```csharp
public sealed class SetupConfigPropertyValue
    public SetupConfigPropertyValue(string propertyName, string jsonName, string value, bool isModulePath, Type propertyType);
    public string PropertyName { get; }
    public string JsonName { get; }
    public string Value { get; }
    public bool IsModulePath { get; }
    public Type PropertyType { get; }
```

## Commands/Config/SetupJsonDiscovery.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupJsonDiscovery.cs)

### ObFramework.Cli.Commands.Config.SetupJsonDiscovery

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupJsonDiscovery.cs#L12).

```csharp
public static class SetupJsonDiscovery
    public static string? FindSetupFile(string? startDirectory = null);
    public static string? ResolveSetupFile(string? explicitFile, string? startDirectory = null, bool requireExplicitFileExists = true);
    public static string ResolvePath(string path, string? baseDirectory = null);
```

## Commands/Config/SetupJsonDocument.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupJsonDocument.cs)

### ObFramework.Cli.Commands.Config.SetupJsonDocument

Declared at [line 27](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupJsonDocument.cs#L27).

```csharp
public sealed class SetupJsonDocument
    public const string DefaultFileName = "setup.json";
    public const string RootPropertyName = "root";
    public const string ModulesPropertyName = "modules";
    public string? FilePath { get; };
    public bool Exists { get; };
    public IReadOnlyList<string> PropertyNames { get; };
    public IReadOnlyList<string> ModuleSectionNames { get; }
    public static SetupJsonDocument Create(string filePath);
    public static SetupJsonDocument Parse(string json, string? filePath = null);
    public static SetupJsonDocument Load(string filePath);
    public static SetupJsonDocument LoadOrCreate(string filePath);
    public static SetupJsonDocument? TryLoad(string filePath);
    public string? GetRootValue();
    public void SetRoot(string absoluteRoot);
    public string GetAbsoluteRoot();
    public bool TryGetAbsoluteRoot(out string root, out string? error);
    public bool HasModuleSection(string sectionName);
    public TConfig? GetModuleConfig<TConfig>(string sectionName) where TConfig : class;
    public object? GetModuleConfig(SetupConfigRegistration registration);
    public object GetOrCreateModuleConfig(SetupConfigRegistration registration);
    public void SetModuleConfig<TConfig>(string sectionName, TConfig config) where TConfig : class;
    public void SetModuleConfig(SetupConfigRegistration registration, object config);
    public bool RemoveModuleConfig(string sectionName);
    public string ToJsonString();
    public void Save();
    public void Save(string filePath);
```

## Commands/Config/SetupPathAttribute.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupPathAttribute.cs)

### ObFramework.Cli.Commands.Config.SetupPathAttribute

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Config/SetupPathAttribute.cs#L12).

```csharp
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)] public sealed class SetupPathAttribute : Attribute
    public bool AllowEmpty { get; set; }
    public string? Description { get; set; }
```
