using ClubLog.Core.Models.Enums;
using DbThing.Attributes;
using DbThing.Interfaces;

namespace ClubLog.Core.Models;

public partial class Member : IDbPreProcessModel
{
    #region DbColumns

    [DbColumn("MemberId", Required = true)]
    public string MemberId { get; set; } = string.Empty;
    
    [DbColumn("FirstName", Required = true)]
    public string FirstName { get; set; } = string.Empty;
    
    [DbColumn("LastName", Required = true)]
    public string LastName { get; set; } = string.Empty; 
    
    [DbColumn("ClubId", Required = true)]
    public long ClubId { get; set; }
    
    [DbColumn("Role", Required = true)]
    public int UserRole { get; set; }
    
    #endregion
    
    public string Name => $"{FirstName} {LastName}";

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(MemberId) && !string.IsNullOrWhiteSpace(FirstName) &&
               !string.IsNullOrWhiteSpace(LastName);
    }
}