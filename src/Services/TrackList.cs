using Microsoft.Data.Sqlite;

namespace Services
{
    public class TracksList
    {
        public void ShowTracksList()
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
        }
    }
}