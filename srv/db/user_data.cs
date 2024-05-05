using SQLite;

public class UserData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Nickname { get; set; }
    public int Level { get; set; }
    public string ItemsList { get; set; }
    public int HP { get; set; }
    public int Mana { get; set; }
    public string Skills { get; set; }
}
