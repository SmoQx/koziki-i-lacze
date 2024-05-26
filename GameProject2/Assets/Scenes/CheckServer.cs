using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerCheck : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    public string errorSceneName = "ConnectionLost";

    void Start()
    {
        CheckServer();
    }

    public void CheckServer()
    {
        try
        {
            Connection.ConnectToServer();

            MyData data = new MyData
            {
                method = "echo",
                message = "Ok"
            };

            Connection.SendMessageToServer(data);
            string response = Connection.ReceiveMessageFromServer();

            if (response != "Ok")
            {
                LoadErrorScene();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception occurred: " + ex.Message);
            LoadErrorScene();
        }
    }

    public void LoadErrorScene()
    {
        SceneManager.LoadScene(errorSceneName);
    }

    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}
