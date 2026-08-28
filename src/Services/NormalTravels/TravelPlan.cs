using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace Services
{

    public class Clothes {
        public string Hoodie {get; set;}
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

            IBackpack backpack = BackPackFactory.Create("normal");


            // if (Weather?.Temperature <= 15)
            // {
            //     clothes.Hoodie = "you should get hoodie";
            //     clothes.Boots = "Recomend higher boots";
            //     Console.WriteLine("Do yo want to add hoodie to your backpack (y/n)");
            //     string UserChosoe = Console.ReadLine().Trim().ToLower() ?? "";

            //     if(UserChosoe == "y")
            //     {
            //         backpack.AddItem();

            //     }
                


            // }
            // else
            // {
            //     clothes.Hoodie = "hoodie is not nesescary";


            // }

            // if (Weather?.Wind >= 10)
            // {
            //  Console.WriteLine("Warrning Very Strong wind");
            //  Console.WriteLine("Make sure you are ready");    

            // }  


            // Attractions attractions = new Attractions();

           
           var context new TripContext();


    
        }

    public async  Task GetAtractions(){
        Attractions attractions = new Attractions();
        ShareTravelPalace.Place? place = await ShareTravelPalace.PlacesService.PlacesInfo(TravelLocation);
        ShareTravelPalace.Place? place = await ShareTravelPalace.PlaceAttraactions(TravelLocation,);


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
