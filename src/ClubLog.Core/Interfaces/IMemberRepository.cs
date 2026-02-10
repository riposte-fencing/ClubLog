using ClubLog.Core.Models;
using ClubLog.Core.Models.Requests;

namespace ClubLog.Core.Interfaces;

public interface IMemberRepository
{
    void CreateMember(CreateMemberRequest request, string userId);
    List<Member> GetMembersForClub(long clubId, string userId);
}