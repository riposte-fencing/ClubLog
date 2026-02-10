using Blazored.LocalStorage;
using ClubLog.Core.Models;

namespace ClubLog.Blazor.Simple.BrowserRepositories;

public class PoolRepository(ILocalStorageService localStorage)
{
    public async Task InsertPools(List<Pool> pools, string eventName)
    {
        var keys = new List<string>();
        foreach (var p in pools)
        {
            var key = $"pool:{p.PoolId}";
            await localStorage.SetItemAsync(key, p);
            keys.Add(key);
        }

        await localStorage.SetItemAsync($"{eventName}-event_pools", keys);
    }

    public async Task<List<Pool>> GetPoolsForEvent(string eventName)
    {
        var keys = await localStorage.GetItemAsync<List<string>>($"{eventName}-event_pools");
        if (keys is null || keys.Count == 0) return new();

        var pools = new List<Pool>();
        foreach (var k in keys)
        {
            pools.Add(await localStorage.GetItemAsync<Pool>(k) ?? new());
        }

        return pools;
    }

    public async Task UpdatePool(Pool pool)
    {
        await localStorage.SetItemAsync($"pool:{pool.PoolId}", pool);
    }
}