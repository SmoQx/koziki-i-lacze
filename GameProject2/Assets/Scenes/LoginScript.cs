using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using ClientServerCommunication;
using UnityEngine.UI;
using TMPro;


public class LoginScript : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    SceneManagerScript change_scene = new SceneManagerScript();
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

        if (response != "non valid" && response != "Unknown method.")
        {
            Save_to_file(tekst: response);
            change_scene.LoadScene("MainMenu");
        }
        
    }


    [ContextMenu("Save_to_file")]
    public void Save_to_file(string tekst)
    {    
        string filePath = "user.txt";
        Debug.Log(Path.GetFullPath(filePath));
        try
        {
            File.WriteAllText(filePath, contents: tekst);
            Debug.Log("Saved to a file");
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}
