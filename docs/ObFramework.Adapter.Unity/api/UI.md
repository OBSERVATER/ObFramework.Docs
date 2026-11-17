# ObFramework.Adapter.Unity / UI

[API index](README.md)

## UI/AUIWindow.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/AUIWindow.cs)

### ObFramework.Adapter.Unity.UI.AUIWindow

Declared at [line 13](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/AUIWindow.cs#L13).

```csharp
public abstract class AUIWindow : AViewController, IUIWindow
    public abstract EUILevel Level { get; }
```

## UI/UIRoot.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/UIRoot.cs)

### ObFramework.Adapter.Unity.UI.UIRoot

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/UIRoot.cs#L7).

```csharp
public class UIRoot : MonoBehaviour
    public Transform SceneRoot { get; };
    public Transform CommonRoot { get; };
    public Transform PopupRoot { get; };
    public void Init();
```

## UI/UnityUIAdapter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/UnityUIAdapter.cs)

### ObFramework.Adapter.Unity.UI.UnityUIAdapter

Declared at [line 22](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/ObFramework.Adapter.Unity/UI/UnityUIAdapter.cs#L22).

```csharp
public sealed class UnityUIAdapter : IUIAdapter
    public UnityUIAdapter(ResourcePlugin assets, UIViewRegistry registry);
    public UIRoot? Root { get; set; }
    public UIViewRegistry Registry { get; }
    public void Register<TView, TWindow>(string resourceKey) where TView : AUIView where TWindow : AUIWindow;
    public async Task<UIViewInstance?> CreateAsync(Type viewType, CancellationToken ct = default);
    public void Open(UIViewInstance instance, IUIData? data = null);
    public void Close(UIViewInstance instance);
    public void Release(UIViewInstance instance);
    public bool TryGetView(IUIWindow window, out AUIView view);
```
