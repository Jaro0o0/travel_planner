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
                

                        Console.WriteLine("choose your kind of travel");
                        Console.WriteLine("1: Standard travel");
                        Console.WriteLine("2: Moluntain Teavel");

                        string travelKind = Console.ReadLine()?.Trim() ?? "";

                        switch (travelKind)
                        {
                            case "1":
                                Console.WriteLine("Where you wanto to travel");
                                string travelPlace = Console.ReadLine()?.Trim() ?? "";
                                //Travel place info
                                if (!string.IsNullOrWhiteSpace(travelPlace))
                                {
                                   Place? PlacesData = await ShareTravelPalace.PlacesService.PlacesInfo(travelPlace);
                                   Console.WriteLine($"Place: {PlacesData?.DisplayName?.Text}");
                                
                                   Console.WriteLine($"Address: {PlacesData?.FormattedAddress}"); 
                                   Console.WriteLine("Do you want to save this place? (y/n)");
                                   string userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

                                   if (userChoice == "y")
                                   {
                                       // TODO: implement place saving
                                       using SqliteConnection connection = new SqliteConnection("Data Source=travel.db");
                                       connection.Open();

                                       using SqliteCommand command = connection.CreateCommand();
                                       command.CommandText = "  INSERT INTO Trips (Destination, StartDate, EndDate) VALUES (@destination, @startDate, @endDate)";
                                       command.ExecuteNonQuery();
                                       

                                   }
                                   else
                                   {
                                       Console.WriteLine("Place not saved.");
                                   }
                                }
                                break;
                        }
                        break;

                    case "2":

                        // Show actual Travel
                        using SqliteConnection connection = new SqliteConnection("Data Source=travel.db");
                        connection.Open();
                        
                        using SqliteCommand command = connection.CreateCommand();
                        command.CommandText = "SELECT * FROM Trips";
                        command.ExecuteNonQuery();


                        using SqliteDataReader reader = command.ExecuteReader();

                        while(reader.Read())
                        {
                            Trip trip = new Trip(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Destination")),
                                reader.GetDateTime(reader.GetOrdinal("StartDate")),
                                reader.GetDateTime(reader.GetOrdinal("EndDate"))
                            );

                            //List of trips
                            List<ISTrip> trips = new List<ISTrip>();
                            trips.Add(trip);
                            Console.WriteLine($"Destination: {reader["Destination"]}, Start Date: {reader["StartDate"]}, End Date: {reader["EndDate"]}");
                        }

                        //Show all tra
                        Console.WriteLine("Do you want to see all trips? (y/n)");
                        string UserChoose = Console.ReadLine();
                        if(UserChoose.Trim().ToLower() == "y")
                        {
                            Console.WriteLine("All trips:");
                            foreach (var trip in trips)
                            {
                                Console.WriteLine($"Destination: {trip.Destination}, Start Date: {trip.StartDate}, End Date: {trip.EndDate}");
                            }
                           
                        }
                        else
                        {
                            Console.WriteLine("YOu don't have any travel yet");
                            


                        }
                         break;
                      
                    
                
                    
                    case "3":
                        Console.WriteLine('Plan yoru equipment');


                    case "4":
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
    }
}
