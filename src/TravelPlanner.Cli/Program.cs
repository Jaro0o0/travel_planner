
using Spectre.Console;
using TravelPlanner.Application;
using TravelPlanner.Infrastructure.Persistence;


    class Program
    {
        

        static void Main()
        {
            new DataBase().CreateDatabase();

            {
                //Home Menu 
                while (true)
                {

                    //Logo
                    var appName = new FigletText("TravelPlanner")
{
                        Color = Color.Blue,
                        Justification = Justify.Center
                    };
                    
                    var version = new Text("Version Beta", new Style(Color.Grey))
                    {
                        Justification = Justify.Center
                    };
                    
                    AnsiConsole.Write(appName);
                    AnsiConsole.Write(version);
                    

                    var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Select [green]Option[/]:")
                    .AddChoices("1: Add new trip", "2: Show Travel list", "3: Exit" ));


                    switch (userChoice)
                    {
                        case "1: Add new trip":
                          
                            AddNewTravel.AddNewTrip().GetAwaiter().GetResult();
                        
                            break;
                                            
                        case "2: Show Travel list":
                            
                            Console.WriteLine("Travels");
                            
                            Travels.ShowTravels();
                            break;
                        
                        case "3: Exit":
                            Console.WriteLine("Plan your equipment");
                            return;
                            

                        

                        
                    }
                }
            }
           
        }

       
    }
