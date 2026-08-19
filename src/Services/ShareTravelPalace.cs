using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using DotNetEnv;

namespace ShareTravelPalace;

public class DisplayName
{
    public string? Text { get; set; }
}

public class PlaceResult
{
    public DisplayName? DisplayName { get; set; }
    public string? FormattedAddress { get; set; }
}

public class PlaceSearchResponse
{
    public List<PlaceResult>? Places { get; set; }
}

class PlacesService
{
    private static void LoadEnvFile()
    {
        DirectoryInfo? dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (dir != null)
        {
            string envPath = Path.Combine(dir.FullName, ".env");
            if (File.Exists(envPath))
            {
                Env.Load(envPath);
                return;
            }

            dir = dir.Parent;
        }
    }

    public static async Task PlacesInfo(string placeName)
    {
        LoadEnvFile();
        string? apiKey = Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("Error: GOOGLE_PLACES_API_KEY is not set.");
            return;
        }

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Goog-Api-Key", apiKey);
        client.DefaultRequestHeaders.Add("X-Goog-FieldMask", "places.displayName,places.formattedAddress");

        string requestBody = JsonSerializer.Serialize(new { textQuery = placeName });
        using StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync(
                "https://places.googleapis.com/v1/places:searchText", content);
            string data = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: API returned {(int)response.StatusCode}");
                Console.WriteLine(data);
                return;
            }

            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            PlaceSearchResponse? result = JsonSerializer.Deserialize<PlaceSearchResponse>(data, options);

            if (result?.Places == null || result.Places.Count == 0)
            {
                Console.WriteLine($"No places found for '{placeName}'.");
                return;
            }

            foreach (PlaceResult place in result.Places)
            {
                Console.WriteLine($"Name: {place.DisplayName?.Text}");
                Console.WriteLine($"Address: {place.FormattedAddress}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Error parsing response: {e.Message}");
        }
    }
}
