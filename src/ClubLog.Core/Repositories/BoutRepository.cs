using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ClubLog.Core.Repositories;

public class BoutRepository(IConfiguration configuration, ILogger logger) : BaseRepository(configuration, logger), IBoutRepository
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
}