using SQLite;

public class User_credentials
{
    [PrimaryKey]
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
}
