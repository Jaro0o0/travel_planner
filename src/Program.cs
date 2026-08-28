using Services;
using Microsoft.Data.Sqlite;
using Spectre.Console;


// using CreateBackpack;

namespace TravelPlanner
{
    class Program
    {
        

        

        // diaspley equipment
        static async Task Main()
        {
            Console.WriteLine("To better experince you can add your interests do you wanto do that (y/n)");
            string UserChoose = Console.ReadLine().Trim().ToLower() ?? "";

            if(UserChoose == "y")
            {

               

                Console.WriteLine("Choose interest");
                //choose intwrrests
                var choices = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<InterestToChoose>()
                    .Title("Select an [green]environment[/]:")
                    .AddChoices(Enum.GetValues<InterestToChoose>()));

                    var googleTypes = choices.SelectMany(InterestMapper.ToGoogleTypes).ToArray();

                    var response = ShareTravelPalace.Place

                     
                AnsiConsole.MarkupLine($"Deploying to [blue]{choices}[/]");
                
                //Add interst to list            
                List<string> UserInterests = new List<string>();

                //ADD_ITEM
                UserInterests.Add(Console.ReadLine() ?? "");
                

                

                //Add to database
                foreach(var interest in UserInterests )
                {
                    
                    using SqliteConnection connection = new SqliteConnection("Data Source=src/Models/interests.db");
                    connection.Open();

                    using SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO userInterests (Interests) VALUES (@interests)";
                    command.Parameters.AddWithValue("@interests", interest );

                }







            }
            else
            {
                while (true)
                {
                    var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Select an [green]environment[/]:")
                    .AddChoices("Add new trip", "2 Show Tracks list", "4 Exit", "plan your equipment"));


                    Console.WriteLine("###Trials_planner###");
                    // Console.WriteLine("choose option");
                    // Console.WriteLine("1 Add new trip ");
                    //   Console.WriteLine("1 Show planp ");
                    //    Console.WriteLine("1 edit plan ");
                    //    Console.WriteLine("1 remove ");
                    //    budget
                    //    caache
                    //    sync
                    //    suggest
                    //    Polly
                    // xuint
                    // Time ENd and Start

                    // Console.WriteLine("2 Show Tracks list ");
                    // Console.WriteLine("4 Exit");
                    // Console.WriteLine("5: plan your equipment");


                    switch (userChoice)
                    {
                        case "1":
                            Console.WriteLine("CASE: 1");
                            await AddNewTravel.AddNewTrip();
                        
                            break;

                        // case "2":
                        // {
                        //     TracksList.ShowTracksList();
                        //     break;
                        // }
                        
                        
                    
                        
                        case "3":
                            Console.WriteLine("Plan your equipment");
                            break;

                        default:
                            Console.WriteLine("Erro");
                            break;

                        // case "5":
                        //     Backpack.SelectSize();
                        //     Console.WriteLine("");
                        //     break;
                    }
                }
            }
           
        }

    }
}
