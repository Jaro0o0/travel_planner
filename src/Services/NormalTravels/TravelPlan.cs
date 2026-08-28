using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Spectre.Console;

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
        var backpackContext = new BackPackContext
        {
            WeatherCondition = Weather?.Description ?? string.Empty,
            Temperature = Weather?.Temperature ?? 0
        };

        var recommendedItems = new BackpackContextEngine()
            .GetRecommendedItems(backpackContext);

        if (recommendedItems.Count == 0)
        {
            Console.WriteLine("Brak dodatkowych rekomendacji do plecaka.");
            return;
        }

        Console.WriteLine("Polecane rzeczy do plecaka:");
        foreach (string item in recommendedItems)
        {
            Console.WriteLine($"- {item}");
        }

        //Chosee recomended items to save
        var choices = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<InterestToChoose>()
                    .Title("Select an [green]environment[/]:")
                    .AddChoices(recommendedItems));
            BackPackFactory.Create("normal");
            

        //Choose oprion savae
        var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Do you want to save this backapck list [green]Trials_planner[/]:")
                    .AddChoices("1: Yes", "2: No"));

            switch (userChoice)
            {
                case "1: Yes":


                    
            }

    }
  
}
}
