using ClubLog.Core.Models;

namespace ClubLog.Core.Utils;

public static class ElimBoutExtensions
{
    public static FencerBase? GetWinnerFencer(this ElimBout bout)
    {
        if (bout.WinnerId == null)
        {
            return null;
        }

        if (bout.WinnerId == bout.LeftId)
        {
            return bout.Left;
        }
        
        return bout.WinnerId == bout.RightId 
            ? bout.Right 
            : null;
    }
}

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

    public static PoolStats GetStatsForFencer(this List<BoutBase> bouts, Guid fencerId, HashSet<Guid>? excludedIds = null)
    {
        var stats = new PoolStats();
        foreach (var b in bouts)
        {
            if (fencerId == b.LeftId || fencerId == b.RightId)
            {
                var opponentId = fencerId == b.LeftId ? b.RightId : b.LeftId;
                if (excludedIds is not null && excludedIds.Contains(opponentId)) continue;

                stats.Opponents++;
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

    public static int CompareTo(this FencerWithStats original, FencerWithStats other, Random rand)
    {
        if (original.Stats.Vm > other.Stats.Vm) return 1;
        if (original.Stats.Vm < other.Stats.Vm) return -1;
        if (original.Stats.Indicator > other.Stats.Indicator) return 1;
        if (original.Stats.Indicator < other.Stats.Indicator) return -1;
        if (original.Stats.TouchesScored > other.Stats.TouchesScored) return 1;
        if (original.Stats.TouchesScored < other.Stats.TouchesScored) return -1;
        original.Tied = true;
        other.Tied = true;
        return new[] { -1, 1 }[rand.Next(2)];
    }
 }