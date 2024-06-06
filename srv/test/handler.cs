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
                    return $"{{\"Correct\":\"{Hashing.ComputeSHA256Hash(user_name)}\"}}";
                }
                else
                    return $"non valid";
            }
            else if (method == "GET" && message.Contains("av_characters"))
            {
                return $"available characters";
            }
            else if (method == "GET" && message.Contains("player_name") && message.Contains("user_name"))
            {
                JObject messageContent = JObject.Parse(message);
                string player_name = messageContent["player_name"]?.ToString() ?? "";
                string user_name = messageContent["user_name"]?.ToString() ?? "";
                Console.WriteLine(user_name);
                string user_info = DB.Readers.read_user_info(user_name, db_path);
                return user_info;
            }
            else if (method == "PUT"  && message.Contains("new_character"))
            {
                Console.WriteLine("entered new char");
                JObject new_player_content = JObject.Parse(message);
                var new_character = new UserData
                {
                    Nickname = new_player_content["Nickname"]?.ToString() ?? "",
                    Level = 0,
                    ItemsList = "{\"Money\": 10}",
                    HP = 100,
                    Mana = 10,
                    Skills = "{\"Speed\": 0, \"Jump\": 0,\"Strong\": 0}",
                    Is_alive = true,
                    UserName = DB.Hashing.ComputeSHA256Hash(new_player_content["UserName"]?.ToString() ?? "")
                };
                DB.Adder.Add(new_character, db_path);
                return $"add new character";
            }
            else if (method == "PUT" && message.Contains("UserName") && message.Contains("Nickname"))
            {
                Console.WriteLine("Changed data about character");
                JObject update_character_info = JObject.Parse(message);
                var new_character = new UserData
                {
                    Id = int.Parse(update_character_info["Id"].ToString()),
                    Nickname = update_character_info["Nickname"]?.ToString() ?? "",
                    Level = int.Parse(update_character_info["Level"].ToString()),
                    ItemsList = update_character_info["ItemsList"]?.ToString() ?? "",
                    HP = int.Parse(update_character_info["HP"].ToString()),
                    Mana = int.Parse(update_character_info["Mana"].ToString()),
                    Skills = update_character_info["Skills"]?.ToString() ?? "",
                    Is_alive = bool.Parse(update_character_info["Is_alive"].ToString()),
                    UserName = update_character_info["UserName"]?.ToString() ?? ""
                };
                DB.Adder.Update(new_character, db_path);

                return $"Updated character data";
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
            else if (method == "PUT" && message.Contains("Nick"))
            {
                JObject messageContent = JObject.Parse(message);
                int win;
                int pd;
                int gametime;
                int.TryParse(messageContent["PD"]?.ToString(), out pd);
                int.TryParse(messageContent["Win"]?.ToString(), out win);
                int.TryParse(messageContent["GameTime"]?.ToString(), out gametime);

                string nick = messageContent["Nick"].ToString();

                try
                {
                    // Retrieve the existing player data from the database
                    var existingPlayer = DB.Readers.leaderboard_entry(nick, db_path);

                    if (existingPlayer != null)
                    {
                        // Increment the existing values with the new values
                        existingPlayer.PoziomDoswiadczenia += pd;
                        existingPlayer.Zwyciestwa += win;
                        existingPlayer.CzasGry += gametime;

                        // Update the player data in the database
                        DB.Adder.Update(existingPlayer, db_path);
                    }
                    else
                    {
                        // If the player does not exist, create a new record
                        var newPlayer = new PlayerData
                        {
                            Nick = nick,
                            PoziomDoswiadczenia = pd,
                            Zwyciestwa = win,
                            CzasGry = gametime
                        };

                        DB.Adder.Add(newPlayer, db_path);
                    }
                }
                catch (Exception ex)
                {
                    var newPlayer = new PlayerData
                        {
                            Nick = nick,
                            PoziomDoswiadczenia = pd,
                            Zwyciestwa = win,
                            CzasGry = gametime
                        };

                        DB.Adder.Add(newPlayer, db_path);
                    return $"Error: {ex.Message}";
                }

                return $"Processed player {nick}";
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
