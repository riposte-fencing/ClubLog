namespace ClubLog.Core.Models;

public class BoutBase
{
    public Guid BoutId { get; set; } = Guid.NewGuid();
    public Guid LeftId { get; set; }
    public Guid RightId { get; set; }
    public int? LeftScore { get; set; }
    public int? RightScore { get; set; }
    
    private Guid? _manualWinnerId;

    public Guid? WinnerId
    {
        get
        {
            if (LeftScore != RightScore)
                return LeftScore > RightScore ? LeftId : RightId;
            return _manualWinnerId;
        }
        set { _manualWinnerId = value; }
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

    public void SetWinner(Guid winnerId)
    {
        if (winnerId != RightId && winnerId != LeftId)
        {
            throw new ArgumentException("winnerId must be LeftId or RightId");
        }

        WinnerId = winnerId;
    }

    public override string ToString()
    {
        return $"{LeftId}";
    }
}