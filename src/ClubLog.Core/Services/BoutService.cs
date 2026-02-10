using System.Text.Json;
using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;

namespace ClubLog.Core.Services;

public class BoutService(IBoutRepository repo) : IBoutService
{
    public Guid CreateBout(Bout newBout, string userId)
    {
        throw new NotImplementedException();
    }

    public Bout GetBout(Guid id, string userId)
    {
        throw new NotImplementedException();
    }

    public void UpdateBout(Guid id, Bout updatedBout, string userId)
    {
        throw new NotImplementedException();
    }

    public void DeleteBout(Guid id, string userId)
    {
        throw new NotImplementedException();
    }

    public List<Bout> GetBoutsForClub(long clubId, string userId)
    {
        throw new NotImplementedException();
    }

    public List<Bout> GetBoutsForMember(string userId)
    {
        throw new NotImplementedException();
    }

    public void LeaveCoachComment(Guid id, string note, string userId)
    {
        throw new NotImplementedException();
    }
}