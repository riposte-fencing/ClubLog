namespace ClubLog.Core.Models;

public class BoutBase
{
    public required Guid LeftId { get; set; }
    public required Guid RightId { get; set; }
    public int? LeftScore { get; set; }
    public int? RightScore { get; set; }
    
    public Guid? WinnerId
    {
        get
        {
            if (LeftScore == RightScore) return null;
            return LeftScore > RightScore ? LeftId : RightId;
        }
    }

    public int? WinnerScore
    {
        get
        {
            if (LeftScore == RightScore) return null;
            return WinnerId == LeftId ? LeftScore : RightScore;
        }
    }

    public int? LoserScore
    {
        get
        {
            if (LeftScore == RightScore) return null;
            return WinnerId == LeftId ? RightScore : LeftScore;
        }
    }
}