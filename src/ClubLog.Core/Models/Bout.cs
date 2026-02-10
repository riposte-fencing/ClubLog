using ClubLog.Core.Models.Enums;
using DbThing.Attributes;
using DbThing.Interfaces;

namespace ClubLog.Core.Models;

public partial class Bout : IDbPreProcessModel
{
    #region DbColumns
    
    [DbColumn("BoutId", Required = true)]
    public Guid BoutId { get; set; }

    [DbColumn("CreatedBy", Required = true)]
    public string CreatedByUserId { get; set; } = string.Empty;

    [DbColumn("CreatedByUserName")]
    public string UserName { get; set; }
    
    [DbColumn("OwnerScore", Required = true)]
    public short Score { get; set; }
    
    [DbColumn("OpponentScore", Required = true)]
    public short OpponentScore { get; set; }
    
    [DbColumn("Notes")]
    public string? Notes { get; set; }
    
    [DbColumn("CoachNotes")]
    public string? CoachNotes { get; set; }
    
    [DbColumn("CreatedDate", Required = true)]
    public DateTime CreatedDate { get; set; }
    
    [DbColumn("BoutType", Required = true)]
    public int BoutType { get; set; }

    [DbColumn("EventType", Required = true)]
    public int EventType { get; set; }

    #endregion
}