using ClubLog.Core.Models;

namespace ClubLog.Core.Interfaces;

public interface IElimService
{
    List<FencerWithStats> GetElimResults(List<Pool> pools);
    List<ElimBout> StartElims(List<FencerWithStats> fencers, double percentAdvance = 1.0);
    List<ElimBout> GetNextRound(List<ElimBout> bouts);
    List<ElimBout> ResolveByeWinners(List<ElimBout> bouts);
    List<List<ElimBout>> GroupIntoRounds(List<ElimBout> bouts);
    List<ElimBout> GetNewNextRoundBouts(List<ElimBout> currentRound, List<ElimBout> existingNextRound);
    void SortNextRound(List<ElimBout> currentRound, List<ElimBout> nextRound);
    List<ElimBout> CascadeWinners(List<List<ElimBout>> rounds, int roundIndex, int boutIndex);
    List<string> GetRoundLabels(List<List<ElimBout>> rounds, int derivedRoundCount);
    List<List<BracketSlot>> GetDerivedRounds(List<List<ElimBout>> rounds);
}