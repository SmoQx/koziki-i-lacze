using SQLite;


public class Adder
{
    public static void Add<T>(T data) where T : new()
    {
        // Specify the path to the SQLite database file
        string databasePath = "MyDatabase.db";

        // Create a new SQLite connection
        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            // Ensure connection is open
            connection.CreateTable<T>();

            // Insert data into the table
            connection.Insert(data);
        }
    }
}
