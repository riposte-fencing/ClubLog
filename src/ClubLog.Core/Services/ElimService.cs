using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;
using ClubLog.Core.Utils;

namespace ClubLog.Core.Services;

public class ElimService : IElimService
{
    public List<FencerWithStats> GetElimResults(List<Pool> pools, double percentAdvance=1)
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
            fencers[i].FinalPlace = i + 1;
            fencers[i].FinalPlaceStr = fencers[i].Tied ? $"T{i + 1}" : $"{i + 1}";
        }

        return fencers;
    }

    public List<ElimBout> GetNextRound(List<ElimBout> bouts)
    {
        throw new NotImplementedException();
    }
    
    
}