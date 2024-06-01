namespace DB
{
    using SQLite;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Readers
    {
    //    private static string databasePath = "MyDatabase.db";
        public static string read_scoreboard(string databasePath)
        {
            List<object> scoreboardData = new List<object>();
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                var allPlayers = connection.Table<PlayerData>().ToList();
                foreach (var p in allPlayers)
                {
                    var playerData = new
                                        {
                                            ID = p.Id,
                                            Nick = p.Nick,
                                            PoziomDoswiadczenia = p.PoziomDoswiadczenia,
                                            Zwyciestwa = p.Zwyciestwa,
                                            CzasGry = p.CzasGry
                                        };
                   
                    scoreboardData.Add(playerData);
                }
                return JsonConvert.SerializeObject(scoreboardData);
            }
        }
        public static string read_user_data(string databasePath)
        {
            List<object> player_stats = new List<object>();
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                var whole_user_data = connection.Table<UserData>().ToList();
                foreach (var item in whole_user_data)
                {
                    var player = new 
                    {
                        ID = item.Id,
                        Nickname = item.Nickname,
                        Level = item.Level,
                        ItemsList = item.ItemsList,
                        HP = item.HP,
                        Mana = item.Mana,
                        Skills = item.Skills,
                        Is_alive = item.Is_alive,
                        UserName = item.UserName
                    };
                    player_stats.Add(player);
                }
                return JsonConvert.SerializeObject(player_stats);
            }
        }
        public static string read_user_info(string user_name, string databasePath)
        {
            SQLiteConnection connection = new SQLiteConnection(databasePath);
            var what_was_found = connection.Table<UserData>().FirstOrDefault(u => u.UserName == user_name);
            if (what_was_found != null)
            {
                return JsonConvert.SerializeObject(what_was_found);
            }
            else
                return "no player data";
        }
    }
}
