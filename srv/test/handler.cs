using Newtonsoft.Json.Linq;
using DB;

public class MessageHandler
{
    public static string HandleMessage(string jsonMessage)
    {
        try
        {
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
                return Readers.read_scoreboard("../db/MyDatabase.db");
            }
            else if (method == "PUT" && message.Contains("user_name") && message.Contains("password"))
            {
                JObject messageContent = JObject.Parse(message);
                string db_path =  "../db/MyDatabase.db";
                string user_name = messageContent["user_name"]?.ToString() ?? "";
                string password = messageContent["password"]?.ToString() ?? "";
                DB.Find_user_and_passoword instance = new Find_user_and_passoword();
                Console.WriteLine(message);
                Console.WriteLine(instance.finduser(user_name, password, db_path));
                if (instance.finduser(user_name, password, db_path))
                {
                    return $"valid";
                }
                else
                    return $"non valid";
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
