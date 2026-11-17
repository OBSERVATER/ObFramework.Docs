# Framework / ThirdParty/VCotainer/Internal/InstanceProviders

[API index](../../../README.md)

## ThirdParty/VCotainer/Internal/InstanceProviders/OpenGenericInstanceProvider.cs

[Source](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/InstanceProviders/OpenGenericInstanceProvider.cs)

### VContainer.Internal.OpenGenericInstanceProvider

Declared at [line 7](https://github.com/OBSERVATER/ObFramework/blob/5471678c694280782ef353e887accd18a03296e8/Framework/ThirdParty/VCotainer/Internal/InstanceProviders/OpenGenericInstanceProvider.cs#L7).

```csharp
public class OpenGenericInstanceProvider : IInstanceProvider
    public OpenGenericInstanceProvider(Type implementationType, Lifetime lifetime, List<IInjectParameter> injectParameters);
    public Registration GetClosedRegistration(Type closedInterfaceType, Type[] typeParameters, object key = null);
    public object SpawnInstance(IObjectResolver resolver);
```
