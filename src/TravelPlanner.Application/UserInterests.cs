using Spectre.Console;
using Microsoft.Data.Sqlite;


namespace TravelPlanner.Application
{
    public class UserInterests
    {
        public static async Task SelectUserInterests()
        {
            Console.WriteLine("To better experince you can add your interests do you wanto do that (y/n)");
            string? UserChoose = Console.ReadLine()?.Trim().ToLower();

             Console.WriteLine("Choose interest");
                //choose intwrrests
                var choices = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<InterestToChoose>()
                    .Title("Select an [green]environment[/]:")
                    .AddChoices(Enum.GetValues<InterestToChoose>()));

                     var googleTypes = choices.SelectMany(InterestMapper.ToGoogleTypes).ToArray();

                    

                     
                AnsiConsole.MarkupLine($"Deploying to [blue]{choices}[/]");
                
                //Add interst to list            
                List<string> UserInterests = new List<string>();

                //ADD_ITEM
                UserInterests.Add(Console.ReadLine() ?? "");
                

                using SqliteConnection connection = new SqliteConnection("Data Source=Models/travel.db");
                connection.Open();

                using SqliteCommand command = connection.CreateCommand();
                

                //Add to database
                foreach(var interest in UserInterests )
                {
                    
                   
                    command.CommandText = "INSERT INTO userInterests (Interests) VALUES (@interests)";
                    command.Parameters.AddWithValue("@interests", interest );
                    command.ExecuteNonQuery();

                }

        }
    }
}
