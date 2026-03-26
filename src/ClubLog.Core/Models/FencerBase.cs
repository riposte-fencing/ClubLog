namespace ClubLog.Core.Models;

/// <summary>
/// The bare minimum we need to represent a fencer.
/// </summary>
public class FencerBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ClubName { get; set; } = "Unattached";
    public string EpeeRating { get; set; } = "U";
    public string FoilRating { get; set; } = "U";
    public string SaberRating { get; set; } = "U";

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }

    public override bool Equals(object? obj)
    {
        return obj is FencerBase fencer &&
               fencer.FirstName.Equals(FirstName, StringComparison.InvariantCultureIgnoreCase) &&
               fencer.LastName.Equals(LastName, StringComparison.InvariantCultureIgnoreCase) &&
               fencer.ClubName.Equals(ClubName, StringComparison.InvariantCultureIgnoreCase);
    }
}