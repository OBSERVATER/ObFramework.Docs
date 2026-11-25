---
status: accepted
---

# Instantiate the Unity UI root at application startup

The Unity initializer copies the packaged UI root prefab and its `.meta` file and generates an editable `GameLauncher` template. A project attaches the launcher to its startup scene, which owns the launcher for its scene lifetime. During `Awake`, the launcher creates a `GameManager` root and loads the prefab with the `Resources` key `ObFramework/UIRoot`. The manager's host owns both persistent roots. `UIRoot` stays at the top level of `DontDestroyOnLoad` for Screen Space - Overlay rendering.

In `Start`, the launcher starts the same `GameApplication` type used by the engine-independent .NET tests. The concrete launcher supplies the complete plugin list when it creates the business application. The framework starts that list in one plugin host. The application initializes the Architecture and runs `EnterGameAsync`. Once that completes, the launcher uses `StartGameAsync` for Unity-specific entry work such as opening the first window or loading a scene.

The editable launcher template creates and configures `ClockPlugin` in `ConfigurePlugins` and provides a hook for game entry. The starter supplies Unity view bindings and a prefab with a canvas and the `Scene`, `Common`, and `Popup` layers. The game project supplies its input components, resource provider, `UIPlugin` configuration, and business startup. Re-running initialization preserves the project's launcher source, prefab, and prefab GUID.

The game application is the entry point for a game-initiated exit request. That request and Unity's quit callback use the same shutdown flow. The application owns awaited business shutdown, including `StopGameAsync`, Architecture teardown, and plugin shutdown. The Unity host releases its persistent objects and asks the Player to quit after application shutdown completes.
