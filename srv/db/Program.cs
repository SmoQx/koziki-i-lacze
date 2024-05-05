using SQLite;

class Program
{
    static void Main(string[] args)
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
                Pozycja = 1,
                Nick = "Veteran",
                PoziomDoswiadczenia = 100,
                Zwyciestwa = 100,
                Porazki = 20,
                Ratio = 5,
                CzasGry = "50h"
            };
            connection.Insert(player);

            // Retrieve all data from the PlayerData table
            var allPlayers = connection.Table<PlayerData>().ToList();
            foreach (var p in allPlayers)
            {
                Console.WriteLine($"ID: {p.Id}, Pozycja: {p.Pozycja}, Nick: {p.Nick}, Poziom doświadczenia: {p.PoziomDoswiadczenia}, Zwycięstwa: {p.Zwyciestwa}, Porażki: {p.Porazki}, Ratio: {p.Ratio}, Czas gry: {p.CzasGry}");
            }

            // Delete the PlayerData table
//            connection.DropTable<PlayerData>();
        }
    }
}
