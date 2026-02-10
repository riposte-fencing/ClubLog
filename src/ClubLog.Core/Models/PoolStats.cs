namespace ClubLog.Core.Models;

public class PoolStats
{
    public int Victories { get; set; }
    public int TouchesScored { get; set; }
    public int TouchesReceived { get; set; }
    public int Indicator => TouchesScored - TouchesReceived;
    public int Opponents { get; set; }
    public double Vm => Opponents == 0 ? 0 : Victories / (double)Opponents;
}