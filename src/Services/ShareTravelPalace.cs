using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using DotNetEnv;

namespace ShareTravelPalace;

public class Place
{
    public DisplayName? DisplayName { get; set; }
    public string? FormattedAddress { get; set; }
}

public class DisplayName
{
    public string? Text { get; set; }
}

public class PlacesResponse
{
    public List<Place>? Places { get; set; }
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

    public static async Task<Place?> PlacesInfo(string placeName)
    {
        LoadEnvFile();

        string? apiKey =
            Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("Error: GOOGLE_PLACES_API_KEY is not set.");
            return null;
        }

        using HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Add("X-Goog-Api-Key", apiKey);

        client.DefaultRequestHeaders.Add(
            "X-Goog-FieldMask",
            "places.displayName,places.formattedAddress");

        string requestBody = JsonSerializer.Serialize(new
        {
            textQuery = placeName
        });

        using StringContent content = new StringContent(
            requestBody,
            Encoding.UTF8,
            "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync(
                "https://places.googleapis.com/v1/places:searchText",
                content);

            string data = await response.Content.ReadAsStringAsync();

            // Sprawdzamy, czy API zwróciło błąd
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    $"Error: API returned {(int)response.StatusCode}");

                Console.WriteLine(data);

                return null;
            }

            // Zamiana JSON -> PlacesResponse
            PlacesResponse? result =
                JsonSerializer.Deserialize<PlacesResponse>(
                    data,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            // Zwracamy pierwsze znalezione miejsce
            return result?.Places?.FirstOrDefault();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"HTTP Error: {e.Message}");
            return null;
        }
        catch (JsonException e)
        {
            Console.WriteLine($"JSON Error: {e.Message}");
            return null;
        }
        
    }