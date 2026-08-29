using Microsoft.Data.Sqlite;
using ShareTravelPalace;
using Spectre.Console;



namespace  Services
{
    

public class AddNewTravel
{
    public async static Task AddNewTrip()
        {
             
                        var travelKind = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                                .Title("Select Option [green]Trials_planner[/]:")
                                .AddChoices("1: Standard travel", "2: Mountain Teavel"));

                      

                        switch (travelKind)
                        {
                            case "1: Standard travel":
                            {
                            string startDate = "";
                            string endDate = "";
                            while (true)
                                {
                                   
                                    try{
                                        //Start
                                        Console.WriteLine("Start Date (yyyy-mm-dd)");
                                        startDate = Console.ReadLine()?.Trim() ?? "";

                                        //End
                                        Console.WriteLine("End Date (yyyy-mm-dd)");
                                        endDate = Console.ReadLine()?.Trim() ?? "";
                                        break;
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine("Wrong format");
                                        continue;
                                    }
                                };
                            


                                if (string.IsNullOrWhiteSpace(startDate)){
                                        startDate = DateTime.Now.ToString("yyyy-MM-dd");
                                }

                                if(DateTime.Parse(startDate) > DateTime.Parse(endDate)){
                                    Console.WriteLine("Start date cannot be after end date.");
                                    return;
                                }

                                if(startDate == endDate){
                                    Console.WriteLine("Start date and end date cannot be the same.");
                                    return;
                                }
                                
                                if(DateTime.Parse(startDate) < DateTime.Now){
                                    Console.WriteLine("Start date cannot be in the past.");
                                    return;
                                }

                                if(DateTime.Parse(endDate) < DateTime.Now){
                                    Console.WriteLine("End date cannot be in the past.");
                                    return;
                                }
                                //Travel place
                                Console.WriteLine("Where you wanto to travel");
                                string travelPlace = Console.ReadLine()?.Trim() ?? "";
                                //Travel place info
                                if (!string.IsNullOrWhiteSpace(travelPlace))
                                {
                                   Place? PlacesData = await PlacesService.PlacesInfo(travelPlace);

                                   if (PlacesData is null)
                                   {
                                       Console.WriteLine("Nie udało się znaleźć wybranego miejsca.");
                                       return;
                                   }

                                   Console.WriteLine($"Place: {PlacesData?.DisplayName?.Text}");
                                
                                   Console.WriteLine($"Address: {PlacesData?.FormattedAddress}"); 
                                   //Weather Data
                                   var WeatherData = await WeatherAPi.GetWeather(PlacesData?.FormattedAddress);

                                   Console.WriteLine($"Description: {WeatherData?.Description}");
                                   Console.WriteLine($"Temperature: °C { Convert.ToDecimal(Math.Round(WeatherData?.Temperature - 273.15), 2) }");
                                   Console.WriteLine($": {WeatherData?.Wind}");

                                   Console.WriteLine("Weather");
                                    var WeatherChart = new BarChart()
                                        .Label("[bold underline]Fruit Sales[/]")
                                        .AddItem("Temperature:", WeatherData?.Temperature ?? 0, Color.Orange1)
                                        .AddItem("Wind", WeatherData?.Wind ?? 0, Color.Blue);
                                    
                                    AnsiConsole.Write(WeatherChart);
                                    Console.WriteLine($"{WeatherData?.Description}");



                                   //TravelPlan
                                    var travelPlan = new TravelPlan(PlacesData?.DisplayName?.Text, WeatherData );
                                    Console.WriteLine("Recomended Atractions");
                                    var tripContext = new TripContext
                                    {
                                        WeatherCondition = WeatherData?.Description ?? string.Empty,
                                        Temperature = WeatherData?.Temperature ?? 0
                                    };
                                    await travelPlan.GetAtractions(tripContext, Array.Empty<string>());
                                    Console.WriteLine("Rocomende items for backpack");
                                    travelPlan.GenerateBackpack();


                                   //SavePlae
                                   Console.WriteLine("Do you want to save this place? (y/n)");
                                   string userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

                                   //Adding palcee 
                                   if (userChoice == "y")
                                    {

                                        //Equipment 
                                        BackPackFactory.Create("normal");
                                        
                                        using SqliteConnection connection = new SqliteConnection("Data Source=Models/travel.db");
                                        connection.Open();



                                        using SqliteCommand command = connection.CreateCommand();
                                        command.CommandText = "INSERT INTO Trips (Destination, StartDate, EndDate) VALUES (@destination, @startDate, @endDate)";
                                        command.Parameters.AddWithValue("@destination", PlacesData?.DisplayName?.Text ?? travelPlace);
                                        command.Parameters.AddWithValue("@startDate", string.IsNullOrWhiteSpace(startDate) ? DateTime.Now.ToString("yyyy-MM-dd") : startDate);
                                        command.Parameters.AddWithValue("@endDate", string.IsNullOrWhiteSpace(endDate) ? DateTime.Now.ToString("yyyy-MM-dd") : endDate);

                                        command.ExecuteNonQuery();

                                        

                                      
                                    }
                                   else
                                   {
                                       Console.WriteLine("Place not saved.");
                                   }
                                }
                                break;
                            }

                            case  "2: Mountain Teavel":

                               var UserChoose = AnsiConsole.Prompt(
                               new SelectionPrompt<string>()
                                    .Title("This function is not available yet")
                                    .AddChoices("1: Choose another option", "2: Back to home")
                                 );

                                if (UserChoose.Trim().ToLower() == "1: Choose another option")
                                {

                                }
                                else
                                {
                                    return;
                                }
                                break;
                                
                                


                            default:
                                Console.WriteLine("Invalid choice.");
                                break;

                     
                            
                                


                        }
}

}

}
