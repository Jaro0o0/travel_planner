using Microsoft.Data.Sqlite;

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
        }
    }
}
