using System;
using System.Net.Http;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;


namespace  Services
{
    

public class AddNewTravel
{
    public async static Task AddNewTrip()
        {
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

                                   //Adding palcee 
                                   if (userChoice == "y")
                                   {
                                       using SqliteConnection connection = new SqliteConnection("Data Source=src/Models/travel.db");
                                       connection.Open();

                                       using SqliteCommand command = connection.CreateCommand();
                                       command.CommandText = "INSERT INTO Trips (Destination, StartDate, EndDate) VALUES (@destination, @startDate, @endDate)";
                                       command.Parameters.AddWithValue("@destination", PlacesData?.DisplayName?.Text ?? travelPlace);
                                       command.Parameters.AddWithValue("@startDate", DateTime.Now.ToString("yyyy-MM-dd"));
                                       command.Parameters.AddWithValue("@endDate", DateTime.Now.ToString("yyyy-MM-dd"));
                                       command.ExecuteNonQuery();

                                       Console.WriteLine("Place saved!");
                                   }
                                   else
                                   {
                                       Console.WriteLine("Place not saved.");
                                   }
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
        }
}

}

}
