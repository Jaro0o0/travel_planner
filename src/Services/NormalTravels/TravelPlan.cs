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
        private readonly ContextEngine _scoringService = new ContextEngine();
        

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

           
           var context = new TripContext();


    
        }

    public async Task GetAtractions(TripContext context, IEnumerable<string> googleTypes)
    {
        var places = await ShareTravelPalace.PlacesService.PlaceAttraactions(
            TravelLocation,
            googleTypes);

        var rankedPlaces = places
            .Select(p => new { Place = p, Score = _scoringService.ScorePlace(p, context) })
            .Where(x => x.Score > 0)
            .OrderByDescending(x => x.Score)
            .Take(5)
            .ToList();

        if (rankedPlaces.Count == 0)
        {
            Console.WriteLine("Nie znaleziono atrakcji pasujących do Twoich preferencji.");
            return;
        }

        Console.WriteLine("Polecane atrakcje:");
        foreach (var item in rankedPlaces)
        {
            Console.WriteLine($"- {item.Place.DisplayName?.Text}");
            Console.WriteLine($"  Adres: {item.Place.FormattedAddress}");
            Console.WriteLine($"  Wynik: {item.Score}");
        }
    }

    public void GenerateBackpack()
    {
            
    }
  
}
}
