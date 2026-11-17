# ObFramework.Cli / Deployment

[API index](README.md)

## Deployment/DeployableFileSource.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeployableFileSource.cs)

### ObFramework.Cli.Deployment.IDeployableFileSource

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeployableFileSource.cs#L13).

```csharp
public interface IDeployableFileSource
    string Description { get; }
    IReadOnlyList<string> ListFiles();
    bool TryRead(string relativePath, out byte[] content);
```

### ObFramework.Cli.Deployment.EmbeddedDeployableFileSource

Declared at [line 36](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeployableFileSource.cs#L36).

```csharp
public sealed class EmbeddedDeployableFileSource : IDeployableFileSource
    public const string DefaultResourcePrefix = "ObFramework.Cli.UnityEditorDeploy";
    public EmbeddedDeployableFileSource(Assembly? assembly = null, string resourcePrefix = DefaultResourcePrefix);
    public static EmbeddedDeployableFileSource CreateDefault();
    public string Description { get; };
    public IReadOnlyList<string> ListFiles();
    public bool TryRead(string relativePath, out byte[] content);
```

### ObFramework.Cli.Deployment.DirectoryDeployableFileSource

Declared at [line 114](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeployableFileSource.cs#L114).

```csharp
public sealed class DirectoryDeployableFileSource : IDeployableFileSource
    public DirectoryDeployableFileSource(string rootDirectory);
    public string RootDirectory { get; }
    public string Description { get; };
    public IReadOnlyList<string> ListFiles();
    public bool TryRead(string relativePath, out byte[] content);
```

## Deployment/DeploymentException.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeploymentException.cs)

### ObFramework.Cli.Deployment.DeploymentException

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeploymentException.cs#L9).

```csharp
public sealed class DeploymentException : Exception
    public DeploymentException(string message);
    public DeploymentException(string message, Exception innerException);
```

## Deployment/DeploymentManifest.cs

`ObFramework.Cli/Deployment/DeploymentManifest.cs` (source checkout)

### ObFramework.Cli.Deployment.DeploymentManifest

Declared at `ObFramework.Cli/Deployment/DeploymentManifest.cs:21` (source checkout).

```csharp
public sealed class DeploymentManifest
    public const string DefaultSourceFileName = "deploy-manifest.json";
    public const string DefaultOwnedManifestPath = "Assets/ObFramework/obf-deploy.json";
    public const string EmbeddedPlanResourceName = "ObFramework.Cli.UnityDeployPlan/deploy-manifest.json";
    public const int CurrentSchemaVersion = 1;
    public int SchemaVersion { get; init; };
    public string Tool { get; init; };
    public string Deployment { get; init; };
    public string ManifestPath { get; init; };
    public string TargetRoot { get; init; };
    public bool AutoDeployResources { get; init; }
    public bool RequireComplete { get; init; }
    public IReadOnlyList<string> Exclude { get; init; };
    public IReadOnlyList<DeployableFile> Files { get; init; };
    public static DeploymentManifest ForFiles(IEnumerable<DeployableFile> files);
    public static DeploymentManifest Load(string json);
    public static DeploymentManifest LoadFromSource(IDeployableFileSource source);
    public static DeploymentManifest LoadEmbeddedPlan(Assembly? assembly = null);
    public IReadOnlyList<DeployableFile> ResolveFiles(IDeployableFileSource source);
    public void Validate();
```

### ObFramework.Cli.Deployment.DeployableFile

Declared at `ObFramework.Cli/Deployment/DeploymentManifest.cs:270` (source checkout).

```csharp
public sealed class DeployableFile
    public DeployableFile();
    public DeployableFile(string source, string target, string? kind = null, string? description = null);
    public string Source { get; init; };
    public string Target { get; init; };
    public string Kind { get; init; };
    public string? Description { get; init; }
```

## Deployment/DeploymentResult.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeploymentResult.cs)

### ObFramework.Cli.Deployment.DeploymentChangeKind

Declared at [line 8](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeploymentResult.cs#L8).

```csharp
public enum DeploymentChangeKind
    Installed,
    Updated,
    Unchanged,
    Removed,
    Skipped,
    Missing,
```

### ObFramework.Cli.Deployment.DeploymentFileChange

Declared at [line 30](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeploymentResult.cs#L30).

```csharp
public sealed class DeploymentFileChange
    public DeploymentFileChange(string path, DeploymentChangeKind change, string? detail = null);
    public string Path { get; }
    public DeploymentChangeKind Change { get; }
    public string? Detail { get; }
```

### ObFramework.Cli.Deployment.DeploymentOptions

Declared at [line 50](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeploymentResult.cs#L50).

```csharp
public sealed class DeploymentOptions
    public bool DryRun { get; init; }
```

### ObFramework.Cli.Deployment.DeploymentResult

Declared at [line 57](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/DeploymentResult.cs#L57).

```csharp
public sealed class DeploymentResult
    public DeploymentResult(IReadOnlyList<DeploymentFileChange> changes, IReadOnlyList<string> warnings, bool dryRun);
    public IReadOnlyList<DeploymentFileChange> Changes { get; }
    public IReadOnlyList<string> Warnings { get; }
    public bool DryRun { get; }
    public int InstalledCount { get; };
    public int UpdatedCount { get; };
    public int UnchangedCount { get; };
    public int RemovedCount { get; };
    public int SkippedCount { get; };
    public int MissingCount { get; };
    public bool HasChanges { get; };
    public int Count(DeploymentChangeKind kind);
    public string Summary { get; }
```

## Deployment/OwnedDeploymentManifest.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/OwnedDeploymentManifest.cs)

### ObFramework.Cli.Deployment.OwnedDeploymentManifest

Declared at [line 22](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/OwnedDeploymentManifest.cs#L22).

```csharp
public sealed class OwnedDeploymentManifest
    public const int CurrentSchemaVersion = 1;
    public int SchemaVersion { get; init; };
    public string Tool { get; init; };
    public string Deployment { get; init; };
    public IReadOnlyList<OwnedDeploymentFile> Files { get; init; };
    public static OwnedDeploymentManifest ForFiles(IEnumerable<OwnedDeploymentFile> files);
    public static OwnedDeploymentManifest? TryLoad(string filePath);
    public string ToJson();
```

### ObFramework.Cli.Deployment.OwnedDeploymentFile

Declared at [line 128](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/OwnedDeploymentManifest.cs#L128).

```csharp
public sealed class OwnedDeploymentFile
    public OwnedDeploymentFile();
    public OwnedDeploymentFile(string path, string sha256, string? kind = null);
    public string Path { get; init; };
    public string Sha256 { get; init; };
    public string Kind { get; init; };
```

## Deployment/UnityDeployCliModule.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/UnityDeployCliModule.cs)

### ObFramework.Cli.Deployment.UnityDeployCliModule

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Deployment/UnityDeployCliModule.cs#L12).

```csharp
public static class UnityDeployCliModule
    public const string CommandName = "unity";
    public static void Register(CliModule cli);
```

## Deployment/UnityEditorDeployer.cs

`ObFramework.Cli/Deployment/UnityEditorDeployer.cs` (source checkout)

### ObFramework.Cli.Deployment.UnityEditorDeployer

Declared at `ObFramework.Cli/Deployment/UnityEditorDeployer.cs:28` (source checkout).

```csharp
public sealed class UnityEditorDeployer
    public UnityEditorDeployer(IDeployableFileSource source, DeploymentManifest manifest);
    public DeploymentManifest Manifest { get; };
    public DeploymentResult Deploy(string unityProjectDirectory, DeploymentOptions? options = null);
    public DeploymentResult Remove(string unityProjectDirectory, DeploymentOptions? options = null);
```
