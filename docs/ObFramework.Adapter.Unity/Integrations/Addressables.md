# Optional Addressables integration

The base `ObFramework.Adapter.Unity` assembly excludes provider integrations. The Addressables adapter is distributed as source and compiled inside the consuming Unity project against that project's own Addressables package.

## Install and compose

Before initialization, set `modules.installation.resources` to `addressables` in the Unity project's `setup.json`. Merge the setting into an existing file, or use this as the initial file.

```json
{
  "modules": {
    "installation": {
      "resources": "addressables"
    }
  }
}
```

Close Unity and run `ob init` from the project root.

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

The recorded provider is used on subsequent initialization and upgrades. To switch providers, preserve any customizations, remove the previous integration as needed, and update `modules.installation.resources` in `setup.json` before reinitializing.

## Verify

Run static `ob doctor --json`, then `ob doctor --unity --unity-editor <Unity.exe>` for import/compilation. In Play Mode, load the application's real keys, reopen UI, cancel during loading, unload a scene and shut down with outstanding handles. Those runtime behaviors depend on the application's catalog and must be checked in its real scene.

Sources: `ObFramework.Adapter.Unity/Integrations/Addressables/AddressablesAssetAdapter.cs`, `ObFramework.Adapter.Unity/Integrations/Addressables/ObFramework.Integrations.Addressables.asmdef`.
