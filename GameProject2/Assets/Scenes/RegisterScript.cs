using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RegisterScript : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    SceneManagerScript change_scene = new SceneManagerScript();
    public TMP_InputField login;
    public TMP_InputField password;

    public void Register()
    {
        Connection.ConnectToServer();
        string user_pass_string = $"{{\"user_name\":\"{login.text}\", \"password\":\"{password.text}\"}}";

        MyData data = new MyData
        {
            method = "PUT",
            message = user_pass_string
        };

        Connection.SendMessageToServer(data);
        string response = Connection.ReceiveMessageFromServer();

        if (response == $"added user: {login.text}")
        {
            change_scene.LoadScene("CreateNickname");
        }
        else
        {
            change_scene.LoadScene("FailedRegister");
        }
    }


    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }

}
