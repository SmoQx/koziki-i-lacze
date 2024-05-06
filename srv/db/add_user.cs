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

        }
    }
}
