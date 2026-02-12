using ClubLog.Core.Models;

namespace ClubLog.Core.Interfaces;

public interface IElimService
{
    List<FencerWithStats> GetElimResults(List<Pool> pools, double percentAdvance=1.0);
    List<ElimBout> GetNextRound(List<ElimBout> bouts);
}