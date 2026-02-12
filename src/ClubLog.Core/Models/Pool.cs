namespace ClubLog.Core.Models;

public class Pool
{
    public Guid PoolId { get; set; } = Guid.NewGuid();
    public string PoolName { get; set; } = string.Empty;
    public Guid EventId { get; set; }
    public int Size { get; set; }
    public List<PoolFencer> Fencers { get; set; } = new();
    public List<BoutBase> Bouts { get; set; } = new();
    public Dictionary<Guid, List<BoutBase>> BoutsForFencers { get; set; } = new();

    public bool Finished
    {
        get
        {
            return Bouts.All(x => x.WinnerId is not null);
        }
    }
}