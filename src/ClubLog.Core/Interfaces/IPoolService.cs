using ClubLog.Core.Models;

namespace ClubLog.Core.Interfaces;

public interface IPoolService
{
    public void CreateBoutsForPool(Pool pool);
    public void CreateBoutsForPool(Pool pool, Dictionary<int, List<BoutPair>> orders);
}