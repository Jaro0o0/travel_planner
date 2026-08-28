using Services;
using Microsoft.Data.Sqlite;
using Spectre.Console;


    class Program
    {
        

        

        // diaspley equipment
        static async Task Main()
        {
            new DataBase().CreateDatabase();

            Console.WriteLine("To better experince you can add your interests do you wanto do that (y/n)");
            string UserChoose = Console.ReadLine().Trim().ToLower() ?? "";

            if(UserChoose == "y"){
                await UserInterests.SelectUserInterests();

            }            
            else
            {
                //Home Menu 
                while (true)
                {
                    var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Select Option [green]Trials_planner[/]:")
                    .AddChoices("1: Add new trip", "2: Show Travel list", "3: Exit" ));


                    switch (userChoice)
                    {
                        case "1: Add new trip":
                          
                            await AddNewTravel.AddNewTrip();
                        
                            break;
                                            
                        case "2: Show Travel list":
                            
                            Travels.ShowTravels();
                            break;
                        
                        case "3: Exit":
                            Console.WriteLine("Plan your equipment");
                            return;
                            

                        default:
                            Console.WriteLine("Erro");
                            break;

                        
                    }
                }
            }
           
        }

    }
