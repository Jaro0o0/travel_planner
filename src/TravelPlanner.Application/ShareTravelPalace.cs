using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DotNetEnv;
using TravelPlanner.Domain.Models;

namespace TravelPlanner.Application;


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

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Env.Load("../.env");
            apiKey = Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("Missing GOOGLE_PLACES_API_KEY.");
            return null;
        }


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

           
            PlacesResponse? result =
                JsonSerializer.Deserialize<PlacesResponse>(
                    data,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

           
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
            Env.Load("../.env");
            apiKey = Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("Missing GOOGLE_PLACES_API_KEY.");
            return new List<Place>();
        }

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Goog-Api-Key", apiKey);
        client.DefaultRequestHeaders.Add(
            "X-Goog-FieldMask",
            "places.id,places.displayName,places.formattedAddress,places.types,places.primaryType");

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
