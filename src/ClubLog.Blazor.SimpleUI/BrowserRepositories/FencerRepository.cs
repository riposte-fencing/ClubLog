using ClubLog.Core.Models;
using Blazored.LocalStorage;

namespace ClubLog.Blazor.Simple.BrowserRepositories;

public class FencerRepository(ILocalStorageService localStorage)
{
    public async Task<FencerBase?> GetFencerAsync(Guid id, string eventName)
    {
        return (await GetFencersForEvent(eventName))?.FirstOrDefault(x => x.Id == id);
    }

    public async Task AddFencerToEvent(FencerBase fencer, string eventName)
    {
        var fencers = await GetFencersForEvent(eventName) ?? new();
        fencers.Add(fencer);
        await localStorage.SetItemAsync($"{eventName}-event_fencers", fencers);
    }
    
    public async Task<List<FencerBase>?> GetFencers()
    {
        if (!await localStorage.ContainKeyAsync("all_fencers"))
        {
            return new();
        }
        return await localStorage.GetItemAsync<List<FencerBase>>("all_fencers");
    }

    public async Task<bool> AddFencer(FencerBase fencer)
    {
        var fencers = await GetFencers() ?? new();
        if (fencers.Any(x => x.Equals(fencer)))
        {
            return false;
        }
        
        fencers.Add(fencer);
        await localStorage.SetItemAsync("all_fencers", fencers);
        return true;
    }

    private async Task<List<FencerBase>?> GetFencersForEvent(string eventName)
    {
        return await localStorage.GetItemAsync<List<FencerBase>>($"{eventName}-event_fencers");
    }
}