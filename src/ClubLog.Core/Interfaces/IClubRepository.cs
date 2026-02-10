using ClubLog.Core.Models;
using ClubLog.Core.Models.Enums;

namespace ClubLog.Core.Interfaces;

public interface IClubRepository
{
    Club GetClub(long clubId);
    void CreateClub(string clubName, ClubType type);
}