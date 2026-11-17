# ObFramework.Adapter.Godot / UI

[API index](README.md)

## UI/AUIWindow.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/AUIWindow.cs)

### ObFramework.Adapter.Godot.UI.AUIWindow

Declared at [line 14](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/AUIWindow.cs#L14).

```csharp
public abstract class AUIWindow : Control, IUIWindow
    public abstract IArchitecture GetArchitecture();
    public abstract EUILevel Level { get; }
```

## UI/GodotUIAdapter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/GodotUIAdapter.cs)

### ObFramework.Adapter.Godot.UI.GodotUIAdapter

Declared at [line 39](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Godot/UI/GodotUIAdapter.cs#L39).

```csharp
public class GodotUIAdapter : IUIAdapter
    public UIRoot? Root { get; set; }
    public IArchitecture? Architecture { get; set; }
    public UIViewRegistry Registry { get; };
    public GodotUIAdapter(ResourcePlugin assets, IArchitecture? architecture = null);
    public void Register<TView, TWindow>(string resourceKey, Func<IArchitecture, TView> factory) where TView : AUIView where TWindow : class, IUIWindow;
    public void Register(Type viewType, Type windowType, string resourceKey, Func<IArchitecture, AUIView> factory);
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
