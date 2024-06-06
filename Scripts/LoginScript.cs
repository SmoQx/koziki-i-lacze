using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using ClientServerCommunication;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;


public class LoginScript : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    SceneManagerScript change_scene = new SceneManagerScript();
    SaveLoadFile save_file = new SaveLoadFile();
    string filePath = "user.txt";
    public TMP_InputField login;
    public TMP_InputField password;

    public void Login()
    {
        Connection.ConnectToServer();
        string user_pass_string = $"{{\"user_name\":\"{login.text}\", \"password\":\"{password.text}\"}}";

        MyData data = new MyData
        {
            method = "GET",
            message = user_pass_string
        };

        Connection.SendMessageToServer(data);
        string response = Connection.ReceiveMessageFromServer();
        
        Debug.Log("Parsed response:" + response);
        Debug.Log("Parsed response content of Correct" + JObject.Parse(response)["Correct"]);
        string resp_parsed = JObject.Parse(response)["Correct"].ToString();
        

        if (response.Contains("Correct"))
        {
            save_file.Save_to_file(tekst: resp_parsed, filePath: filePath);
            save_file.Load_to_file(filePath: filePath);
            change_scene.LoadScene("MainMenu");
        }
        else
        {
            Debug.LogError("Could not connect");
        }
        
    }


    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}
