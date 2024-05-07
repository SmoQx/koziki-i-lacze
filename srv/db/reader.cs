using SQLite;

public class Readers
{
    private static string databasePath = "MyDatabase.db";
    public static void read_scoreboard()
    {

        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            var allPlayers = connection.Table<PlayerData>().ToList();
            foreach (var p in allPlayers)
            {
                Console.WriteLine($"ID: {p.Id}, Pozycja: {p.Pozycja}, Nick: {p.Nick}, Poziom doświadczenia: {p.PoziomDoswiadczenia}, Zwycięstwa: {p.Zwyciestwa}, Porażki: {p.Porazki}, Ratio: {p.Ratio}, Czas gry: {p.CzasGry}");
            }

        }
    }
    public static void read_user_data()
    {

        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            var whole_user_data = connection.Table<UserData>().ToList();
            foreach (var item in whole_user_data)
            {
                Console.WriteLine($"ID: {item.Id}, Nickname: {item.Nickname}, Level: {item.Level}, Items: {item.ItemsList}, HP: {item.HP}, Mana: {item.Mana}, Skills: {item.Skills}");
            }
        }
    }
    public static void read_users()
    {

        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            var allUsers = connection.Table<User_credentials>().ToList();
            foreach (var u in allUsers)
            {
                Console.WriteLine($"user name: {u.UserName}, password: {u.PasswordHash}");
            }
        }
    }
}
