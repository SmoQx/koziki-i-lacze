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
                                            Pozycja = p.Pozycja,
                                            Nick = p.Nick,
                                            PoziomDoswiadczenia = p.PoziomDoswiadczenia,
                                            Zwyciestwa = p.Zwyciestwa,
                                            Porazki = p.Porazki,
                                            Ratio = p.Ratio,
                                            CzasGry = p.CzasGry
                                        };
                   
                    scoreboardData.Add(playerData);
                }
                return JsonConvert.SerializeObject(scoreboardData);
            }
        }
        public string read_user_data(string databasePath)
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
                        Skills = item.Skills
                    };

                }
                return JsonConvert.SerializeObject(player_stats);
            }
        }
    }
}
