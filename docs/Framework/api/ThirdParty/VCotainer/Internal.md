# Framework / ThirdParty/VCotainer/Internal

[API index](../../README.md)

## ThirdParty/VCotainer/Internal/ContainerLocal.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/ContainerLocal.cs)

### VContainer.Internal.ContainerLocal<T>

Declared at [line 3](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/ContainerLocal.cs#L3).

```csharp
public sealed class ContainerLocal<T>
    public readonly T Value;
    [Inject] public ContainerLocal(T value);
```

## ThirdParty/VCotainer/Internal/InjectorCache.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/InjectorCache.cs)

### VContainer.Internal.InjectorCache

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/InjectorCache.cs#L7).

```csharp
public static class InjectorCache
    public static IInjector GetOrBuild(Type type);
```

## ThirdParty/VCotainer/Internal/OpenGenericRegistrationBuilder.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/OpenGenericRegistrationBuilder.cs)

### VContainer.Internal.OpenGenericRegistrationBuilder

Declared at [line 6](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/OpenGenericRegistrationBuilder.cs#L6).

```csharp
public class OpenGenericRegistrationBuilder : RegistrationBuilder
    public OpenGenericRegistrationBuilder(Type implementationType, Lifetime lifetime);
    public override Registration Build();
    public override RegistrationBuilder AsImplementedInterfaces();
    protected override void AddInterfaceType(Type interfaceType);
```
