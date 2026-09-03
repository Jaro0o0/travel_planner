using Microsoft.Data.Sqlite;

namespace TravelPlanner.Infrastructure.Persistence;

public class DataBase
{
    private static string DatabasePath =>
        Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..", "..",
            "TravelPlanner.Infrastructure",
            "travel.db"));

    public static string ConnectionString => $"Data Source={DatabasePath}";

    public void CreateDatabase()
    {
        if (File.Exists(DatabasePath))
            return;

        using SqliteConnection connection = new($"Data Source={DatabasePath}");
        connection.Open();

        using SqliteCommand command = connection.CreateCommand();
        command.CommandText = """
                    CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Interests TEXT
                );

                CREATE TABLE IF NOT EXISTS UserInterests (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Interests TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Trips (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Destination TEXT NOT NULL,
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS BackpackItems (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    TripId INTEGER NOT NULL,
                    Item TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL,

                    FOREIGN KEY (TripId) REFERENCES Trips(Id)
                );
            """;
        command.ExecuteNonQuery();
    }
}
