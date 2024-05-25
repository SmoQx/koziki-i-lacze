using Newtonsoft.Json.Linq;
using DB;

public class MessageHandler
{
    public static string HandleMessage(string jsonMessage)
    {
        try
        {
            string db_path =  "../db/MyDatabase.db";
            // Parse the JSON message
            JObject messageObject = JObject.Parse(jsonMessage);
            if (messageObject == null)
                return "error whle parsing json";
            // Extract method and message from JSON
            string method = messageObject["method"]?.ToString() ?? "";
            string message = messageObject["message"]?.ToString() ?? "";
            // Determine response based on the method and message
            if (method == "greet")
            {
                Console.WriteLine($"Server is sending: a great");
                return Greet(message);
            }
            else if (method == "echo")
            {
                return Echo(message);
            }
            else if (method == "GET" && message == "user_table")
            {
                return Readers.read_scoreboard(db_path);
            }
            else if (method == "GET" && message.Contains("user_name") && message.Contains("password"))
            {
                JObject messageContent = JObject.Parse(message);
                string user_name = messageContent["user_name"]?.ToString() ?? "";
                string password = messageContent["password"]?.ToString() ?? "";
                DB.Find_user_and_passoword instance = new Find_user_and_passoword();
                if (instance.finduser(DB.Hashing.ComputeSHA256Hash(user_name), DB.Hashing.ComputeSHA256Hash(password), db_path))
                {
                    return $"{Hashing.ComputeSHA256Hash(user_name)}";
                }
                else
                    return $"non valid";
            }
            else if (method == "GET" && message.Contains("av_characters"))
            {
                return $"available characters";
            }
            else if (method == "PUT"  && message.Contains("new_character"))
            {
                Console.WriteLine("entered new char");
                JObject new_player_content = JObject.Parse(message);
                var new_character = new UserData
                {
                    Nickname = new_player_content["Nickname"]?.ToString() ?? "",
                    Level = 0,
                    ItemsList = "",
                    HP = 10,
                    Mana = 10,
                    Skills = "",
                    Is_alive = true,
                    UserName = DB.Hashing.ComputeSHA256Hash(new_player_content["UserName"]?.ToString() ?? "")
                };
                DB.Adder.Add(new_character, db_path);
                return $"add new character";
            }
            else if (method == "PUT" && message.Contains("user_name") && message.Contains("password"))
            {
                JObject messageContent = JObject.Parse(message);
                string user_name = messageContent["user_name"]?.ToString() ?? "";
                string password = messageContent["password"]?.ToString() ?? "";
                var user_cred_to_add = new User_credentials
                {
                    UserName = DB.Hashing.ComputeSHA256Hash(user_name),
                    PasswordHash = DB.Hashing.ComputeSHA256Hash(password)
                };
                try
                {
                DB.Adder.Add(user_cred_to_add, db_path);
                }
                catch (Exception e)
                {
                    return $"error while adding user {e}";
                }
                return $"added user: {user_name}";
            }
            else if (method == "GET" && message.Contains("player_name") && message.Contains("user_name"))
            {
                JObject messageContent = JObject.Parse(message);
                string player_name = messageContent["player_name"]?.ToString() ?? "";
                string user_name = messageContent["user_name"]?.ToString() ?? "";
                user_name = DB.Hashing.ComputeSHA256Hash(user_name);
                string user_info = DB.Readers.read_user_info(player_name, user_name, db_path);
                return user_info;
            }
            else
            {
                return "Unknown method.";
            }
        }
        catch (Exception ex)
        {
            return $"Error handling message: {ex.Message}";
        }
    }

    private static string Greet(string name)
    {
        return $"Hello, {name}!";
    }

    private static string Echo(string message)
    {
        return message;
    }
}
