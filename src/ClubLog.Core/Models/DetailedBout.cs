namespace ClubLog.Core.Models;

public class DetailedBout : BoutBase
{
    public DetailedBout() { Left = null!; Right = null!; }

    public DetailedBout(BoutBase bout, FencerBase left, FencerBase right)
    {
        BoutId = bout.BoutId;
        LeftId = bout.LeftId;
        RightId = bout.RightId;
        LeftScore = bout.LeftScore;
        RightScore = bout.RightScore;

        Left = left;
        Right = right;
    }
    
    public FencerBase Left { get; set; }
    public FencerBase Right { get; set; }

    public override string ToString()
    {
        return $"{Left} v. {Right}: Who won?";
    }
}