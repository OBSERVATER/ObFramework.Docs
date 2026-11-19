# Random selection and shuffling

[Runtime API](../api/Integrated/Random.md) · [Unity source API](../../ObFramework.Adapter.Unity/api/Random.md)

Add `new RandomPlugin(new SystemRandomSource(seed))` to the application's plugin list. Business code obtains it through `architecture.GetPlugin<RandomPlugin>()`. The plugin reports its source's seed and advances that source on each random operation. Use separate source instances when independent sequences are required.

## Selection and in-place shuffling

The following method receives the running application's plugin:

```csharp
using System;
using System.Collections.Generic;
using ObFramework.Integrated.Random;

public static class RandomSelection
{
    public static void Run(RandomPlugin random)
    {
        int roll = random.Range(1, 6); // Includes both 1 and 6.
        int onlyItem = random.GetRandomItem(new List<int> { 42 }); // Always 42.

        var items = new List<string> { "A", "B", "C" };
        var shuffled = random.Shuffle(items); // Returns the same list after modifying it.

        string reward = random.GetByWeight(new Dictionary<string, float>
        {
            ["common"] = 3f,
            ["rare"] = 1f,
            ["disabled"] = 0f
        });
        Console.WriteLine($"{roll}, {onlyItem}, {reward}");
    }
}
```

Integer `Range(minInclude, maxInclude)` uses a closed interval, supports the full `int` domain, and rejects a lower bound greater than the upper bound. Collection indices therefore use `Range(0, Count - 1)`.

`GetRandomItem` requires a non-null, non-empty list and leaves its contents unchanged. `Shuffle` requires a non-null list, modifies it in place, and returns that same instance. Empty and single-item shuffles consume no random values.

## Weight rules and failures

`GetByWeight` selects among entries with finite positive weights. Weights are relative and do not need to sum to one. Finite zero and negative weights are ignored. Totals and cumulative weights use `double`, so large finite `float` weights can be combined without overflowing a `float` sum. A random value of exactly one selects the final positive entry.

| Input | Failure |
| --- | --- |
| Null list or weight dictionary | `ArgumentNullException` |
| Empty list passed to `GetRandomItem`, or empty weight dictionary | `ArgumentException` |
| Any NaN or infinite weight | `ArgumentException` |
| No positive weight | `InvalidOperationException` |

Validation completes before a random value is consumed. A successful weight selection returns an existing key rather than using `default(T)` as a failure sentinel.

## Sources and sequence ownership

`SystemRandomSource` owns its `System.Random` instance. Within the same implementation and runtime, the same seed and call sequence reproduce the same sequence. Mixing selection, shuffling, and direct draws advances that shared source; changing the order or number of calls changes later results.

Unity applications can use `SystemRandomSource` or `ObFramework.Adapter.Unity.Random.UnityRandomSource`. The Unity source stores its own `UnityEngine.Random.State`, loads it for an operation, and restores the external global state afterward, including when an operation throws. Construction also preserves the external state. Multiple Unity source instances and external Unity random calls can therefore be interleaved without advancing each other's sequences on the main thread. Different source algorithms are not promised to produce matching sequences for the same seed.

Use these sources from a single owning thread; the Unity source requires Unity's main thread. The plugin does not add locking. `Value01` may return either endpoint, including one.

## Independent deterministic streams

Create each stream once for a sequence of draws. Use stable numeric keys in a fixed order. Recreating a stream with the same seed and keys restarts its sequence. Creating or advancing another stream leaves this sequence intact.

```csharp
var shop = RandomPlugin.CreateStream(42UL, 0UL);
var battle = RandomPlugin.CreateStream(42UL, 2UL);
int offerIndex = shop.NextInt(5); // 0 through 4
int attackRoll = battle.NextInt(100); // 0 through 99
```

The static factory uses the supplied 64-bit seed. With keys, it folds the seed and each key as eight little-endian bytes using FNV-1a. With just a seed, SplitMix64 starts directly at that state. Use NextUInt64() for a full-width draw.
