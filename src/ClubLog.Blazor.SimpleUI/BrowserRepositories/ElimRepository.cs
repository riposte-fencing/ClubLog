using Blazored.LocalStorage;
using ClubLog.Core.Models;

namespace ClubLog.Blazor.Simple.BrowserRepositories;

public class ElimRepository(ILocalStorageService localStorage)
{
    public async Task CreateElimBouts(List<ElimBout> bouts, string eventName)
    {
        var keys = new List<string>();
        foreach (var b in bouts)
        {
            var key = $"elim:{b.BoutId}";
            await localStorage.SetItemAsync(key, b);
            keys.Add(key);
        }

        await localStorage.SetItemAsync($"{eventName}-event_elims", keys);
    }

    public async Task<List<ElimBout>> GetElimBouts(string eventName)
    {
        var keys = await localStorage.GetItemAsync<List<string>>($"{eventName}-event_elims");
        if (keys is null || keys.Count == 0) return new();

        var elims = new List<ElimBout>();
        foreach (var k in keys)
        {
            elims.Add((await localStorage.GetItemAsync<ElimBout>(k))!);
        }

        return elims;
    }

    public async Task AddElimBouts(List<ElimBout> bouts, string eventName)
    {
        var keys = await localStorage.GetItemAsync<List<string>>($"{eventName}-event_elims") ?? new List<string>();
        foreach (var b in bouts)
        {
            var key = $"elim:{b.BoutId}";
            await localStorage.SetItemAsync(key, b);
            keys.Add(key);
        }
        await localStorage.SetItemAsync($"{eventName}-event_elims", keys);
    }

    public async Task UpdateElim(ElimBout bout)
    {
        await localStorage.SetItemAsync($"elim:{bout.BoutId}", bout);
    }
}