using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace Services
{
    

    public  static class Travels
    {
  
            public  static void ShowTravels()
            {
                
            
                using SqliteConnection connection = new SqliteConnection("Data Source=Models/travel.db");  
                connection.Open();

                using SqliteCommand command = connection.CreateCommand();

                
                command.CommandText = "SELECT * FROM Trips";

                using SqliteDataReader reader = command.ExecuteReader();
                bool hasTravels = false;

                //Create table for dataa
                var table = new Table();
                   table.AddColumn("[green]Id[/]");
                    table.AddColumn("[green]Destination[/]");
                    table.AddColumn("[green]StartDate[/]");
                    table.AddColumn("[green]EndDate[/]");
                    table.AddColumn("[green]Backpack[/]");

                //Display
                while (reader.Read())
                {


                    hasTravels = true;
                    table.AddRow($"{reader.GetInt64(0)}", reader.GetString(1), reader.GetString(2), reader.GetString(3), "");

                 
                        
                    using SqliteCommand backpackCommand = connection.CreateCommand();
                    backpackCommand.CommandText =
                        "SELECT DISTINCT Item FROM BackpackItems WHERE Destination = @destination ORDER BY Item";
                    backpackCommand.Parameters.AddWithValue("@destination", reader.GetString(1));

                    using SqliteDataReader backpackReader = backpackCommand.ExecuteReader();
                    var backpackItems = new List<string>();
                    while (backpackReader.Read())
                    {
                        backpackItems.Add(backpackReader.GetString(0));
                    }

                    if (backpackItems.Count > 0)
                    {
                        Console.WriteLine("  Plecak:");
                        foreach (string item in backpackItems)
                        {
                            Console.WriteLine($"  - {item}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  Plecak: (brak)");
                    }

                }

                AnsiConsole.Write(table);


                //Dynamic Select options
                string[] promt = hasTravels
                    ? new[] { "1: Delete", "2: Back to home" }
                    : new[] { "1: Back to home" };
                
                //Select
                var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Options [green]Trials_planner[/]:")
                    .AddChoices(promt));

                switch (userChoice)
                {
                    case "1: Delete":
                        DeleteTravel();
                        break;

                    case "2: Back to home":
                        return;

                }

        
                


            }

            //DELETE_METHOD
            public static void DeleteTravel()
            {
                using SqliteConnection connection = new SqliteConnection("Data Source=Models/travel.db");  
                connection.Open();

                using SqliteCommand command = connection.CreateCommand();

                //Polly jesli nie ma nic w bazie
                command.CommandText = "SELECT Id, Destination, StartDate, EndDate FROM Trips";
            
               
            
          
                
            
               

                Dictionary<string, long> travelsToDelete = new Dictionary<string, long>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string travel = $"{reader.GetInt64(0)} | {reader.GetString(1)} | " +
                            $"{reader.GetString(2)} - {reader.GetString(3)}";
                        travelsToDelete.Add(travel, reader.GetInt64(0));
                    }
                }

                if (travelsToDelete.Count == 0)
                {
                    Console.WriteLine("No travels to delete.");
                    var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Do yo wanna add? [green]Trials_planner[/]:")
                    .AddChoices("1: Yes", "2: No" ));
                    
                    
                    

                    return;
                }

                var userChoices = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title("Select travel to delete [green]Trials_planner[/]:")
                        .AddChoices(travelsToDelete.Keys));

                command.CommandText = "DELETE FROM Trips WHERE Id = @id";
                foreach (string travel in userChoices)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", travelsToDelete[travel]);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Selected travels deleted.");

        }

     
     
    }
}
