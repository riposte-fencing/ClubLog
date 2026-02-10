using ClubLog.Core.Models;
using Blazored.LocalStorage;

namespace ClubLog.Blazor.Simple.BrowserRepositories;

public class FencerRepository(ILocalStorageService localStorage)
{
    public async Task<FencerBase?> GetFencerAsync(Guid id)
    {
        return (await GetFencersFromLocalStorage())?.FirstOrDefault(x => x.Id == id);
    }

    public async Task<List<FencerBase>> GetFencersAsync(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public async Task AddFencerAsync(FencerBase fencer, string eventName)
    {
        var fencers = await GetFencersFromLocalStorage() ?? new();
        fencers.Add(fencer);
        await localStorage.SetItemAsync($"{eventName}-event_fencers", fencer);
    }

    public Task DeleteFencerAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateFencerAsync(FencerBase updatedFencer)
    {
        throw new NotImplementedException();
    }
    
    private async Task<List<FencerBase>?> GetFencersFromLocalStorage()
    {
        return await localStorage.GetItemAsync<List<FencerBase>>("event_fencers");
    }
}