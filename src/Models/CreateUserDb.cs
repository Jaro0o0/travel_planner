using Microsoft.Data.Sqlite;

public class UsersDb
{
    private readonly string ConectString = "Data Source=src/Models/travel.db";
    public void CreateDb()
    {
        using SqliteConnection connection = new SqliteConnection(ConectString);
        connection.Open();

         using SqliteCommand commadn = connection.CreateCommand();
         commadn.CommandText = "CREATE TABLE If NOT EXIST USERS  ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL Interests";

    }
  

}
