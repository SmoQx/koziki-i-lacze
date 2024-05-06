using SQLite;

public class Read_users
{
    public static void read_users()
    {
        string databasePath = "MyDatabase.db";

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
