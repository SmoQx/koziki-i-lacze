using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetUserData : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    SceneManagerScript change_scene = new SceneManagerScript();
    SaveLoadFile save_file = new SaveLoadFile();
    string filePath = "user_data.json";
    string hashFileName = "user.txt";

    void Start()
    {
        Get_Data_About_User();
    }

    public void Get_Data_About_User()
    {
        Connection.ConnectToServer();
        string hash = save_file.Load_to_file(hashFileName);
        string user_pass_string = $"{{\"player_name\":\"tekst\", \"user_name\":\"{hash}\"}}";

        MyData data = new MyData
        {
            method = "GET",
            message = user_pass_string
        };

        Connection.SendMessageToServer(data);
        string response = Connection.ReceiveMessageFromServer();
        JObject parsed = JObject.Parse(response);
        JObject skills = JObject.Parse(parsed["Skills"].ToString());
        JObject inv_parsed = JObject.Parse(parsed["ItemsList"].ToString());
        parsed["Skills"] = skills;
        parsed["ItemsList"] = inv_parsed;
        if (response != "no player data")
        {
            save_file.Save_to_file(filePath: filePath, tekst: parsed.ToString());
        }
        
    }


    public void Save_Data_About_User()
    {
        Connection.ConnectToServer();
        string user_data_to_update = save_file.Load_to_file(filePath);

        MyData data = new MyData
        {
            method = "PUT",
            message = user_data_to_update
        };

        Connection.SendMessageToServer(data);
        string response = Connection.ReceiveMessageFromServer();

        Debug.Log(response);
        
    }


    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}
