using ObFramework.Core;
using ObFramework.Integrated.Clock;
using ObFramework.Integrated.Pooling;
using ObFramework.Integrated.Resource;
using ObFramework.Utility.Collection;
using ObFramework.Utility.Pipeline;

var adapter = new MemoryAssets();
var application = new ExampleApplication(adapter);
try
{
    await application.StartAsync();
    var architecture = application.Architecture;
    var health = new BlackboardKey<int>("Health");
    var board = new Blackboard();
    board.Set(health, 100);
    Check(architecture.SendQuery(new ReadValue<int>(board, health)) == 100, "query reads current data");
    int version = board.Version;
    board.Set(health, 100);
    Check(board.Version == version, "equal writes do not change the version");
    board.Set(health, 80);
    Check(architecture.SendQuery(new ReadValue<int>(board, health)) == 80, "queries are not cached");

    var resources = architecture.GetPlugin<ResourcePlugin>();
    using (var first = await resources.LoadAssetAsync<Payload>("example"))
    using (var second = resources.LoadAsset<Payload>("example"))
    {
        Check(ReferenceEquals(first.Get<Payload>(), second.Get<Payload>()), "loads share an asset entry");
        Check(adapter.Reads == 1, "shared entry loads only once");
    }
    Check(adapter.Releases == 1, "the final business handle releases the native handle");

    var list = ListPool<int>.Pool.Get();
    try { list.Add(42); }
    finally { ListPool<int>.Pool.Return(list); }
    var reused = ListPool<int>.Pool.Get();
    try { Check(reused.Count == 0, "a returned list is cleared"); }
    finally { ListPool<int>.Pool.Return(reused); }

    int result = new PipelineBuilder<int>().Add(new Increment()).Build().Execute(41);
    Check(result == 42, "a pipeline returns its updated struct context");

    // This console application has no engine frame driver. Only its explicit driver advances time.
    int fired = 0;
    var clock = architecture.GetPlugin<ClockPlugin>();
    clock.Schedule(3, () => fired++);
    clock.Advance(3);
    Check(fired == 1 && clock.Tick == 3, "a timer uses logical ticks");
}
finally { await application.StopAsync(); }
Check(adapter.Releases == 1, "shutdown does not release a disposed handle twice");
Console.WriteLine("Documentation examples passed.");

static void Check(bool condition, string contract)
{
    if (!condition) throw new InvalidOperationException("Example failed: " + contract);
}

public sealed class ExampleArchitecture : AArchitecture<ExampleArchitecture>
{
    protected override void Init() { }
}
public sealed class ExampleApplication : PluginApplication
{
    private readonly MemoryAssets _adapter;
    public ExampleApplication(MemoryAssets adapter) => _adapter = adapter;
    protected override IPlugin[] CreatePlugins() => new IPlugin[] { ResourcePlugin.Create(_adapter), new ClockPlugin(0.02f) };
    protected override IArchitecture CreateArchitecture(IPluginContext plugins)
    {
        ExampleArchitecture.InitInterface(plugins);
        return ExampleArchitecture.Interface;
    }
    protected override Task EnterGameAsync(CancellationToken ct) => Task.CompletedTask;
}
public sealed class Payload { }
public sealed class MemoryAssets : IAssetAdapter<int>
{
    public int Reads { get; private set; }
    public int Releases { get; private set; }
    public AssetLoadResult<T, int> Read<T>(AssetKey request) where T : class
    {
        if (typeof(T) != typeof(Payload)) throw new NotSupportedException(typeof(T).FullName);
        return new AssetLoadResult<T, int>((T)(object)new Payload(), ++Reads);
    }
    public Task<AssetLoadResult<T, int>> ReadAsync<T>(AssetKey request, CancellationToken ct = default) where T : class
    {
        ct.ThrowIfCancellationRequested();
        return Task.FromResult(Read<T>(request));
    }
    public void Release(int handle) => Releases++;
}
public sealed class ReadValue<T> : AQuery<T>
{
    private readonly Blackboard _board;
    private readonly BlackboardKey<T> _key;
    public ReadValue(Blackboard board, BlackboardKey<T> key) { _board = board; _key = key; }
    public override T Q() => _board.Get(_key);
}
public sealed class Increment : IPipelineStep<int>
{
    public StepResult Execute(ref int context) { context++; return StepResult.Continue; }
}
