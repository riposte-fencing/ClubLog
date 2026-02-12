namespace ClubLog.Core.Models;

public class FencerWithStats : FencerBase
{
    // Empty constructor to make System.Text.Json happy. 
    public FencerWithStats() { }
    
    public FencerWithStats(FencerBase fencer, int opponentCount)
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
    public int FinalPlace { get; set; }
    public string FinalPlaceStr { get; set; }
    public bool Tied { get; set; } = false;
}