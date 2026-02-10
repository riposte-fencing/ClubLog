using ClubLog.Core.Models;
using ClubLog.Core.Models.Requests;
using ClubLog.Core.Repositories;

namespace ClubLog.Core.Services;

public class MemberService(MemberRepository repo)
{
    public void CreateMember(CreateMemberRequest request, string userId)
    {
        
    }
    
    public List<Member> GetMembersForClub(long clubId)
    {
        return new();
    }
}