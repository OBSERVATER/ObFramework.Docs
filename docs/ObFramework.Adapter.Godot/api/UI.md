# ObFramework.Adapter.Godot / UI

[API index](README.md)

## UI/AUIWindow.cs

`ObFramework.Adapter.Godot/UI/AUIWindow.cs` (source checkout)

### ObFramework.Adapter.Godot.UI.AUIWindow

Declared at `ObFramework.Adapter.Godot/UI/AUIWindow.cs:7` (source checkout).

```csharp
public abstract partial class AUIWindow : Control, IUIWindow
    public abstract IArchitecture GetArchitecture();
    public abstract EUILevel Level { get; }
```

## UI/GodotUIAdapter.cs

`ObFramework.Adapter.Godot/UI/GodotUIAdapter.cs` (source checkout)

### ObFramework.Adapter.Godot.UI.GodotUIAdapter

Declared at `ObFramework.Adapter.Godot/UI/GodotUIAdapter.cs:13` (source checkout).

```csharp
public sealed class GodotUIAdapter : IUIAdapter
    public UIRoot? Root { get; set; }
    public UIViewRegistry Registry { get; }
    public GodotUIAdapter(ResourcePlugin assets, UIViewRegistry registry);
    public void Register<TView, TWindow>(string resourceKey) where TView : AUIView where TWindow : AUIWindow;
    public async Task<UIViewInstance?> CreateAsync(Type viewType, CancellationToken ct = default);
    public void Open(UIViewInstance instance, IUIData? data = null);
    public void Close(UIViewInstance instance);
    public void Release(UIViewInstance instance);
    public bool TryGetView(IUIWindow window, out AUIView view);
```

## UI/UIRoot.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/UIRoot.cs)

### ObFramework.Adapter.Godot.UI.UIRoot

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/UIRoot.cs#L6).

```csharp
public sealed class UIRoot
    public Node Node { get; }
    public Control SceneRoot { get; }
    public Control CommonRoot { get; }
    public Control PopupRoot { get; }
    public UIRoot(Node root);
    public static UIRoot Ensure(SceneTree tree);
```
