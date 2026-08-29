using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.Data.Sqlite;

public class DataBase
{
    private readonly string ConectString = "Data Source=Models/travel.db";

    public void CreateDatabase()
    {

        

        using SqliteConnection connection = new SqliteConnection(ConectString);
        connection.Open();

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Trips (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Destination TEXT NOT NULL,
                StartDate TEXT NOT NULL,
                EndDate TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Interests TEXT
            );

            CREATE TABLE IF NOT EXISTS userInterests (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Interests TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS BackpackItems (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Destination TEXT NOT NULL,
                Item TEXT NOT NULL,
                CreatedAt TEXT NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }
}
