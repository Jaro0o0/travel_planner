using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Spectre.Console;
using Microsoft.Data.Sqlite;
using TravelPlanner.Domain.Models;
using TravelPlanner.Infrastructure.Weather;
using TravelPlanner.Infrastructure.Persistence;

namespace TravelPlanner.Application
{

    public class Clothes {
        public string Hoodie {get; set;} = string.Empty;
        public string Boots {get; set;} = string.Empty;
        public string Tshirt {get; set;} = string.Empty;

    }

    public class Attractions
    {
        
    }

    public class  TravelPlan
    {
        public string TravelLocation {get; set;} = "";
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
        var places = await PlacesService.PlaceAttraactions(
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
            Console.WriteLine("No attractions found matching your preferences..");
            return;
        }

        Console.WriteLine("Recommended attractions:");
        foreach (var item in rankedPlaces)
        {
            Console.WriteLine($"- {item.Place.DisplayName?.Text}");
            Console.WriteLine($"  Address: {item.Place.FormattedAddress}");
            Console.WriteLine($"  Score: {item.Score}");
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
            Console.WriteLine("No additional backpack recommendations.");
        }
      

        //Chosee recomended items to save
        List<string> choices = new List<string>();
        if (recommendedItems.Count > 0)
        {
            var selectedRecommendedItems = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Select recommended items for your backpack:")
                    .AddChoices(recommendedItems.Append("0: Exit")));

            if (selectedRecommendedItems.Contains("0: Exit"))
                {
                    
                    return;
                }        

            choices.AddRange(selectedRecommendedItems);
        }

        Console.WriteLine("Add your own items. Press Enter without text to finish.");
        while (true)
        {
            string customItem = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(customItem))
            {
                break;
            }

            choices.Add(customItem);
        }

        //Choose oprion savae
        var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Do you want to save this backapck list [green]Trials_planner[/]:")
                    .AddChoices("1: Yes", "2: No"));

            switch (userChoice)
            {
                case "1: Yes":
                    if (choices.Count == 0)
                    {
                        Console.WriteLine("No backpack items selected.");
                        break;
                    }

                    using (SqliteConnection connection = new SqliteConnection(DataBase.ConnectionString))
                    {
                        connection.Open();

                        using SqliteCommand command = connection.CreateCommand();
                        command.CommandText = "INSERT INTO BackpackItems (Destination, Item, CreatedAt) VALUES (@destination, @item, @createdAt)";

                        foreach (string item in choices.Distinct())
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@destination", TravelLocation);
                            command.Parameters.AddWithValue("@item", item);
                            command.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine("Backpack saved.");
                    break;

                case "2: No":
                    break;
            }

    }
  
}
}
