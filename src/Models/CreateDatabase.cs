using Microsoft.Data.Sqlite;

public class DataBase
{
    private readonly string ConectString = "Data Source=travel.db";

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
            """;
        command.ExecuteNonQuery();
    }
}
