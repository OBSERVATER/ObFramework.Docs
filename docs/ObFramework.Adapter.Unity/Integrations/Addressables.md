# Optional Addressables integration

The base `ObFramework.Adapter.Unity` assembly excludes provider integrations. The Addressables adapter is distributed as source and compiled inside the consuming Unity project against that project's own Addressables package.

## Install and compose

On initial installation, with Unity closed:

```powershell
ob init --engine unity --root "D:/UnityProjects/MyGame" --resources addressables
```

The CLI preserves a compatible registry-based 1.21.x selection, installs 1.21.21 when absent, and rejects incompatible/non-registry selections. Source and a dedicated asmdef are deployed under `Assets/ObFramework/Integrations/Addressables`. Assemblies with explicit reference lists that use the adapter type need a reference to `ObFramework.Integrations.Addressables`.

Inside your application's `CreatePlugins()` composition:

```csharp
using ObFramework.Adapter.Unity.Resource;
using ObFramework.Integrated.Resource;

var resources = ResourcePlugin.Create(new AddressablesAssetAdapter());
```

Include `resources` with the other plugins, and create `UnityUIAdapter` from that same running resource plugin. Set up Addressables groups, keys, catalogs and content builds in the application. Init does not create them.

## Loading and release contract

`ReadAsync<T>` supports Unity object types, starts `Addressables.LoadAssetAsync<T>`, checks cancellation before loading and after completion, and transfers the successful native handle to the resource manager. A failure/cancellation before transfer releases the native handle in the adapter's `finally`. Successful loads are released by the framework's last business-handle disposal.

Synchronous `Read<T>` throws `NotSupportedException`; use asynchronous loads. The underlying Addressables operation is awaited to completion, so cancellation does not promise immediate termination of that engine operation. Framework callers sharing a resource entry also share its underlying load.

Raw-file loading uses a `TextAsset`, returns its `bytes` without transferring an Addressables handle, and releases the temporary `TextAsset` handle in `finally`. String decoding is handled by the engine-independent resource layer. Do not mutate a shared byte array returned from a cached raw-file entry.

## Changing providers

Omitting `--resources` preserves the provider already recorded by init. Switching a recorded provider requires explicitly preserving/removing its previous integration and updating `modules.installation.resources` in `setup.json` before reinitializing. Keep customized adapters separate from CLI-owned source; the installer does not silently delete customizations.

## Verify

Run static `ob doctor --json`, then `ob doctor --unity --unity-editor <Unity.exe>` for import/compilation. In Play Mode, load the application's real keys, reopen UI, cancel during loading, unload a scene and shut down with outstanding handles. Those runtime behaviors depend on the application's catalog and must be checked in its real scene.

Sources: `ObFramework.Adapter.Unity/Integrations/Addressables/AddressablesAssetAdapter.cs`, `ObFramework.Adapter.Unity/Integrations/Addressables/ObFramework.Integrations.Addressables.asmdef`.
