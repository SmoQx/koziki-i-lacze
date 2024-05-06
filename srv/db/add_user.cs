using SQLite;

public class Add_User
{
    public static void add_user()
    {
        // Specify the path to the SQLite database file
        string databasePath = "MyDatabase.db";

        // Create a new SQLite connection
        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            // Create the UserData table (if it doesn't exist)
            connection.CreateTable<User_credentials>();

            // Insert sample data into the UserData table
            var user = new User_credentials
            {
                UserName = "usr",
                PasswordHash = "password"
            };
            connection.Insert(user);

            // Retrieve all data from the UserData table
            var allUsers = connection.Table<User_credentials>().ToList();
            foreach (var u in allUsers)
            {
                Console.WriteLine($"user name: {u.UserName}, password: {u.PasswordHash}");
            }

            // Drop the UserData table (for cleanup, you may omit this)
        }
    }
}
