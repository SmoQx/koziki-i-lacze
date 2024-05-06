using SQLite;

public class Add_player
{
    public static void add_player()
    {
        // Specify the path to the SQLite database file
        string databasePath = "MyDatabase.db";

        // Create a new SQLite connection
        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            // Create the UserData table (if it doesn't exist)
            connection.CreateTable<UserData>();

            // Insert sample data into the UserData table
            var user = new UserData
            {
                Nickname = "Player1",
                Level = 10,
                ItemsList = "Sword, Shield, Potion",
                HP = 100,
                Mana = 50,
                Skills = "Attack, Defense, Heal"
            };
            connection.Insert(user);

        }
    }
}
