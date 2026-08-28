using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using DotNetEnv;

namespace ShareTravelPalace;

public class Place
{
    public string? Id { get; set; }
    public DisplayName? DisplayName { get; set; }
    public string? FormattedAddress { get; set; }
    public Location? Location { get; set; }
}

public class DisplayName
{
    public string? Text { get; set; }
}

public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class PlacesResponse
{
    public List<Place>? Places { get; set; }
}

public class PlacesService
{
   

    public static async Task<Place?> PlacesInfo(string placeName)
    {
        Env.Load();

        string? apiKey =
            Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");


        using HttpClient client = new HttpClient();

        client.DefaultRequestHeaders.Add("X-Goog-Api-Key", apiKey);

        client.DefaultRequestHeaders.Add(
            "X-Goog-FieldMask",
            "places.id,places.displayName,places.formattedAddress,places.location");

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

    public static async Task<List<Place>> PlaceAttraactions(
        string placeName,
        IEnumerable<string> googleTypes)
    {
        Place? selectedPlace = await PlacesInfo(placeName);

        if (selectedPlace?.Location is null)
        {
            return new List<Place>();
        }

        Env.Load();
        string? apiKey = Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("Missing GOOGLE_PLACES_API_KEY.");
            return new List<Place>();
        }

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Goog-Api-Key", apiKey);
        client.DefaultRequestHeaders.Add(
            "X-Goog-FieldMask",
            "places.id,places.displayName,places.formattedAddress");

        var request = new
        {
            includedTypes = googleTypes.Distinct().ToArray(),
            maxResultCount = 20,
            languageCode = "pl",
            locationRestriction = new
            {
                circle = new
                {
                    center = new
                    {
                        latitude = selectedPlace.Location.Latitude,
                        longitude = selectedPlace.Location.Longitude
                    },
                    radius = 5000.0
                }
            }
        };

        try
        {
            using HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://places.googleapis.com/v1/places:searchNearby",
                request);

            string data = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: API returned {(int)response.StatusCode}");
                Console.WriteLine(data);
                return new List<Place>();
            }

            PlacesResponse? result = JsonSerializer.Deserialize<PlacesResponse>(
                data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result?.Places ?? new List<Place>();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"HTTP Error: {e.Message}");
            return new List<Place>();
        }
        catch (JsonException e)
        {
            Console.WriteLine($"JSON Error: {e.Message}");
            return new List<Place>();
        }
    }
}
