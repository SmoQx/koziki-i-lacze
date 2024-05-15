namespace DB
{
    using SQLite;

    public class Find_user_and_passoword
    {

        public static string finduser(string userName, string userPassoword)
        {
            string databasePath = "MyDatabase.db";
            SQLiteConnection connection = new SQLiteConnection(databasePath);
            var what_was_found = connection.Table<User_credentials>().FirstOrDefault(u => u.UserName == userName && u.PasswordHash == userPassoword);
            if (what_was_found != null)
            {
                Console.WriteLine($"user name: {what_was_found.UserName}, password: {what_was_found.PasswordHash}");
                return "valid";
            }
            else
                return "not valid";
        }
    }
}
