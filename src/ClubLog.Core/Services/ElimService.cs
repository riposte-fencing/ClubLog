using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;
using ClubLog.Core.Utils;

namespace ClubLog.Core.Services;

public class ElimService : IElimService
{
    private static Guid ByeGuid = Guid.Empty;
    private static int ByePlace = -1;
    private static FencerWithStats ByeFencer = new()
    {
        Id = ByeGuid,
        Place = ByePlace,
        PlaceStr = string.Empty,
        FirstName = "BYE",
        LastName = string.Empty
    };
    private static readonly int[] BracketSizes = { 2, 4, 8, 16, 32, 64, 128 };
    
    public List<FencerWithStats> GetElimResults(List<Pool> pools)
    {
        var fencerHs = new HashSet<Guid>();
        var fencers = pools
            .SelectMany(x => x.Fencers)
            .Where(x => fencerHs.Add(x.Id))
            .ToList();

        var random = new Random();
        fencers.Sort((x, y) => y.CompareTo(x, random));

        for (var i = 0; i < fencers.Count; i++)
        {
            fencers[i].Place = i + 1;
            fencers[i].PlaceStr = fencers[i].Tied ? $"T{i + 1}" : $"{i + 1}";
        }

        return fencers;
    }

    public List<ElimBout> StartElims(List<FencerWithStats> fencers, double percentAdvance=1.0)
    {
        var bracketSize = BracketSizes.First(x => fencers.Count <= x);
        fencers = fencers.OrderBy(x => x.Place).ToList();

        var result = new List<ElimBout>();
        var advancing = (int)Math.Ceiling(fencers.Count * percentAdvance);
        var seedOrder = GetBracketSeedOrder(bracketSize);

        for (var i = 0; i < seedOrder.Length; i += 2)
        {
            var topSeed = seedOrder[i];
            var bottomSeed = seedOrder[i + 1];

            var fotr = topSeed <= advancing ? fencers[topSeed - 1] : ByeFencer;
            var fotl = bottomSeed <= advancing ? fencers[bottomSeed - 1] : ByeFencer;

            if (fotr.Id == ByeGuid && fotl.Id == ByeGuid) continue;

            var boutBase = new BoutBase
            {
                LeftId = fotl.Id,
                RightId = fotr.Id
            };
            result.Add(new ElimBout(boutBase, fotl, fotr)
            {
                LeftPlace = fotl.Place,
                RightPlace = fotr.Place,
                WinnerPlace = fotr.Id != ByeGuid ? fotr.Place : fotl.Place
            });
        }

        foreach (var bout in result)
        {
            if (bout.LeftId == ByeGuid)       bout.WinnerId = bout.RightId;
            else if (bout.RightId == ByeGuid) bout.WinnerId = bout.LeftId;
        }

        return result;
    }

    private static int[] GetBracketSeedOrder(int bracketSize)
    {
        var order = new[] { 1, 2 };
        var current = 2;
        while (current < bracketSize)
        {
            current *= 2;
            var next = new int[current];
            for (var i = 0; i < order.Length; i++)
            {
                next[i * 2]     = order[i];
                next[i * 2 + 1] = current + 1 - order[i];
            }
            order = next;
        }
        return order;
    }

    public List<ElimBout> GetNextRound(List<ElimBout> bouts)
    {
        var result = new List<ElimBout>();
        for (var i = 0; i + 1 < bouts.Count; i += 2)
        {
            var boutA = bouts[i];
            var boutB = bouts[i + 1];

            var winnerA = GetWinnerFencer(boutA);
            var winnerB = GetWinnerFencer(boutB);

            if (winnerA == null || winnerB == null) continue;

            // Lower WinnerPlace = better seed = top seed (Right)
            var aIsTop = (boutA.WinnerPlace ?? long.MaxValue) <= (boutB.WinnerPlace ?? long.MaxValue);
            var fotr      = aIsTop ? winnerA : winnerB;
            var fotl      = aIsTop ? winnerB : winnerA;
            var topPlace  = aIsTop ? boutA.WinnerPlace : boutB.WinnerPlace;
            var botPlace  = aIsTop ? boutB.WinnerPlace : boutA.WinnerPlace;

            var boutBase = new BoutBase { LeftId = fotl.Id, RightId = fotr.Id };
            result.Add(new ElimBout(boutBase, fotl, fotr)
            {
                Round        = bouts[i].Round + 1,
                RightPlace   = topPlace,
                LeftPlace    = botPlace,
                WinnerPlace  = topPlace
            });
        }
        return result;
    }

    private static FencerBase? GetWinnerFencer(ElimBout bout)
    {
        if (bout.WinnerId == null)          return null;
        if (bout.WinnerId == bout.LeftId)   return bout.Left;
        if (bout.WinnerId == bout.RightId)  return bout.Right;
        return null;
    }
    
    
}