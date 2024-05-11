using SQLite;

class Find_user_and_passoword
{
    static string databasePath = "MyDatabase.db";
    SQLiteConnection connection = new SQLiteConnection(databasePath);

    public bool finduser(string userName, string userPassoword)
    {
        var what_was_found = connection.Table<User_credentials>().FirstOrDefault(u => u.UserName == userName && u.PasswordHash == userPassoword);
        if (what_was_found != null)
        {
            Console.WriteLine($"user name: {what_was_found.UserName}, password: {what_was_found.PasswordHash}");
            return true;
        }
        else
            return false;
    }
}
