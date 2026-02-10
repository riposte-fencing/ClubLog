using System.Globalization;
using ClubLog.Core.Models;
using CsvHelper.Configuration;

public sealed class FencerBaseMap : ClassMap<FencerBase>
{
    public FencerBaseMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.FirstName).Name("firstname", "first", "first_name");
        Map(m => m.LastName).Name("lastname", "last", "last_name");
        Map(m => m.ClubName).Name("clubname", "club", "club_name");

        Map(m => m.EpeeRating)
            .Name("epee")
            .Optional()
            .Default("U");

        Map(m => m.FoilRating)
            .Name("foil")
            .Optional()
            .Default("U");

        Map(m => m.SaberRating)
            .Name("saber")
            .Optional()
            .Default("U");
    }
}