# Framework / Extension

[API index](README.md)

## Extension/DictionaryExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/DictionaryExtension.cs)

### ObFramework.Extension.SystemCollectionExtension

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/DictionaryExtension.cs#L5).

```csharp
public static class SystemCollectionExtension
    public static V GetOrAdd<K, V>(this Dictionary<K, V> self, K key, V val);
```

## Extension/ICanExtension.cs

`Framework/Extension/ICanExtension.cs` (source checkout)

### ObFramework.Extension.ICanExtension

Declared at `Framework/Extension/ICanExtension.cs:8` (source checkout).

```csharp
public static class ICanExtension
    public static T GetPlugin<T>(this ICanGetPlugin self) where T : class, IPlugin;
    public static T GetModel<T>(this ICanGetModel self) where T : class, IModel;
    public static T GetSystem<T>(this ICanGetSystem self) where T : class, ISystem;
    public static void SendCommand<T>(this ICanSendCommand self) where T : ICommand, new();
    public static void SendCommand(this ICanSendCommand self, ICommand command);
    public static TResult SendCommand<TResult>(this ICanSendCommand self, ICommand<TResult> command);
    public static ValueTask SendCommandAsync<T>(this ICanSendCommand self, CancellationToken ct = default) where T : IAsyncCommand, new();
    public static ValueTask SendCommandAsync(this ICanSendCommand self, IAsyncCommand command, CancellationToken ct = default);
    public static ValueTask<TResult> SendCommandAsync<TResult>(this ICanSendCommand self, IAsyncCommand<TResult> command, CancellationToken ct = default);
    public static IDisposable RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent) where T : struct;
    public static void SendEvent<T>(this ICanSendEvent self, T e) where T : struct;
    public static void SendEvent<T>(this ICanSendEvent self) where T : struct;
    public static void RegisterLocalService<I, T>(this ICanRegisterService self) where I : IService where T : class, I;
    public static T GetLocalService<T>(this ICanRegisterService self) where T : class, IService;
    public static TResult Query<TQuery, TResult>(this ICanQuery self) where TQuery : class, IQuery<TResult>, new();
    public static TResult Query<TResult>(this ICanQuery self, IQuery<TResult> query);
```

## Extension/MathExtension.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/MathExtension.cs)

### ObFramework.Extension.MathExtension

Declared at [line 4](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/Extension/MathExtension.cs#L4).

```csharp
public class MathExtension
    public static ulong IntPow(ulong b, int exp);
    public static int NextPowerTo2(uint val);
    public static int NextPowerTo2_64(uint val);
```
