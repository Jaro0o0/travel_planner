using System;
using System.Net.Http;

namespace Services
{
    public class MountainWeatherAPiCall
    {

     public static async Task GetWeaather(double lat, double lon, string apiKey)
        {
            using var client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/4.0/onecall/current?lat={lat}&lon={lon}&appid={apiKey}";

            try
            {
                var response = client.GetAsync(url);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine($"Weather API error {e}");
            }

        }  
    }
}

