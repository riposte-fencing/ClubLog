using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;
using ClubLog.Core.Utils;

namespace ClubLog.Core.Services;

public class ElimService : IElimService
{
    private static readonly Guid ByeGuid = Guid.Empty;
    private const int ByePlace = -1;

    private static readonly FencerWithStats ByeFencer = new()
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
            .Where(x => fencerHs.Add(x.Id) && !x.Excluded)
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

            var fotr = topSeed <= advancing 
                ? fencers[topSeed - 1] 
                : ByeFencer;
            var fotl = bottomSeed <= advancing 
                ? fencers[bottomSeed - 1] 
                : ByeFencer;

            if (fotr.Id == ByeGuid && fotl.Id == ByeGuid)
            {
                continue;
            }

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
            if (bout.LeftId == ByeGuid)
            {
                bout.WinnerId = bout.RightId;
            }
            else if (bout.RightId == ByeGuid)
            {
                bout.WinnerId = bout.LeftId;
            }
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
                next[i * 2] = order[i];
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

            var winnerA = boutA.GetWinnerFencer();
            var winnerB = boutB.GetWinnerFencer();

            if (winnerA == null || winnerB == null)
            {
                continue;
            }

            var (fotr, fotl, topPlace, botPlace) = OrderByPlace(boutA, winnerA, boutB, winnerB);

            var boutBase = new BoutBase { LeftId = fotl.Id, RightId = fotr.Id };
            result.Add(new ElimBout(boutBase, fotl, fotr)
            {
                Round = bouts[i].Round + 1,
                RightPlace = topPlace,
                LeftPlace = botPlace,
                WinnerPlace = topPlace
            });
        }
        return result;
    }

    public List<ElimBout> ResolveByeWinners(List<ElimBout> bouts)
    {
        var changed = new List<ElimBout>();
        foreach (var bout in bouts.Where(bout => bout.WinnerId == null))
        {
            if (bout.LeftId != Guid.Empty && bout.RightId != Guid.Empty)
            {
                continue;
            }
            
            if (bout.LeftId == Guid.Empty)
            {
                bout.WinnerId = bout.RightId;
            }
            else if (bout.RightId == Guid.Empty)
            {
                bout.WinnerId = bout.LeftId;
            }
            
            changed.Add(bout);
        }
        return changed;
    }

    public List<List<ElimBout>> GroupIntoRounds(List<ElimBout> bouts)
    {
        return bouts
            .GroupBy(b => b.Round)
            .OrderBy(g => g.Key)
            .Select(g => g.ToList())
            .ToList();
    }

    public List<ElimBout> GetNewNextRoundBouts(List<ElimBout> currentRound, List<ElimBout> existingNextRound)
    {
        var existingRightPlaces = existingNextRound
            .Where(b => b.RightPlace.HasValue)
            .Select(b => b.RightPlace!.Value)
            .ToHashSet();

        var toAdd = new List<(int PairIdx, ElimBout Bout)>();

        for (var i = 0; i + 1 < currentRound.Count; i += 2)
        {
            var boutA = currentRound[i];
            var boutB = currentRound[i + 1];

            var winnerA = boutA.GetWinnerFencer();
            var winnerB = boutB.GetWinnerFencer();
            if (winnerA == null || winnerB == null)
            {
                continue;
            }

            var (fotr, fotl, topPlace, botPlace) = OrderByPlace(boutA, winnerA, boutB, winnerB);

            if (topPlace.HasValue && existingRightPlaces.Contains(topPlace.Value)) continue;

            var boutBase = new BoutBase { LeftId = fotl.Id, RightId = fotr.Id };
            toAdd.Add((i / 2, new ElimBout(boutBase, fotl, fotr)
            {
                Round = currentRound[i].Round + 1,
                RightPlace = topPlace,
                LeftPlace = botPlace,
                WinnerPlace = topPlace,
            }));
        }

        return toAdd
            .OrderBy(x => x.PairIdx)
            .Select(x => x.Bout)
            .ToList();
    }

    public List<ElimBout> CascadeWinners(List<List<ElimBout>> rounds, int roundIndex, int boutIndex)
    {
        var changed = new List<ElimBout>();
        var round = roundIndex;
        var idx = boutIndex;

        while (round + 1 < rounds.Count)
        {
            var nextRoundIdx = round + 1;
            var pairStart = (idx / 2) * 2;

            if (pairStart + 1 >= rounds[round].Count)
            {
                break;
            }

            var boutA = rounds[round][pairStart];
            var boutB = rounds[round][pairStart + 1];

            var winnerA = boutA.GetWinnerFencer();
            var winnerB = boutB.GetWinnerFencer();
            if (winnerA == null || winnerB == null)
            {
                break;
            }

            var (newRight, newLeft, topPlace, botPlace) = OrderByPlace(boutA, winnerA, boutB, winnerB);

            var nextBout = rounds[nextRoundIdx].FirstOrDefault(b => b.RightPlace == topPlace);
            if (nextBout == null)
            {
                break;
            }

            if (nextBout.RightId == newRight.Id && nextBout.LeftId == newLeft.Id)
            {
                break;
            }

            nextBout.RightId = newRight.Id;
            nextBout.Right = newRight;
            nextBout.LeftId = newLeft.Id;
            nextBout.Left = newLeft;
            nextBout.RightPlace = topPlace;
            nextBout.LeftPlace = botPlace;
            nextBout.WinnerPlace = nextBout.RightPlace;
            nextBout.RightScore = null;
            nextBout.LeftScore = null;
            nextBout.WinnerId = null;

            changed.Add(nextBout);

            round = nextRoundIdx;
            idx = rounds[nextRoundIdx].IndexOf(nextBout);
        }

        return changed;
    }

    private static (FencerBase Top, FencerBase Bot, long? TopPlace, long? BotPlace) OrderByPlace(ElimBout boutA, FencerBase winnerA, ElimBout boutB, FencerBase winnerB)
    {
        return (boutA.WinnerPlace ?? long.MaxValue) <= (boutB.WinnerPlace ?? long.MaxValue)
            ? (winnerA, winnerB, boutA.WinnerPlace, boutB.WinnerPlace)
            : (winnerB, winnerA, boutB.WinnerPlace, boutA.WinnerPlace);
    }
}