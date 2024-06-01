using SQLite;

public class PlayerData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [Unique]
    public string Nick { get; set; }
    public int PoziomDoswiadczenia { get; set; }
    public int Zwyciestwa { get; set; }
    public string CzasGry { get; set; }
}
