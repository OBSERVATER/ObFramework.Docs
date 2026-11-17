# Framework / ThirdParty/VCotainer/Diagnostics

[API index](../../README.md)

## ThirdParty/VCotainer/Diagnostics/DiagnosticsCollector.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/DiagnosticsCollector.cs)

### VContainer.Diagnostics.DiagnosticsCollector

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/DiagnosticsCollector.cs#L9).

```csharp
public sealed class DiagnosticsCollector
    public string ScopeName { get; }
    public DiagnosticsCollector(string scopeName);
    public IReadOnlyList<DiagnosticsInfo> GetDiagnosticsInfos();
    public void Clear();
    public void TraceRegister(RegisterInfo registerInfo);
    public void TraceBuild(RegistrationBuilder registrationBuilder, Registration registration);
    public object TraceResolve(Registration registration, Func<Registration, object> resolving);
    public void NotifyContainerBuilt(IObjectResolver container);
```

## ThirdParty/VCotainer/Diagnostics/DiagnosticsContext.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/DiagnosticsContext.cs)

### VContainer.Diagnostics.DiagnositcsContext

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/DiagnosticsContext.cs#L7).

```csharp
public static class DiagnositcsContext
    public static event Action<IObjectResolver> OnContainerBuilt;
    public static DiagnosticsCollector GetCollector(string name);
    public static ILookup<string, DiagnosticsInfo> GetGroupedDiagnosticsInfos();
    public static IEnumerable<DiagnosticsInfo> GetDiagnosticsInfos();
    public static void NotifyContainerBuilt(IObjectResolver container);
    public static void RemoveCollector(string name);
```

## ThirdParty/VCotainer/Diagnostics/DiagnosticsInfo.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/DiagnosticsInfo.cs)

### VContainer.Diagnostics.DiagnosticsInfo

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/DiagnosticsInfo.cs#L5).

```csharp
public sealed class DiagnosticsInfo
    public string ScopeName { get; }
    public RegisterInfo RegisterInfo { get; }
    public ResolveInfo ResolveInfo { get; set; }
    public List<DiagnosticsInfo> Dependencies { get; };
    public DiagnosticsInfo(string scopeName, RegisterInfo registerInfo);
```

## ThirdParty/VCotainer/Diagnostics/RegisterInfo.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/RegisterInfo.cs)

### VContainer.Diagnostics.RegisterInfo

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/RegisterInfo.cs#L9).

```csharp
public sealed class RegisterInfo
    public int Id { get; }
    public RegistrationBuilder RegistrationBuilder { get; }
    public StackTrace StackTrace { get; }
    public RegisterInfo(RegistrationBuilder registrationBuilder);
    public string GetFilename();
    public int GetFileLineNumber();
    public string GetScriptAssetPath();
    public string GetHeadline();
```

## ThirdParty/VCotainer/Diagnostics/ResolveInfo.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/ResolveInfo.cs)

### VContainer.Diagnostics.ResolveInfo

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Diagnostics/ResolveInfo.cs#L5).

```csharp
public sealed class ResolveInfo
    public Registration Registration { get; }
    public List<object> Instances { get; };
    public int MaxDepth { get; set; };
    public int RefCount { get; set; }
    public long ResolveTime { get; set; }
    public ResolveInfo(Registration registration);
```
