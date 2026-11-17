# ObFramework.Cli / Utility

[API index](README.md)

## Utility/ReferenceNodeHelper.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Utility/ReferenceNodeHelper.cs)

### ObFramework.Cli.Utility.ReferenceNodeHelper

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Utility/ReferenceNodeHelper.cs#L6).

```csharp
public static class ReferenceNodeHelper
    public static string CreateNode(string name, string desc, params string[] names);
    public static string AddReference(string name, params string[] names);
    public static bool RemoveReference(string name, string reference);
    public static bool DeleteNode(string name);
    public static IReadOnlyList<string> ListNodes();
    public static bool TryFindNode(string name, out string result);
    public static bool TryFindReverseNode(string name, out string result);
```
