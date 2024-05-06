using SQLite;

public class Add_user_to_score_board
{
    public static void add_user_to_score_board()
    {
        // Specify the path to the SQLite database file
        string databasePath = "MyDatabase.db";

        // Create a new SQLite connection
        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            // Create the PlayerData table (if it doesn't exist)
            connection.CreateTable<PlayerData>();

            // Insert data into the PlayerData table
            var player = new PlayerData
            {
                Pozycja = 4,
                Nick = "Name",
                PoziomDoswiadczenia = 100,
                Zwyciestwa = 100,
                Porazki = 20,
                Ratio = 5,
                CzasGry = "50h"
            };
            connection.Insert(player);

        }
    }
}
