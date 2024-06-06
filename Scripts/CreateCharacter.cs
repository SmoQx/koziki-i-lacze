using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    SceneManagerScript change_scene = new SceneManagerScript();
    string filePath = "user.txt";
    SaveLoadFile io_steam = new SaveLoadFile();
    public TMP_InputField character_name;

    public void CreateCharacter_with_UserLogin()
    {
        Connection.ConnectToServer();
        
        string content = io_steam.Load_to_file(filePath);
        string new_character = $"{{\"new_character\":\"yes\", \"Nickname\": \"{character_name.text}\", \"UserName\": \"{content}\"}}";

        MyData data = new MyData
        {
            method = "PUT",
            message = new_character
        };

        string string_to_add_to_leaderboard = $"{{\"Nick\": \"{character_name.text}\"}}";

        MyData data2 = new MyData
        {
            method = "PUT",
            message = string_to_add_to_leaderboard
        };

        Connection.SendMessageToServer(data);
        string response = Connection.ReceiveMessageFromServer();
        Debug.Log(response);
        
        Connection.ConnectToServer();
        Connection.SendMessageToServer(data2);
        string response2 = Connection.ReceiveMessageFromServer();
        Debug.Log(response2);

        if (response == "add new character")
            change_scene.LoadScene("LoginPage");
        
    }
    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}