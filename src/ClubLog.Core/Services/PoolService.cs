using System.Text.Json;
using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;

namespace ClubLog.Core.Services;

public class PoolService : IPoolService
{
    public void CreateBoutsForPool(Pool pool, Dictionary<int, List<BoutPair>> orders)
    {
        if (!orders.TryGetValue(pool.Fencers.Count, out var bouts))
        {
            throw new ArgumentException("Invalid number of fencers in pool.");
        }

        pool.Bouts = bouts
            .Select(b => new BoutBase { LeftId = pool.Fencers[b.Left - 1].Id, RightId = pool.Fencers[b.Right - 1].Id })
            .ToList();
    }

    public void CreateBoutsForPool(Pool pool)
    {
        var orders = JsonSerializer.Deserialize<Dictionary<int, List<BoutPair>>>(File.ReadAllText("bout_order.json"));
        if (orders is null || !orders.TryGetValue(pool.Fencers.Count, out var bouts))
        {
            throw new ArgumentException("Invalid number of fencers in pool.");
        }

        pool.Bouts = bouts
            .Select(b => new BoutBase { LeftId = pool.Fencers[b.Left - 1].Id, RightId = pool.Fencers[b.Right - 1].Id })
            .ToList();

    }
}