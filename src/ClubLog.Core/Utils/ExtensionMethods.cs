using ClubLog.Core.Models;

namespace ClubLog.Core.Utils;

public static class ExtensionMethods
{
    public static string ToPermutationString(this List<Pool> pools)
    {
        var res = pools
            .GroupBy(x => x.Size)
            .OrderBy(x => x.Key)
            .Select(x => $"{x.Count()} {(x.Count() == 1 ? "pool" : "pools")} of {x.Key}");
        return string.Join(", ", res);
    }

    /// <summary>
    /// Used to get just a base fencer object from a class that inherits FencerBase.
    /// </summary>
    /// <param name="fencer"></param>
    /// <returns></returns>
    public static FencerBase ExtractFencerBase(this FencerBase fencer)
    {
        return new FencerBase
        {
            Id = fencer.Id,
            FirstName = fencer.FirstName,
            LastName = fencer.LastName,
            ClubName = fencer.ClubName,
            EpeeRating = fencer.EpeeRating,
            FoilRating = fencer.FoilRating,
            SaberRating = fencer.SaberRating
        };
    }

    public static PoolStats GetStatsForFencer(this List<BoutBase> bouts, Guid fencerId)
    {
        var stats = new PoolStats();
        foreach (var b in bouts)
        {
            if (fencerId == b.LeftId || fencerId == b.RightId)
            {
                if (b.WinnerId == fencerId)
                {
                    stats.Victories++;
                    stats.TouchesScored += b.WinnerScore ?? 0;
                    stats.TouchesReceived += b.LoserScore ?? 0;
                }
                else
                {
                    stats.TouchesScored += b.LoserScore ?? 0;
                    stats.TouchesReceived += b.WinnerScore ?? 0;   
                }
            }
        }
        
        return stats;
    }

    public static bool AllPoolsFinished(this IEnumerable<Pool> pools)
    {
        return pools.All(x => x.Finished);
    }
}