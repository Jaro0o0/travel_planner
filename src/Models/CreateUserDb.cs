using Microsoft.Data.Sqlite;

public class UsersDb
{
    private readonly string ConectString = "Data Source=Models/travel.db";
    public void CreateDb()
    {
        using SqliteConnection connection = new SqliteConnection(ConectString);
        connection.Open();

         using SqliteCommand commadn = connection.CreateCommand();
         commadn.CommandText = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Interests TEXT)";
         commadn.ExecuteNonQuery();

    }
  

}
