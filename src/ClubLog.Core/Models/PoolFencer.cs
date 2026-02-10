namespace ClubLog.Core.Models;

public class PoolFencer : FencerBase
{
    // Empty constructor to make System.Text.Json happy. 
    public PoolFencer() { }
    
    public PoolFencer(FencerBase fencer, int opponentCount)
    {
        Id = fencer.Id;
        FirstName = fencer.FirstName;
        LastName = fencer.LastName;
        ClubName = fencer.ClubName;
        EpeeRating = fencer.EpeeRating;
        FoilRating = fencer.FoilRating;
        SaberRating = fencer.SaberRating;

        Stats.Opponents = opponentCount;
    }

    public PoolStats Stats { get; set; } = new();
}