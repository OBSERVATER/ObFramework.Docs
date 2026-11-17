# ObFramework.Cli / Commands/Reference

[API index](../README.md)

## Commands/Reference/ReferenceCommand.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Reference/ReferenceCommand.cs)

### ObFramework.Cli.Commands.Reference.ReferenceSettings

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Reference/ReferenceCommand.cs#L7).

```csharp
public sealed class ReferenceSettings : CommandSettings
    [CommandArgument(0, "<ACTION>")] [Description("add, link, unlink, find, reverse, list or delete")] public string Action { get; set; };
    [CommandArgument(1, "[NODE]")] [Description("System or Model name")] public string Node { get; set; };
    [CommandArgument(2, "[REFERENCES]")] [Description("Referenced System or Model names")] public string[] References { get; set; };
    [CommandOption("-d|--desc <DESCRIPTION>")] [Description("Description of the System or Model")] public string NodeDescription { get; set; };
```

### ObFramework.Cli.Commands.Reference.ReferenceCommand

Declared at [line 25](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Reference/ReferenceCommand.cs#L25).

```csharp
public sealed class ReferenceCommand : Command<ReferenceSettings>
    public override int Execute(CommandContext context, ReferenceSettings settings, CancellationToken cancellationToken);
```

## Commands/Reference/ReferenceNode.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Reference/ReferenceNode.cs)

### ObFramework.Cli.Commands.Reference.ReferenceNode

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Cli/Commands/Reference/ReferenceNode.cs#L3).

```csharp
public record ReferenceNode
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string>? Reference { get; set; }
```
