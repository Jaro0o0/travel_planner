using System;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
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
    public class Latitiude
    {

      
       
        public static async Task GetLatitiude(string city)
        { 
            Env.Load();
          
            string apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY") ?? "";
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q=${city}&limit=1&appid=${apiKey}";
            

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
                    var weatherContent = await weatherResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(weatherContent);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Weather API error {e}");
            }



        }
    }
}
