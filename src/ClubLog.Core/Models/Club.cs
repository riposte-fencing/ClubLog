using DbThing.Attributes;
using DbThing.Interfaces;

namespace ClubLog.Core.Models;

public partial class Club : IDbPreProcessModel
{
    #region DbColumns
    
    [DbColumn("ClubId", Required = true)]
    public long ClubId { get; set; }
    
    [DbColumn("ClubName", Required = true)]
    public string ClubName { get; set; }
    
    #endregion
}