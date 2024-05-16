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
                Console.WriteLine(message);
                Console.WriteLine(instance.finduser(DB.Hashing.ComputeSHA256Hash(user_name), DB.Hashing.ComputeSHA256Hash(password), db_path));
                if (instance.finduser(DB.Hashing.ComputeSHA256Hash(user_name), DB.Hashing.ComputeSHA256Hash(password), db_path))
                {
                    return $"valid";
                }
                else
                    return $"non valid";
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
                return $"added user {user_name}";
            }
            else if (method == "GET" && message.Contains("inventory"))
            {
                JObject messageContent = JObject.Parse(message);
                string player_name = messageContent["player_name"]?.ToString() ?? "";
                string user_info = DB.Readers.read_user_info(db_path, player_name);
                Console.WriteLine(user_info);
                return $"User inventory";
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
