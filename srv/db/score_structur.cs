using SQLite;

public class PlayerData
{
    [PrimaryKey]
    public string Nick { get; set; }
    public int PoziomDoswiadczenia { get; set; }
    public int Zwyciestwa { get; set; }
    public int CzasGry { get; set; }
}
