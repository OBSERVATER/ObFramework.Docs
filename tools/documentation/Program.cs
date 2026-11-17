using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

if (args.Length < 2 || args.Length > 3 || (args.Length == 3 && args[2] != "--check"))
{
    Console.Error.WriteLine("Usage: DocsTool <repository-root> <documentation-root> [--check]");
    return 2;
}
var repo = Path.GetFullPath(args[0]);
var output = Path.GetFullPath(args[1]);
var contextPath = Path.Combine(Directory.GetParent(output)!.FullName, "source-context.json");
if (!File.Exists(contextPath))
{
    Console.Error.WriteLine("source-context.json must exist next to docs/. Run this tool from an ObFramework.Docs checkout with an explicit framework source root.");
    return 2;
}
using var sourceContext = JsonDocument.Parse(File.ReadAllText(contextPath));
var repositoryUrl = sourceContext.RootElement.GetProperty("source_repository").GetString()!.TrimEnd('/');
var publishedCommit = sourceContext.RootElement.GetProperty("published_commit").GetString()!;
var publishedHashes = sourceContext.RootElement.GetProperty("published_hashes");
var projects = new[] { "Framework", "ObFramework.Adapter.Unity", "ObFramework.Adapter.Godot",
    "ObFramework.Adapter.UnrealSharp", "ObFramework.Cli", "ObFramework.Editor.Core",
    "ObFramework.Generator", "ObFramework.Generator.Unity" };
var inventory = new List<object>();
var stale = new List<string>();
if (projects.Any(project => !Directory.Exists(Path.Combine(repo, project))))
{
    Console.Error.WriteLine("The first argument must be the framework source checkout containing Framework/ and the adapter/generator projects, not the documentation checkout.");
    return 2;
}
foreach (var project in projects)
{
    var root = Path.Combine(repo, project);
    var entries = new List<(string File, string Module, List<(string Name, string Signature, int Line)> Types)>();
    var sourceRoot = project == "ObFramework.Editor.Core" ? Path.Combine(repo, "ObFramework.Adapter.Unity", "EditorDeploy", "Core") : root;
    foreach (var file in Directory.EnumerateFiles(sourceRoot, "*.cs", SearchOption.AllDirectories).Order(StringComparer.Ordinal))
    {
        var rel = Slash(Path.GetRelativePath(sourceRoot, file));
        if (rel.Split('/').Any(p => p is "obj" or "bin" or "artifacts" or ".git")) continue;
        // Include the checked-in Unity editor and optional integration branches as well.
        var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(file), new CSharpParseOptions(
            LanguageVersion.Preview, preprocessorSymbols: new[] { "UNITY_EDITOR", "UNITY_2022_3_OR_NEWER" }));
        var syntax = tree.GetRoot();
        var types = new List<(string Name, string Signature, int Line)>();
        foreach (var type in syntax.DescendantNodes().OfType<MemberDeclarationSyntax>()
            .Where(n => n is BaseTypeDeclarationSyntax or DelegateDeclarationSyntax))
        {
            if (!Visible(type)) continue;
            var ns = string.Join(".", type.Ancestors().OfType<BaseNamespaceDeclarationSyntax>().Reverse().Select(n => n.Name.ToString()));
            var name = type switch { TypeDeclarationSyntax t => t.Identifier.Text + t.TypeParameterList?.ToString(), BaseTypeDeclarationSyntax t => t.Identifier.Text, DelegateDeclarationSyntax d => d.Identifier.Text + d.TypeParameterList?.ToString(), _ => "" };
            var parents = type.Ancestors().OfType<BaseTypeDeclarationSyntax>().Reverse().Select(n => n.Identifier.Text);
            var qualified = string.Join(".", new[] { ns }.Concat(parents).Append(name).Where(s => s.Length > 0));
            var lines = new List<string> { Signature(type) };
            if (type is TypeDeclarationSyntax td)
                foreach (var member in td.Members.Where(m => m is not BaseTypeDeclarationSyntax && m is not DelegateDeclarationSyntax && Visible(m)))
                    lines.Add("    " + Signature(member));
            if (type is EnumDeclarationSyntax ed)
                lines.AddRange(ed.Members.Select(m => "    " + Flat(m.WithoutTrivia().ToString()) + ","));
            types.Add((qualified, string.Join("\n", lines), tree.GetLineSpan(type.Span).StartLinePosition.Line + 1));
        }
        entries.Add((rel, Slash(Path.GetDirectoryName(rel) ?? ""), types));
    }
    var api = Path.Combine(output, project, "api");
    if (args.Length != 3) Directory.CreateDirectory(api);
    var index = new StringBuilder($"# {project} API reference\n\nGenerated from checked-in C# declarations. Use the [project guide](../README.md) for setup and usage.\n\nPublic and protected declarations (including implicit interface members and enum values) are grouped by source folder. Inherited members, compiler-synthesized members and generated business members are not expanded. This is a source inventory, not a compiled ABI or a promise that every engine project builds standalone. Unity editor symbols are enabled; other conditional branches may be absent. Attributes and generic constraints are retained; method bodies and nonconstant field initializers are omitted. Private accessors remain visible as part of property signatures.\n\n[Project guide](../README.md)\n\n");
    foreach (var group in entries.Where(e => e.Types.Count > 0).GroupBy(e => e.Module).OrderBy(g => g.Key, StringComparer.Ordinal))
    {
        var page = group.Key.Length == 0 ? "root.md" : group.Key + ".md";
        var dest = Path.Combine(api, page);
        if (args.Length != 3) Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
        index.AppendLine($"- [{(group.Key.Length == 0 ? "Project root" : group.Key)}]({page})");
        var body = new StringBuilder($"# {project} / {(group.Key.Length == 0 ? "root" : group.Key)}\n\n[API index]({Slash(Path.GetRelativePath(Path.GetDirectoryName(dest)!, Path.Combine(api, "README.md")))})\n\n");
        foreach (var entry in group)
        {
            var source = Slash(Path.GetRelativePath(repo, Path.Combine(sourceRoot, entry.File)));
            body.AppendLine($"## {entry.File}\n\n{SourceReference(source, "Source")}\n");
            foreach (var type in entry.Types)
                body.AppendLine($"### {type.Name}\n\nDeclared at {SourceReference(source, $"line {type.Line}", type.Line)}.\n\n```csharp\n{type.Signature}\n```\n");
        }
        Write(dest, body.ToString());
    }
    Write(Path.Combine(api, "README.md"), index.ToString());
    inventory.Add(new { project, sourceRoot = Slash(Path.GetRelativePath(repo, sourceRoot)), files = entries.Select(e => new {
        source = Slash(Path.GetRelativePath(repo, Path.Combine(sourceRoot, e.File))), module = e.Module,
        sha256 = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(Path.Combine(sourceRoot, e.File)))).ToLowerInvariant(),
        types = e.Types.Select(t => new { name = t.Name, line = t.Line }) }) });
    Console.WriteLine($"{project}: {entries.Count} files, {entries.Sum(e => e.Types.Count)} public/protected types");
}
Write(Path.Combine(output, "api-inventory.json"), JsonSerializer.Serialize(inventory, new JsonSerializerOptions { WriteIndented = true }) + "\n");
foreach (var file in stale) Console.Error.WriteLine("Stale or missing: " + file);
return stale.Count == 0 ? 0 : 1;

string SourceReference(string path, string label, int? line = null)
{
    var hash = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(Path.Combine(repo, path)))).ToLowerInvariant();
    if (publishedHashes.TryGetProperty(path, out var expected) && expected.GetString() == hash)
    {
        var urlPath = string.Join("/", path.Split('/').Select(Uri.EscapeDataString));
        return $"[{label}]({repositoryUrl}/blob/{publishedCommit}/{urlPath}{(line.HasValue ? $"#L{line}" : "")})";
    }
    return $"`{path}{(line.HasValue ? $":{line}" : "")}` (source checkout)";
}

static string Slash(string value) => value.Replace('\\', '/');
static string Flat(string value) => Regex.Replace(value, @"\s+", " ").Trim();
void Write(string path, string value)
{
    value = value.Replace("\r\n", "\n").TrimEnd() + "\n";
    if (args.Length == 3)
    {
        if (!File.Exists(path) || File.ReadAllText(path).Replace("\r\n", "\n") != value) stale.Add(path);
    }
    else File.WriteAllText(path, value, new UTF8Encoding(false));
}
static bool Visible(MemberDeclarationSyntax node)
{
    var modifiers = node.Modifiers;
    if (modifiers.Any(SyntaxKind.PrivateKeyword) || (modifiers.Any(SyntaxKind.InternalKeyword) && !modifiers.Any(SyntaxKind.ProtectedKeyword))) return false;
    bool visible = modifiers.Any(SyntaxKind.PublicKeyword) || modifiers.Any(SyntaxKind.ProtectedKeyword)
        || node.Parent is InterfaceDeclarationSyntax;
    return visible && node.Ancestors().OfType<BaseTypeDeclarationSyntax>().All(Visible);
}
static string Signature(MemberDeclarationSyntax node)
{
    // Work on syntax nodes so strings, expression bodies, constraints and overloads survive parsing.
    var clean = node.WithoutLeadingTrivia().WithoutTrailingTrivia();
    clean = clean switch
    {
        TypeDeclarationSyntax t => t.WithMembers(default).WithOpenBraceToken(default).WithCloseBraceToken(default),
        EnumDeclarationSyntax e => e.WithMembers(default).WithOpenBraceToken(default).WithCloseBraceToken(default),
        MethodDeclarationSyntax m => m.WithBody(null).WithExpressionBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
        ConstructorDeclarationSyntax c => c.WithBody(null).WithExpressionBody(null).WithInitializer(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
        DestructorDeclarationSyntax d => d.WithBody(null).WithExpressionBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
        OperatorDeclarationSyntax o => o.WithBody(null).WithExpressionBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
        ConversionOperatorDeclarationSyntax c => c.WithBody(null).WithExpressionBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
        PropertyDeclarationSyntax p => p.WithInitializer(null).WithExpressionBody(null).WithAccessorList(Accessors(p.AccessorList)),
        IndexerDeclarationSyntax i => i.WithExpressionBody(null).WithAccessorList(Accessors(i.AccessorList)),
        EventDeclarationSyntax e => e.WithAccessorList(Accessors(e.AccessorList)),
        FieldDeclarationSyntax f => f.Modifiers.Any(SyntaxKind.ConstKeyword) ? f : f.WithDeclaration(f.Declaration.WithVariables(SyntaxFactory.SeparatedList(f.Declaration.Variables.Select(v => v.WithInitializer(null))))),
        EventFieldDeclarationSyntax e => e.WithDeclaration(e.Declaration.WithVariables(SyntaxFactory.SeparatedList(e.Declaration.Variables.Select(v => v.WithInitializer(null))))),
        _ => clean
    };
    return Flat(clean.NormalizeWhitespace().ToFullString());
}
static AccessorListSyntax Accessors(AccessorListSyntax? list) => list == null
    ? SyntaxFactory.AccessorList(SyntaxFactory.SingletonList(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))))
    : list.WithAccessors(SyntaxFactory.List(list.Accessors.Select(a => a.WithBody(null).WithExpressionBody(null).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))));
