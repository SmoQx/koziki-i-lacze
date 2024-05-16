namespace DB
{
    using SQLite;

    public class Find_user_and_passoword
    {

        public bool finduser(string userName, string userPassoword, string databasePath)
        {
            SQLiteConnection connection = new SQLiteConnection(databasePath);
            var what_was_found = connection.Table<User_credentials>().FirstOrDefault(u => u.UserName == userName && u.PasswordHash == userPassoword);
            if (what_was_found != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
