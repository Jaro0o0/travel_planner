using System;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace Services
{

    public class Lart
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class WeatherMain
    {
        public double temp { get; set; }
        public int humidity { get; set; }
    }

    public class WeatherInfo
    {
        public string description { get; set; }
    }

    public class WindInfo
    {
        public double speed { get; set; }
    }

    public class WeatherResponse
    {
        public WeatherMain main { get; set; }
        public List<WeatherInfo> weather { get; set; }
        public WindInfo wind { get; set; }
    }

    public class WeatherData
    {
        public string? Description { get; set; }
        public double? Temperature { get; set; }
        public double? Wind { get; set; }
    }

    public class WeatherAPi
    {

      
       
        public static async Task<WeatherData?> GetWeather(string? city)
        { 
            Env.Load();
          
            string apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY") ?? "";
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}";
            

            using var clinet = new HttpClient();

            try
            {
                var response = await clinet.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                var locations = JsonSerializer.Deserialize<List<Lart>>(content);

                if (locations != null && locations.Count > 0)
                {
                    var location = locations[0];
                    Console.WriteLine($"Latitude: {location.lat}, Longitude: {location.lon}");

                    string weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={location.lat}&lon={location.lon}&appid={apiKey}";
                    var weatherResponse = await clinet.GetAsync(weatherUrl);
                    // var weatherContent = await weatherResponse.Content.ReadAsStringAsync();
                    var weather = await weatherResponse.Content.ReadFromJsonAsync<WeatherResponse>();

                    return new WeatherData
                    {
                        Description = weather?.weather[0]?.description,
                        Temperature = weather?.main?.temp,
                        Wind = weather?.wind?.speed
                    };
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Weather API error {e}");
            }

            return null;
        }
    }
}
