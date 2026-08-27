using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace Services
{

    public class Clothes {
        public bool Hoodie {get; set;}
        public string Boots {get; set;} = string.Empty;
        public string Tshirt {get; set;} = string.Empty;

    }

    public class Attractions
    {
        
    }

    public class  TravelPlan
    {
        public string TravelLocation {get; set;} = string.Empty;
        public WeatherData? Weather {get; set;}
        public IBackpack? Backpack {get; set;}
        private readonly Clothes clothes = new Clothes();
        

        public TravelPlan(string? travelLocation, WeatherData? weather, IBackpack? backPack = null)
        {

            TravelLocation = travelLocation ?? string.Empty;
            Weather = weather;
            Backpack = backPack;


            // Clothes Object
            List<Clothes> ClothingList = new List<Clothes>();    

        }

        public void GeneratePlan(){

            if (Weather?.Temperature is <= 15)
            {
                clothes.Hoodie = true;
                clothes.Boots = "Recomend higher boots";



            }

            Attractions attractions = new Attractions();

           


    
        }

    public async  Task GetAtractions(){
        Attractions attractions = new Attractions();
        ShareTravelPalace.Place? place = await ShareTravelPalace.PlacesService.PlacesInfo(TravelLocation);

        if (string.IsNullOrWhiteSpace(place?.Id))
        {
            return;
        }

        string url = $"https://places.googleapis.com/v1/places/{place.Id}";

        using HttpClient client  = new HttpClient();

        try
        {
            var response  =  await client.GetAsync(url);
            var data  = await response.Content.ReadFromJsonAsync<Attractions>();
            Console.WriteLine(data);


        }
        catch (Exception e)
        {
            
            
        }

        


    }
}
}
