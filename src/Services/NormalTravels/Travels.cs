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

                //Polly jesli nie ma nic w bazie
                command.CommandText = "SELECT Id, Destination, StartDate, EndDate FROM Trips";

                using SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"{reader.GetInt64(0)} | {reader.GetString(1)} | " +
                        $"{reader.GetString(2)} - {reader.GetString(3)}");
                }

                //Select
                var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Options [green]Trials_planner[/]:")
                    .AddChoices( "1: Delete", "2: Back to home" ));

                switch (userChoice)
                {
                    case "1: Edit":
                        DeleteTravel();
                        break;

                    case "2: Back to home":
                        return;

                }

        
                


            }

            public static void DeleteTravel()
            {
                using SqliteConnection connection = new SqliteConnection("Data Source=Models/travel.db");  
                connection.Open();

                using SqliteCommand command = connection.CreateCommand();

                //Polly jesli nie ma nic w bazie
                command.CommandText = "SELECT Id, Destination, StartDate, EndDate FROM Trips";

                using SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                        var userChoices = AnsiConsole.Prompt(
                        new MultiSelectionPrompt<string>()
                        .Title("Select travel to delete [green]Trials_planner[/]:")
                        .AddChoices(  $"{reader.GetInt64(0)} | {reader.GetString(1)} | " +
                        $"{reader.GetString(2)} - {reader.GetString(3)}" ));

                        //Delete querry
                        command.CommandText  = $"DELETE FROM Trips WHERER Id = {userChoices }";
                    
                }


            

        }
    }
}
