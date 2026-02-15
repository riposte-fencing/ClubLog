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
    public int Place { get; set; }
    public string PlaceStr { get; set; }
    public bool Tied { get; set; } = false;

    public override string ToString()
    {
        return $"{(string.IsNullOrWhiteSpace(PlaceStr) ? string.Empty : $"{PlaceStr}: ")}{FirstName} {LastName}";
    }
}