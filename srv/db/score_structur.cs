using SQLite;

public class PlayerData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Pozycja { get; set; }
    public string Nick { get; set; }
    public int PoziomDoswiadczenia { get; set; }
    public int Zwyciestwa { get; set; }
    public int Porazki { get; set; }
    public float Ratio { get; set; }
    public string CzasGry { get; set; }
}
