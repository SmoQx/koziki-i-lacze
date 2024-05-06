using SQLite;

public class Read_user_data
{
    public static void read_user_data()
    {
        string databasePath = "MyDatabase.db";

        using (SQLiteConnection connection = new SQLiteConnection(databasePath))
        {
            var whole_user_data = connection.Table<UserData>().ToList();
            foreach (var item in whole_user_data)
            {
                Console.WriteLine($"ID: {item.Id}, Nickname: {item.Nickname}, Level: {item.Level}, Items: {item.ItemsList}, HP: {item.HP}, Mana: {item.Mana}, Skills: {item.Skills}");
            }
        }
    }
}
