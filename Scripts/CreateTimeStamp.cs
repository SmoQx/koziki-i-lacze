using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class CreateTimeStamp : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication communication =  new ClientServerCommunication.ClientServerCommunication();
    ClientServerCommunication.ClientServerCommunication.MyData my_data = new ClientServerCommunication.ClientServerCommunication.MyData
    {
        method = "PUT",
        message = ""
    };
    SaveLoadFile save_file = new SaveLoadFile();
    string filePath = "time.txt";
    string file_to_user = "user_data.json";
    // Start is called before the first frame update
    void Start()
    {
        Create_Time();
    }

    public void Create_Time()
    {
        DateTime now = DateTime.UtcNow;
        DateTimeOffset dateTimeOffset = new DateTimeOffset(now);
        long currentTime = dateTimeOffset.ToUnixTimeSeconds();
        Debug.Log(currentTime);
        save_file.Save_to_file(filePath: filePath, tekst: currentTime.ToString());
    }

    [ContextMenu("show_time_passed")]
    public void Save_Time_Played()
    {
        DateTime now = DateTime.UtcNow;
        DateTimeOffset dateTimeOffset = new DateTimeOffset(now);
        long currentTime = dateTimeOffset.ToUnixTimeSeconds();
        long date_time_then = int.Parse(save_file.Load_to_file(filePath));
        long time_played = currentTime - date_time_then;
        Debug.Log(time_played.ToString());
        JObject Nick = JObject.Parse(save_file.Load_to_file(file_to_user));
        string nickname = Nick["Nickname"].ToString();
        my_data.message = $"{{\"Nick\": \"{nickname}\", \"GameTime\": \"{time_played}\"}}";
        communication.ConnectToServer();
        communication.SendMessageToServer(my_data);
        string response = communication.ReceiveMessageFromServer();
        Debug.Log(response);
        Create_Time();
    }
}
