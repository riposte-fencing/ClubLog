namespace ClubLog.Core.Models.Requests;

public class CreateMemberRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required long ClubId { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && ClubId > 0;
    }
}