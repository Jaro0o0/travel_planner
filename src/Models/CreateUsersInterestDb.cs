using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;

public class UsersInterestsDataBase
{
    private readonly string ConectString = "Data Source=Models/travel.db";

    public void CreateDatabase()
    {

        

        using SqliteConnection connection = new SqliteConnection(ConectString);
        connection.Open();

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS userInterests (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Interests TEXT NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }
}
