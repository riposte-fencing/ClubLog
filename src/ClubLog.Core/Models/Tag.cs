using DbThing.Attributes;
using DbThing.Interfaces;

namespace ClubLog.Core.Models;

public partial class Tag : IDbPreProcessModel
{
    [DbColumn("TagId", Required = true)]
    public Guid TagId { get; set; }
    
    [DbColumn("TagName", Required = true)]
    public string TagName { get; set; } = string.Empty;
 
    [DbColumn("ClubId", Required = true)]
    public long ClubId { get; set; }

    [DbColumn("CreatedBy", Required = true)]
    public string CreatedBy { get; set; } = string.Empty;
}