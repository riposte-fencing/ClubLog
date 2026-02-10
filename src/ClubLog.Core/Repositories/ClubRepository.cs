using ClubLog.Core.Interfaces;
using ClubLog.Core.Models;
using ClubLog.Core.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ClubLog.Core.Repositories;

public class ClubRepository(IConfiguration configuration, ILogger logger) : BaseRepository(configuration, logger), IClubRepository
{
    public Club GetClub(long clubId)
    {
        throw new NotImplementedException();
    }

    public void CreateClub(string clubName, ClubType type)
    {
        Execute("proc_CreateClub", [
            new SqlParameter("@clubName", clubName),
            new SqlParameter("@clubType", type)
        ]);
    }
}