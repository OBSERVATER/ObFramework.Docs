# Framework (ObFramework runtime)

The engine-independent runtime lives in `Framework/` and targets .NET Standard 2.1. [API index](api/README.md) covers public/protected declarations by folder, including Extension and vendored VContainer APIs. The `Framework/ObFramework.csproj` enables XML documentation in built packages.

## Core

| Area | Guide | Source declarations |
| --- | --- | --- |
| Layer responsibilities, communication rules and Service ownership | [Architecture conventions](Core/Conventions.md) | [Core API](api/Core.md) |
| Architecture, models, systems, commands and events | [Architecture](Core/Architecture.md) | [Core API](api/Core.md) |
| Plugin graph and application ownership | [Plugins](Core/Plugins.md) | [Plugin API](api/Core/Plugins.md) |
| Query and typed current-data storage | [Query and Blackboard](Core/Query.md) | [Query API](api/Core.md), [Collection API](api/Utility/Collection.md) |
| Tasks, cancellation and lifetime versions | [Async](Core/Async.md) | [Async API](api/Core/Async.md) |

## Integrated modules

| Area | Guide | Public facade |
| --- | --- | --- |
| Resource loading and native handles | [Resource](Integrated/Resource.md) | `ResourcePlugin` |
| Views, native windows, caching and commands | [UI](Integrated/UI.md) | `UIPlugin` |
| Configuration catalogs and Luban tables | [Config](Integrated/Config.md) | `ConfigPlugin` |
| Save targets, slots and migration | [Save](Integrated/Save.md) | `SavePlugin` |
| Combat attributes, effects and abilities | [Gameplay Ability System](Integrated/GAS.md) | `GASPlugin`, `AbilitySystem` |
| Logic ticks and frame callbacks | [Clock](Integrated/Clock.md) | `ClockPlugin` |
| Random selection, weights and shuffling | [Random](Integrated/Random.md) | `RandomPlugin` |
| Contextual logging | [Diagnostics](Integrated/Diagnostics.md) | `DiagnosticsPlugin` |
| Reusable lists, dictionaries and custom objects | [Pooling](Integrated/Pooling.md) | `ListPool<T>.Pool`, `DictionaryPool<TKey,TValue>.Pool` |

[Utility guide](Utility/README.md) covers Pipeline, FSM, LRUCache, Singleton and capability extensions. Managers such as `AssetManager`, `UIManager`, `SaveManager` and `ConfigManager` are implementation details; business code uses plugin facades. Plugin contexts are scoped to a host. Architecture's generic static instance is per closed Architecture type and must be deinitialized before reuse with another context.
