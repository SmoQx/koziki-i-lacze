using DB;

class Program
{
    static void Main(string[] args)
    {
//    var userData = new UserData
//        {
//            Nickname = "nickname",
//            Level = 10,
//            ItemsList = "itemsList",
//            HP = 10,
//            Mana = 20,
//            Skills = "skills"
//        };
        var user = new User_credentials
        {
            UserName = Hashing.ComputeSHA256Hash("user1"),
            PasswordHash = Hashing.ComputeSHA256Hash("pass")
        };
//        var player = new PlayerData
//        {
//            Pozycja = 4,
//            Nick = "Name",
//            PoziomDoswiadczenia = 100,
//            Zwyciestwa = 100,
//            Porazki = 20,
//            Ratio = 5,
//            CzasGry = "50h"
//        };
//
//        Adder.Add(userData);
//        Adder.Add(user);
//        Adder.Add(player);
//        Readers.read_user_data();
//        Readers.read_users();
//        Readers.read_scoreboard();
        Find_user_and_passoword found_user = new Find_user_and_passoword();
        found_user.finduser(Hashing.ComputeSHA256Hash("user1"), Hashing.ComputeSHA256Hash("pass"));
    }
}
