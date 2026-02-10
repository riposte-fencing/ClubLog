using ClubLog.Core.Models;

namespace ClubLog.Core.Interfaces;

public interface IFencerRepository
{
    public FencerBase? GetFencer(Guid id);
    public List<FencerBase> GetFencers(IEnumerable<Guid> ids);
    public void AddFencer(FencerBase fencer);
    public void DeleteFencer(Guid id);
    public void UpdateFencer(FencerBase updatedFencer);
    
    public Task<FencerBase?> GetFencerAsync(Guid id);
    public Task<List<FencerBase>> GetFencersAsync(IEnumerable<Guid> ids);
    public Task AddFencerAsync(FencerBase fencer);
    public Task DeleteFencerAsync(Guid id);
    public Task UpdateFencerAsync(FencerBase updatedFencer);
}