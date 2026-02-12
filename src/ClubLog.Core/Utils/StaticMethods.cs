using ClubLog.Core.Models;

namespace ClubLog.Core.Utils;

public static class StaticMethods
{
    private static readonly int[] GoodPoolSizes = [6, 7, 8];
    private static readonly int[] BadPoolSizes = [5, 4, 3, 2];

    public static List<List<Pool>> GeneratePoolsSizes(int fencerCount)
    {
        var results = new List<List<Pool>>();

        // Tier 1 — perfect pools (6/7/8)
        GeneratePool(fencerCount, new List<int>(), results, GoodPoolSizes);

        if (results.Count > 0)
            return results;

        // Tier 2 — allow a single fallback size (5)
        GeneratePool(fencerCount, new List<int>(), results, GoodPoolSizes.Concat(new[] {5}).ToArray());

        if (results.Count > 0)
            return results;

        // Tier 3 — if STILL no match, allow 4/3/2
        GeneratePool(fencerCount, new List<int>(), results, GoodPoolSizes.Concat(BadPoolSizes).ToArray());

        return results;
    }
    
    public static List<FencerBase> SnakeFencerList(List<FencerBase> fencers, int numPools)
    {
        var chunkedFencers = fencers.Chunk(numPools).ToList();
        var resultFencers = new List<FencerBase>();

        for (var i = 0; i < chunkedFencers.Count; i++)
        {
            resultFencers.AddRange(i % 2 != 0 
                ? chunkedFencers[i].Reverse() 
                : chunkedFencers[i]);
        }

        return resultFencers;
    }

    public static void PopulatePools(List<FencerBase> fencers, List<Pool> pools)
    {
        var numPools = pools.Count;
        for (var i = 0; i < fencers.Count; i++)
        {
            var pool = pools[i % numPools];
            pool.Fencers.Add(new FencerWithStats(fencers[i], pool.Fencers.Count - 1));
        }
    }

    private static void GeneratePool(
        int remaining,
        List<int> currentSizes,
        List<List<Pool>> results,
        int[] allowedSizes
    )
    {
        if (remaining == 0)
        {
            results.Add(
                currentSizes
                    .Select(s => new Pool { Size = s })
                    .ToList()
            );
            return;
        }

        // Enforce non-decreasing order
        var minSize = currentSizes.Count == 0
            ? 0
            : currentSizes[^1];

        foreach (var size in allowedSizes)
        {
            if (size < minSize || size > remaining)
                continue;

            currentSizes.Add(size);
            GeneratePool(remaining - size, currentSizes, results, allowedSizes);
            currentSizes.RemoveAt(currentSizes.Count - 1);
        }
    }
}