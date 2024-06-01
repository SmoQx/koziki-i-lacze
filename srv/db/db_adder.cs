namespace DB
{
    using SQLite;

    public class Adder
    {
        public static void Add<T>(T data, string databasePath) where T : new()
        {
            // Create a new SQLite connection
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                // Ensure connection is open
                connection.CreateTable<T>();

                // Insert data into the table
                connection.Insert(data);
            }
        }
        public static void Update<T>(T data, string databasePath) where T : new()
        {
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.CreateTable<T>();
                connection.Update(data);
            }
        }
    }
}
