using DB;

class Program
{
    static void Main(string[] args)
    {
    var userData = new UserData
        {
            Nickname = "easd",
            Level = 10,
            ItemsList = "itemsList",
            HP = 10,
            Mana = 20,
            Skills = "skills"
        };
        var user = new User_credentials
        {
            UserName = Hashing.ComputeSHA256Hash("new"),
            PasswordHash = Hashing.ComputeSHA256Hash("pass")
        };
        var player = new PlayerData
        {
            Id = 2,
            Nick = "Sdas",
            PoziomDoswiadczenia = 50,
            Zwyciestwa = 100,
            CzasGry = "50h"
        };
//
        Adder.Update(player, "MyDatabase.db");
        Adder.Update(user, "MyDatabase.db");
//        Adder.Add(user);
//        Adder.Add(player, "MyDatabase.db");
//        Console.WriteLine(Readers.read_user_data("MyDatabase.db"));
//        Readers.read_users();
//        Readers.read_scoreboard("MyDatabase.db");
//        Find_user_and_passoword found_user = new Find_user_and_passoword();
//        found_user.finduser(Hashing.ComputeSHA256Hash("user1"), Hashing.ComputeSHA256Hash("pass"));
    }
}
