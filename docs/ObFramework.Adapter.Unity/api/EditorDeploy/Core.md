# ObFramework.Adapter.Unity / EditorDeploy/Core

[API index](../README.md)

## EditorDeploy/Core/BindingTablePath.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTablePath.cs)

### ObFramework.Editor.Core.BindingTablePath

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTablePath.cs#L12).

```csharp
public static class BindingTablePath
    public static string MakeRelative(string? storedPath, string? prefabRootName);
    public static string CombinePath(System.Collections.Generic.IReadOnlyList<string> namesInRootOrder);
```

## EditorDeploy/Core/BindingTableRow.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTableRow.cs)

### ObFramework.Editor.Core.UIComponentKind

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTableRow.cs#L7).

```csharp
public enum UIComponentKind
    Unknown = 0,
    Button = 1,
    Text = 2,
    TmpText = 3,
    InputField = 4,
    TmpInputField = 5,
    Toggle = 6,
    Slider = 7,
```

### ObFramework.Editor.Core.UIBindMode

Declared at [line 35](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTableRow.cs#L35).

```csharp
public enum UIBindMode
    OneWay = 0,
    TwoWay = 1,
```

### ObFramework.Editor.Core.UIComponentKindInfo

Declared at [line 48](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTableRow.cs#L48).

```csharp
public static class UIComponentKindInfo
    public static bool IsSupported(UIComponentKind kind);
    public static bool IsCommand(UIComponentKind kind);
    public static UIBindMode DefaultMode(UIComponentKind kind);
    public static string? UnityControlTypeName(UIComponentKind kind);
    public static string? ViewStateTypeName(UIComponentKind kind);
    public static UIComponentKind FromUnityTypeName(string? unityTypeName);
```

### ObFramework.Editor.Core.BindingTableRow

Declared at [line 140](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/BindingTableRow.cs#L140).

```csharp
public sealed class BindingTableRow
    public string ComponentName { get; set; };
    public UIComponentKind Kind { get; set; }
    public string FieldName { get; set; };
    public string ViewMemberName { get; set; };
    public UIBindMode? Mode { get; set; }
    public string? Format { get; set; }
    public string? PrefabPath { get; set; }
    public bool IsDestructive { get; set; }
    public UIBindMode EffectiveMode { get; };
```

## EditorDeploy/Core/EditorDiagnostic.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorDiagnostic.cs)

### ObFramework.Editor.Core.EditorDiagnosticSeverity

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorDiagnostic.cs#L7).

```csharp
public enum EditorDiagnosticSeverity
    Info = 0,
    Warning = 1,
    Error = 2,
```

### ObFramework.Editor.Core.EditorDiagnosticCodes

Declared at [line 24](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorDiagnostic.cs#L24).

```csharp
public static class EditorDiagnosticCodes
    public const string SetupRootRequired = "OBFE001";
    public const string SetupPathInvalid = "OBFE002";
    public const string SetupResourceKeyInvalid = "OBFE003";
    public const string UnitySectionMissing = "OBFE004";
    public const string OverridesParseFailed = "OBFE005";
    public const string DuplicateViewMapping = "OBFE101";
    public const string InvalidRegistryEntry = "OBFE102";
    public const string DuplicateResourceKey = "OBFE103";
    public const string RegistryHasNoEntries = "OBFE104";
    public const string UnusedResourceKeyOverride = "OBFE105";
    public const string InvalidIdentifier = "OBFE201";
    public const string InvalidOutputPath = "OBFE202";
    public const string InvalidLevel = "OBFE203";
    public const string ArchitectureNotSelected = "OBFE204";
    public const string InvalidBindingRow = "OBFE301";
    public const string UnsupportedControlKind = "OBFE302";
    public const string DuplicateBinding = "OBFE303";
    public const string InvalidPrefabPath = "OBFE304";
    public const string MissingPrefabReference = "OBFE305";
    public const string EmptyBindingTable = "OBFE306";
    public const string DestructiveChangePending = "OBFE401";
    public const string DestructiveChangeApplied = "OBFE402";
    public const string ExistingSourceNotEditable = "OBFE403";
```

### ObFramework.Editor.Core.EditorDiagnostic

Declared at [line 105](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorDiagnostic.cs#L105).

```csharp
public sealed class EditorDiagnostic
    public EditorDiagnostic(EditorDiagnosticSeverity severity, string code, string message, string? target = null);
    public EditorDiagnosticSeverity Severity { get; }
    public string Code { get; }
    public string Message { get; }
    public string? Target { get; }
    public bool IsError { get; };
    public override string ToString();
```

## EditorDeploy/Core/EditorFrameworkNames.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorFrameworkNames.cs)

### ObFramework.Editor.Core.EditorFrameworkNames

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorFrameworkNames.cs#L9).

```csharp
public static class EditorFrameworkNames
    public const string AUIView = "ObFramework.Integrated.UI.AUIView";
    public const string AUIWindow = "ObFramework.Adapter.Unity.UI.AUIWindow";
    public const string IArchitecture = "ObFramework.Core.IArchitecture";
    public const string EUILevel = "ObFramework.Integrated.UI.EUILevel";
    public const string IUIData = "ObFramework.Integrated.UI.IUIData";
    public const string ObservablePropertyAttribute = "ObFramework.Integrated.UI.ObservableProperty";
    public const string RelayCommandAttribute = "ObFramework.Integrated.UI.RelayCommand";
    public const string BindViewAttribute = "ObFramework.Adapter.Unity.ViewBinding.BindView";
    public const string UIBindDataAttribute = "ObFramework.Adapter.Unity.ViewBinding.UIBindData";
    public const string UIBindCommandAttribute = "ObFramework.Adapter.Unity.ViewBinding.UIBindCommand";
    public const string UIBindMode = "ObFramework.Adapter.Unity.ViewBinding.UIBindMode";
    public const string UnitySerializeField = "UnityEngine.SerializeField";
    public static string Global(string fullName);
```

## EditorDeploy/Core/EditorGenerationRequest.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationRequest.cs)

### ObFramework.Editor.Core.EditorGenerationRequest

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationRequest.cs#L13).

```csharp
public sealed class EditorGenerationRequest
    public string ViewName { get; set; };
    public string WindowName { get; set; };
    public string Namespace { get; set; };
    public string ContractName { get; set; };
    public bool GenerateContractInterface { get; set; };
    public string ArchitectureTypeFullName { get; set; };
    public string Level { get; set; };
    public string OutputDirectory { get; set; };
    public string ViewOutputPath { get; set; };
    public string WindowOutputPath { get; set; };
    public string ContractOutputPath { get; set; };
    public string? ExistingViewSource { get; set; }
    public string? ExistingWindowSource { get; set; }
    public string? ExistingContractSource { get; set; }
    public string? PrefabPath { get; set; }
    public bool EmitLifecycleHooks { get; set; };
    public IList<BindingTableRow> Rows { get; set; };
    public IList<string> ConfirmedDestructiveChangeIds { get; set; };
    public IDictionary<UIComponentKind, string>? ControlTypeNameOverrides { get; set; }
```

## EditorDeploy/Core/EditorGenerationResult.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationResult.cs)

### ObFramework.Editor.Core.GeneratedFileKind

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationResult.cs#L9).

```csharp
public enum GeneratedFileKind
    View = 0,
    Window = 1,
    Contract = 2,
    Registry = 3,
```

### ObFramework.Editor.Core.GeneratedFile

Declared at [line 25](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationResult.cs#L25).

```csharp
public sealed class GeneratedFile
    public GeneratedFile(string path, GeneratedFileKind kind, string content, bool isModified);
    public string Path { get; }
    public GeneratedFileKind Kind { get; }
    public string Content { get; }
    public bool IsModified { get; }
```

### ObFramework.Editor.Core.DestructiveChangeKind

Declared at [line 57](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationResult.cs#L57).

```csharp
public enum DestructiveChangeKind
    Rename = 0,
    Delete = 1,
```

### ObFramework.Editor.Core.DestructiveChange

Declared at [line 72](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationResult.cs#L72).

```csharp
public sealed class DestructiveChange
    public DestructiveChange(string id, DestructiveChangeKind kind, string description, string target, string? before = null, string? after = null);
    public string Id { get; }
    public DestructiveChangeKind Kind { get; }
    public string Description { get; }
    public string Target { get; }
    public string? Before { get; }
    public string? After { get; }
    public bool Applied { get; internal set; }
    public override string ToString();
```

### ObFramework.Editor.Core.EditorGenerationResult

Declared at [line 126](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorGenerationResult.cs#L126).

```csharp
public sealed class EditorGenerationResult
    public EditorGenerationResult(IReadOnlyList<GeneratedFile> files, IReadOnlyList<EditorDiagnostic> diagnostics, IReadOnlyList<DestructiveChange> destructiveChanges);
    public IReadOnlyList<GeneratedFile> Files { get; }
    public IReadOnlyList<EditorDiagnostic> Diagnostics { get; }
    public IReadOnlyList<DestructiveChange> DestructiveChanges { get; }
    public bool Success { get; };
    public bool HasPendingDestructiveChanges { get; };
    public IReadOnlyList<EditorDiagnostic> Errors { get; };
    public static string WindowFieldChangeId(string fieldName);
```

## EditorDeploy/Core/EditorNaming.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorNaming.cs)

### ObFramework.Editor.Core.EditorNaming

Declared at [line 14](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorNaming.cs#L14).

```csharp
public static class EditorNaming
    public static bool IsValidIdentifier(string? value);
    public static bool IsValidNamespace(string? value);
    public static bool IsValidQualifiedName(string? value);
    public static bool IsValidLevel(string? value);
    public static bool TryNormalizeLevel(string? value, out string level);
    public static string DefaultWindowName(string? viewName);
    public static string DefaultContractName(string? viewName);
    public static string DefaultFieldName(string? componentName);
    public static string DefaultViewMemberName(string? componentName);
    public static string DefaultCommandName(string? componentName);
    public static string ToPascalCase(string? value);
    public static string ToCamelCase(string? value);
    public static bool TryResolveCommandNames(string? commandTarget, out string methodName, out string commandName);
    public static string ToCSharpStringLiteral(string? value);
```

## EditorDeploy/Core/EditorTemplateGenerator.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorTemplateGenerator.cs)

### ObFramework.Editor.Core.EditorTemplateGenerator

Declared at [line 19](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/EditorTemplateGenerator.cs#L19).

```csharp
public static class EditorTemplateGenerator
    public static EditorGenerationResult Generate(EditorGenerationRequest request);
```

## EditorDeploy/Core/UIRegistryGeneration.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistryGeneration.cs)

### ObFramework.Editor.Core.UIRegistryEntry

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistryGeneration.cs#L8).

```csharp
public sealed class UIRegistryEntry
    public string ViewTypeFullName { get; set; };
    public string WindowTypeFullName { get; set; };
    public string? ResourceKeyOverride { get; set; }
```

### ObFramework.Editor.Core.UIRegistryGenerationInput

Declared at [line 26](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistryGeneration.cs#L26).

```csharp
public sealed class UIRegistryGenerationInput
    public UISetupSnapshot? Setup { get; set; }
    public IList<UIRegistryEntry> Entries { get; set; };
    public string Namespace { get; set; };
    public string ClassName { get; set; };
    public string MethodName { get; set; };
    public string AdapterTypeName { get; set; };
    public string OutputPath { get; set; };
    public string? ExistingSource { get; set; }
```

### ObFramework.Editor.Core.UIRegistryResolvedEntry

Declared at [line 60](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistryGeneration.cs#L60).

```csharp
public sealed class UIRegistryResolvedEntry
    public UIRegistryResolvedEntry(string viewTypeFullName, string windowTypeFullName, string viewTypeShortName, string resourceKey);
    public string ViewTypeFullName { get; }
    public string WindowTypeFullName { get; }
    public string ViewTypeShortName { get; }
    public string ResourceKey { get; }
```

### ObFramework.Editor.Core.UIRegistryGenerationOutput

Declared at [line 88](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistryGeneration.cs#L88).

```csharp
public sealed class UIRegistryGenerationOutput
    public UIRegistryGenerationOutput(GeneratedFile? file, IReadOnlyList<EditorDiagnostic> diagnostics, IReadOnlyList<UIRegistryResolvedEntry> registrations);
    public GeneratedFile? File { get; }
    public IReadOnlyList<EditorDiagnostic> Diagnostics { get; }
    public IReadOnlyList<UIRegistryResolvedEntry> Registrations { get; }
    public bool Success { get; }
```

## EditorDeploy/Core/UIRegistrySourceGenerator.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistrySourceGenerator.cs)

### ObFramework.Editor.Core.UIRegistrySourceGenerator

Declared at [line 19](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UIRegistrySourceGenerator.cs#L19).

```csharp
public static class UIRegistrySourceGenerator
    public const string DefaultFileName = "GeneratedUIRegistry.g.cs";
    public static UIRegistryGenerationOutput Generate(UIRegistryGenerationInput input);
    public static string BuildSource(string @namespace, string className, string methodName, string adapterTypeName, IReadOnlyList<UIRegistryResolvedEntry> registrations);
```

## EditorDeploy/Core/UISetupOverridesJson.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupOverridesJson.cs)

### ObFramework.Editor.Core.UISetupOverridesJson

Declared at [line 19](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupOverridesJson.cs#L19).

```csharp
public static class UISetupOverridesJson
    public const string PropertyName = "resourceKeyOverrides";
    public static bool TryRead(string? sectionJson, out IReadOnlyDictionary<string, string> overrides, out string? error);
```

## EditorDeploy/Core/UISetupSnapshot.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshot.cs)

### ObFramework.Editor.Core.UISetupSection

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshot.cs#L13).

```csharp
public sealed class UISetupSection
    public string? Root { get; set; }
    public string? ScriptOutputRoot { get; set; }
    public string? UIAssetRoot { get; set; }
    public string? ResourceKeyFormat { get; set; }
    public IDictionary<string, string>? ResourceKeyOverrides { get; set; }
```

### ObFramework.Editor.Core.UISetupSnapshot

Declared at [line 40](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshot.cs#L40).

```csharp
public sealed class UISetupSnapshot
    public const string DefaultScriptOutputRoot = "Assets/ObFramework/Generated/UI";
    public const string DefaultUIAssetRoot = "Assets/ObFramework/UI";
    public const string DefaultResourceKeyFormat = "UI/{view}";
    public string Root { get; }
    public string ScriptOutputRoot { get; }
    public string UIAssetRoot { get; }
    public string ResourceKeyFormat { get; }
    public IReadOnlyDictionary<string, string> ResourceKeyOverrides { get; };
    public string ScriptOutputDirectory { get; };
    public string UIAssetDirectory { get; };
    public string ResolveResourceKey(string viewTypeName);
    public string ResolvePath(string moduleRelativePath);
    public override string ToString();
```

### ObFramework.Editor.Core.UISetupSnapshotResult

Declared at [line 126](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshot.cs#L126).

```csharp
public sealed class UISetupSnapshotResult
    public UISetupSnapshotResult(UISetupSnapshot? snapshot, IReadOnlyList<EditorDiagnostic> diagnostics);
    public UISetupSnapshot? Snapshot { get; }
    public IReadOnlyList<EditorDiagnostic> Diagnostics { get; }
    public bool Success { get; };
```

### ObFramework.Editor.Core.UISetupPaths

Declared at [line 145](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshot.cs#L145).

```csharp
public static class UISetupPaths
    public static bool IsAbsolutePath(string? path);
    public static bool TryNormalizeRoot(string? root, out string normalized, out string? error);
    public static bool TryNormalizeModuleRelative(string? path, string description, out string normalized, out string? error);
    public static bool LooksLikeFileSystemPath(string? resourceKey, string? developmentRoot);
    public static bool TryNormalizeProjectFile(string? path, string description, out string normalized, out string? error);
```

## EditorDeploy/Core/UISetupSnapshotFactory.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshotFactory.cs)

### ObFramework.Editor.Core.UISetupSnapshotFactory

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/EditorDeploy/Core/UISetupSnapshotFactory.cs#L12).

```csharp
public static class UISetupSnapshotFactory
    public static UISetupSnapshotResult Create(UISetupSection? section);
```
