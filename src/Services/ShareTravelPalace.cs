using System;
using System.Net.Http;
using System.Text.Json;
using DotNetEnv;



namespace ShareTravelPalace;

//DAtaTypoe
public class PlaceData
{
    public string Name { get; set; }
    public int Population { get; set; }
}

class SahrePlaces
{
    

   public static async Task PlacesInfo()
    {
        Env.Load();
        string? apiKey = Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");

        string url = $"https://places.googleapis.com/v1/places/GyuEmsRBfy61i59si0?fields=addressComponents&key={apiKey}";
        using HttpClient client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string Data = await response.Content.ReadAsStringAsync();
            PlaceData? PlaceInfo = JsonSerializer.Deserialize<PlaceData>(Data);

            Console.WriteLine(PlaceInfo);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}