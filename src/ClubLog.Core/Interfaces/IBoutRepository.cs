using ClubLog.Core.Models;

namespace ClubLog.Core.Interfaces;

public interface IBoutRepository
{
    Guid CreateBout(Bout newBout, string userId);
    Bout GetBout(Guid id, string userId);
    void UpdateBout(Guid id, Bout updatedBout, string userId);
    void DeleteBout(Guid id, string userId);
    List<Bout> GetBoutsForClub(long clubId, string userId);
    List<Bout> GetBoutsForMember(string userId);
}