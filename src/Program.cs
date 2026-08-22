using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pastel;
using ShareTravelPalace;
using Services;
using Microsoft.Data.Sqlite;

// using CreateBackpack;

namespace TravelPlanner
{
    class Program
    {

        // diaspley equipment
        static async Task Main()
        {
            while (true)
            {
                Console.WriteLine("###Trials_planner###".Pastel(Color.Blue));
                Console.WriteLine("choose option");
                Console.WriteLine("1 Add new trip ");
                Console.WriteLine("2 Show Tracks list ");
                Console.WriteLine("4 Exit");
                Console.WriteLine("5: plan your equipment");

                string choose = Console.ReadLine() ?? "";

                switch (choose)
                {
                    case "1":
                        Console.WriteLine("CASE: 1");
                

                        // Console.WriteLine("choose your kind of travel");
                        // Console.WriteLine("1: Standard travel");
                        // Console.WriteLine("2: Moluntain Teavel");

                        // string travelKind = Console.ReadLine()?.Trim() ?? "";

                        // switch (travelKind)
                        // {
                        //     case "1":
                        //         Console.WriteLine("Where you wanto to travel");
                        //         string travelPlace = Console.ReadLine()?.Trim() ?? "";
                        //         //Travel place info
                        //         if (!string.IsNullOrWhiteSpace(travelPlace))
                        //         {
                        //            Place? PlacesData = await ShareTravelPalace.PlacesService.PlacesInfo(travelPlace);
                        //            Console.WriteLine($"Place: {PlacesData?.DisplayName?.Text}");
                                
                        //            Console.WriteLine($"Address: {PlacesData?.FormattedAddress}"); 
                        //            Console.WriteLine("Do you want to save this place? (y/n)");
                        //            string userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

                        //            //Adding palcee 
                        //            if (userChoice == "y")
                        //            {
                        //                // TODO: implement place saving
                        //                using SqliteConnection connection = new SqliteConnection("Data Source=src/Models/travel.db");
                        //                connection.Open();

                        //                using SqliteCommand command = connection.CreateCommand();
                        //                command.CommandText = "  INSERT INTO Trips (Destination, StartDate, EndDate) VALUES (@destination, @startDate, @endDate)";
                        //                command.ExecuteNonQuery();
                                       

                        //            }
                        //            else
                        //            {
                        //                Console.WriteLine("Place not saved.");
                        //            }
                        //         }
                        //         break;
                        
                        // break;

                    case "2":
                    {
                        // Show actual Travel
                        using SqliteConnection connection = new SqliteConnection("Data Source=src/Models/travel.db");
                        connection.Open();
                        
                        using SqliteCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT * FROM Trips";

                        using SqliteDataReader reader = command.ExecuteReader();

                        while(reader.Read())
                        {
                            Console.WriteLine($"Destination: {reader["Destination"]}, Start Date: {reader["StartDate"]}, End Date: {reader["EndDate"]}");
                            Trip Travel = new Trip(
                                Convert.ToInt32(reader["Id"]),
                                reader["Destination"].ToString() ?? "",
                                Convert.ToDateTime(reader["StartDate"]),
                                Convert.ToDateTime(reader["EndDate"])

                            );
                            List<Trip> trips = new List<Trip>();
                            trips.Add(Travel);

                        }

                      
                    

                        
                            Console.WriteLine("All trips shown above.");
                        
                        {
                            Console.WriteLine("YOu don't have any travel yet");
                        }
                        break;
                    }
                      
                    
                
                    
                    case "3":
                        Console.WriteLine("Plan your equipment");
                        break;
                       return;
                        


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

        void DisplayWeatherForecast(ISTrip  travel )
        {
            travel.WeatherForecast();
        }
    }
}
