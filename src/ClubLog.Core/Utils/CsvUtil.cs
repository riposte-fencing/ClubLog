using System.Globalization;
using ClubLog.Core.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Components.Forms;

namespace ClubLog.Core.Utils;

public static class CsvUtil
{
    public static async Task<IEnumerable<FencerBase>> ImportFencersFromBrowserCsvFile(IBrowserFile file)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = null, 
            HeaderValidated = null, 
        };
        
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<FencerBaseMap>();
        
        var records = new List<FencerBase>();

        await foreach (var record in csv.GetRecordsAsync<FencerBase>())
        {
            records.Add(record);
        }

        return records;
    }
}