namespace ClubLog.Core.Models;

public class ElimBout(BoutBase bout, FencerBase left, FencerBase right) : DetailedBout(bout, left, right)
{
    /*
     * What place will the winner take in the next DE round, for instance, if we're looking at the 1 v 8 bout in the
     * round of 8, the WinnerPlace will be 1 in the round of 4.
     * If we're looking at the 3 v 6 bout in the 8, the winner place will be 3 in the round of 4, etc.
     *
     * 1----|
     *      | 1----
     * 8----|
     * ...
     * 3----|
     *      | 3----
     * 6----|
     */
    public long? WinnerPlace { get; set; }
    
    // These represent the places the fencers were initially out of pools, they will not change after assignment. 
    public long? RightPlace { get; set; }
    public long? LeftPlace { get; set; }
}