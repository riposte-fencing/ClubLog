using System.Data;
using System.Diagnostics;
using System.Text.Json;

namespace ClubLog.Blazor.Common.Utils;

public static class DataGrabber
{
    public static async Task<T> Grab<T>(HttpClient http, string path, bool debug=false)
    {
        try
        {
            var rawJson = await http.GetStringAsync(path);
            
            if (debug) Console.WriteLine(rawJson);
            
            var info = JsonSerializer.Deserialize<T>(rawJson);
            if (info is null)
            {
                throw new DataException($"Could not parse {path}");
            }

            return info;
        }
        catch (HttpRequestException)
        {
            throw new DataException($"Could not find {path}");
        }  
    }
}