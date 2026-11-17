# Framework / ThirdParty/VCotainer

[API index](../README.md)

## ThirdParty/VCotainer/Container.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Container.cs)

### VContainer.IObjectResolver

Declared at [line 9](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Container.cs#L9).

```csharp
public interface IObjectResolver : IDisposable
    object ApplicationOrigin { get; }
    DiagnosticsCollector Diagnostics { get; set; }
    object Resolve(Type type, object key = null);
    bool TryResolve(Type type, out object resolved, object key = null);
    object Resolve(Registration registration);
    IScopedObjectResolver CreateScope(Action<IContainerBuilder> installation = null);
    void Inject(object instance);
    bool TryGetRegistration(Type type, out Registration registration, object key = null);
```

### VContainer.IScopedObjectResolver

Declared at [line 45](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Container.cs#L45).

```csharp
public interface IScopedObjectResolver : IObjectResolver
    IObjectResolver Root { get; }
    IScopedObjectResolver Parent { get; }
```

### VContainer.Lifetime

Declared at [line 51](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Container.cs#L51).

```csharp
public enum Lifetime
    Transient,
    Singleton,
    Scoped,
```

### VContainer.ScopedContainer

Declared at [line 58](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Container.cs#L58).

```csharp
public sealed class ScopedContainer : IScopedObjectResolver
    public IObjectResolver Root { get; }
    public IScopedObjectResolver Parent { get; }
    public object ApplicationOrigin { get; }
    public DiagnosticsCollector Diagnostics { get; set; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public object Resolve(Type type, object key = null);
    public bool TryResolve(Type type, out object resolved, object key = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public object Resolve(Registration registration);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public IScopedObjectResolver CreateScope(Action<IContainerBuilder> installation = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Inject(object instance);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool TryGetRegistration(Type type, out Registration registration, object key = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Dispose();
```

### VContainer.Container

Declared at [line 204](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Container.cs#L204).

```csharp
public sealed class Container : IObjectResolver
    public object ApplicationOrigin { get; }
    public DiagnosticsCollector Diagnostics { get; set; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public object Resolve(Type type, object key = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool TryResolve(Type type, out object resolved, object key = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public object Resolve(Registration registration);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public IScopedObjectResolver CreateScope(Action<IContainerBuilder> installation = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Inject(object instance);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool TryGetRegistration(Type type, out Registration registration, object key = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Dispose();
```

## ThirdParty/VCotainer/ContainerBuilder.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/ContainerBuilder.cs)

### VContainer.IContainerBuilder

Declared at [line 12](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/ContainerBuilder.cs#L12).

```csharp
public interface IContainerBuilder
    object ApplicationOrigin { get; set; }
    DiagnosticsCollector Diagnostics { get; set; }
    int Count { get; }
    RegistrationBuilder this[int index] { get; set; }
    T Register<T>(T registrationBuilder) where T : RegistrationBuilder;
    void RegisterBuildCallback(Action<IObjectResolver> container);
    bool Exists(Type type, bool includeInterfaceTypes = false, bool findParentScopes = false);
```

### VContainer.ScopedContainerBuilder

Declared at [line 24](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/ContainerBuilder.cs#L24).

```csharp
public sealed class ScopedContainerBuilder : ContainerBuilder
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public IScopedObjectResolver BuildScope();
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public override IObjectResolver Build();
    public override bool Exists(Type type, bool includeInterfaceTypes = false, bool findParentScopes = false);
```

### VContainer.ContainerBuilder

Declared at [line 74](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/ContainerBuilder.cs#L74).

```csharp
public class ContainerBuilder : IContainerBuilder
    public object ApplicationOrigin { get; set; }
    public int Count { get; };
    public RegistrationBuilder this[int index] { get; set; }
    public DiagnosticsCollector Diagnostics { get; set; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public T Register<T>(T registrationBuilder) where T : RegistrationBuilder;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void RegisterBuildCallback(Action<IObjectResolver> callback);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public virtual bool Exists(Type type, bool includeInterfaceTypes = false, bool findParentScopes = false);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public virtual IObjectResolver Build();
    [MethodImpl(MethodImplOptions.AggressiveInlining)] protected Registry BuildRegistry();
    [MethodImpl(MethodImplOptions.AggressiveInlining)] protected void EmitCallbacks(IObjectResolver container);
```

## ThirdParty/VCotainer/ContainerBuilderExtensions.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/ContainerBuilderExtensions.cs)

### VContainer.ContainerBuilderExtensions

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/ContainerBuilderExtensions.cs#L7).

```csharp
public static class ContainerBuilderExtensions
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder Register(this IContainerBuilder builder, Type type, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder Register(this IContainerBuilder builder, Type interfaceType, Type implementationType, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder Register<T>(this IContainerBuilder builder, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder Register<TInterface, TImplement>(this IContainerBuilder builder, Lifetime lifetime) where TImplement : TInterface;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder Register<TInterface1, TInterface2, TImplement>(this IContainerBuilder builder, Lifetime lifetime) where TImplement : TInterface1, TInterface2;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder Register<TInterface1, TInterface2, TInterface3, TImplement>(this IContainerBuilder builder, Lifetime lifetime) where TImplement : TInterface1, TInterface2, TInterface3;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder Register<TInterface>(this IContainerBuilder builder, Func<IObjectResolver, TInterface> implementationConfiguration, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterInstance(this IContainerBuilder builder, object instance, Type implementationType);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterInstance<TInterface>(this IContainerBuilder builder, TInterface instance);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterInstance<TInterface1, TInterface2>(this IContainerBuilder builder, TInterface1 instance);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterInstance<TInterface1, TInterface2, TInterface3>(this IContainerBuilder builder, TInterface1 instance);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<T>(this IContainerBuilder builder, Func<T> factory);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, T>(this IContainerBuilder builder, Func<TParam1, T> factory);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, TParam2, T>(this IContainerBuilder builder, Func<TParam1, TParam2, T> factory);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, T>(this IContainerBuilder builder, Func<TParam1, TParam2, TParam3, T> factory);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, TParam4, T>(this IContainerBuilder builder, Func<TParam1, TParam2, TParam3, TParam4, T> factory);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<T>(this IContainerBuilder builder, Func<IObjectResolver, Func<T>> factoryFactory, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, T>(this IContainerBuilder builder, Func<IObjectResolver, Func<TParam1, T>> factoryFactory, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, TParam2, T>(this IContainerBuilder builder, Func<IObjectResolver, Func<TParam1, TParam2, T>> factoryFactory, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, T>(this IContainerBuilder builder, Func<IObjectResolver, Func<TParam1, TParam2, TParam3, T>> factoryFactory, Lifetime lifetime);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static RegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, TParam4, T>(this IContainerBuilder builder, Func<IObjectResolver, Func<TParam1, TParam2, TParam3, TParam4, T>> factoryFactory, Lifetime lifetime);
    public static void RegisterDisposeCallback(this IContainerBuilder builder, Action<IObjectResolver> callback);
    [Obsolete("IObjectResolver is registered by default. This method does nothing.")] public static void RegisterContainer(this IContainerBuilder builder);
```

## ThirdParty/VCotainer/IInjectParameter.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IInjectParameter.cs)

### VContainer.IInjectParameter

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IInjectParameter.cs#L5).

```csharp
public interface IInjectParameter
    bool Match(Type parameterType, string parameterName);
    object GetValue(IObjectResolver resolver);
```

## ThirdParty/VCotainer/IInjector.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IInjector.cs)

### VContainer.IInjector

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IInjector.cs#L5).

```csharp
public interface IInjector
    void Inject(object instance, IObjectResolver resolver, IReadOnlyList<IInjectParameter> parameters);
    object CreateInstance(IObjectResolver resolver, IReadOnlyList<IInjectParameter> parameters);
```

## ThirdParty/VCotainer/IInstanceProvider.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IInstanceProvider.cs)

### VContainer.IInstanceProvider

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IInstanceProvider.cs#L3).

```csharp
public interface IInstanceProvider
    object SpawnInstance(IObjectResolver resolver);
```

## ThirdParty/VCotainer/IObjectResolverExtensions.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IObjectResolverExtensions.cs)

### VContainer.IObjectResolverExtensions

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/IObjectResolverExtensions.cs#L7).

```csharp
public static class IObjectResolverExtensions
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static T Resolve<T>(this IObjectResolver resolver, object key = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static bool TryResolve<T>(this IObjectResolver resolver, out T resolved, object key = null);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public static T ResolveOrDefault<T>(this IObjectResolver resolver, T defaultValue = default, object key = null);
    [Preserve] [MethodImpl(MethodImplOptions.AggressiveInlining)] public static object ResolveNonGeneric(this IObjectResolver resolve, Type type, object key = null);
    public static object ResolveOrParameter(this IObjectResolver resolver, Type parameterType, string parameterName, IReadOnlyList<IInjectParameter> parameters, object key = null);
```

## ThirdParty/VCotainer/Registration.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Registration.cs)

### VContainer.Registration

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Registration.cs#L7).

```csharp
public sealed class Registration
    public readonly Type ImplementationType;
    public readonly IReadOnlyList<Type> InterfaceTypes;
    public readonly Lifetime Lifetime;
    public readonly IInstanceProvider Provider;
    public readonly object Key;
    public Registration(Type implementationType, Lifetime lifetime, IReadOnlyList<Type> interfaceTypes, IInstanceProvider provider, object key = null);
    public override string ToString();
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public object SpawnInstance(IObjectResolver resolver);
```

## ThirdParty/VCotainer/RegistrationBuilder.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/RegistrationBuilder.cs)

### VContainer.RegistrationBuilder

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/RegistrationBuilder.cs#L7).

```csharp
public class RegistrationBuilder
    protected internal readonly Type ImplementationType;
    protected internal readonly Lifetime Lifetime;
    protected internal List<Type> InterfaceTypes;
    protected internal List<IInjectParameter> Parameters;
    protected internal object Key;
    public RegistrationBuilder(Type implementationType, Lifetime lifetime);
    public virtual Registration Build();
    public RegistrationBuilder As<TInterface>();
    public RegistrationBuilder As<TInterface1, TInterface2>();
    public RegistrationBuilder As<TInterface1, TInterface2, TInterface3>();
    public RegistrationBuilder As<TInterface1, TInterface2, TInterface3, TInterface4>();
    public RegistrationBuilder AsSelf();
    public virtual RegistrationBuilder AsImplementedInterfaces();
    public RegistrationBuilder As(Type interfaceType);
    public RegistrationBuilder As(Type interfaceType1, Type interfaceType2);
    public RegistrationBuilder As(Type interfaceType1, Type interfaceType2, Type interfaceType3);
    public RegistrationBuilder As(params Type[] interfaceTypes);
    public RegistrationBuilder WithParameter(string name, object value);
    public RegistrationBuilder WithParameter(string name, Func<IObjectResolver, object> value);
    public RegistrationBuilder WithParameter(Type type, object value);
    public RegistrationBuilder WithParameter(Type type, Func<IObjectResolver, object> value);
    public RegistrationBuilder WithParameter<TParam>(TParam value);
    public RegistrationBuilder WithParameter<TParam>(Func<IObjectResolver, TParam> value);
    public RegistrationBuilder WithParameter<TParam>(Func<TParam> value);
    public RegistrationBuilder Keyed(object key);
    protected virtual void AddInterfaceType(Type interfaceType);
```

## ThirdParty/VCotainer/Registry.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Registry.cs)

### VContainer.Internal.Registry

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Registry.cs#L7).

```csharp
public sealed class Registry
    public static Registry Build(Registration[] registrations);
    public bool TryGet(Type interfaceType, object key, out Registration registration);
    public bool Exists(Type type, object key);
```

## ThirdParty/VCotainer/VContainerException.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/VContainerException.cs)

### VContainer.VContainerException

Declared at [line 5](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/VContainerException.cs#L5).

```csharp
public sealed class VContainerException : Exception
    public readonly Type InvalidType;
    public VContainerException(Type invalidType, string message);
```
