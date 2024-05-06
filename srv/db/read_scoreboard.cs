using SQLite;

public class Read_scoreboard
{
    public static void read_scoreboard()
    {
        string databasePath = "MyDatabase.db";

        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            var allPlayers = connection.Table<PlayerData>().ToList();
            foreach (var p in allPlayers)
            {
                Console.WriteLine($"ID: {p.Id}, Pozycja: {p.Pozycja}, Nick: {p.Nick}, Poziom doświadczenia: {p.PoziomDoswiadczenia}, Zwycięstwa: {p.Zwyciestwa}, Porażki: {p.Porazki}, Ratio: {p.Ratio}, Czas gry: {p.CzasGry}");
            }

        }
    }
}
