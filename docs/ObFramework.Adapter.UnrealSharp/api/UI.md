# ObFramework.Adapter.UnrealSharp / UI

[API index](README.md)

## UI/AUIWindow.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/AUIWindow.cs)

### ObFramework.Adapter.UnrealSharp.UI.AUIWindow

Declared at [line 14](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/AUIWindow.cs#L14).

```csharp
public abstract partial class AUIWindow : UUserWidget, IUIWindow
    public abstract IArchitecture GetArchitecture();
    public abstract EUILevel Level { get; }
```

## UI/UIRoot.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/UIRoot.cs)

### ObFramework.Adapter.UnrealSharp.UI.UIRoot

Declared at [line 4](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/UIRoot.cs#L4).

```csharp
public sealed class UIRoot
    public UUserWidget Widget { get; }
    public UCanvasPanel SceneRoot { get; }
    public UCanvasPanel CommonRoot { get; }
    public UCanvasPanel PopupRoot { get; }
    public UIRoot(UUserWidget widget);
```

## UI/UnrealUIAdapter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/UnrealUIAdapter.cs)

### ObFramework.Adapter.UnrealSharp.UI.UnrealUIAdapter

Declared at [line 40](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.UnrealSharp/UI/UnrealUIAdapter.cs#L40).

```csharp
public class UnrealUIAdapter : IUIAdapter
    public UIRoot? Root { get; private set; }
    public IArchitecture? Architecture { get; set; }
    public UIViewRegistry Registry { get; };
    public UnrealUIAdapter(APlayerController owningController, IArchitecture? architecture = null);
    public void Register<TView, TWindow>(string resourceKey, Func<IArchitecture, TView> factory) where TView : AUIView where TWindow : class, IUIWindow;
    public void Register(Type viewType, Type windowType, string resourceKey, Func<IArchitecture, AUIView> factory);
    public async Task InitAsync(CancellationToken ct = default);
    public void Deinit();
    public async Task<UIViewInstance?> CreateAsync(Type viewType, CancellationToken ct = default);
    public void Open(UIViewInstance instance, IUIData? data = null);
    public void Close(UIViewInstance instance);
    public void Release(UIViewInstance instance);
    public bool TryGetView(IUIWindow window, out AUIView view);
```
