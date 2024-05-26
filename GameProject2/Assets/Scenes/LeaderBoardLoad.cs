using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using ClientServerCommunication;
using LeaderBoard;
using UnityEngine.UI;


public class LeaderBoardLoad : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    public Text Rank;
    public Text Nick;
    public Text PD;
    public Text Wins;
    public Text Lose;
    public Text GameTime;
    List<LeaderBoardEntry> listed_leader_board;

    public void Show_Leader_Board()
    {
        Connection.ConnectToServer();

        MyData data = new MyData
        {
            method = "GET",
            message = "user_table",
        };

        Connection.SendMessageToServer(data);
        string response = Connection.ReceiveMessageFromServer();
        listed_leader_board = JsonConvert.DeserializeObject<List<LeaderBoardEntry>>(response);
        display_data();
    }

    [ContextMenu("Display_text")]
    public void display_data()
    {
        Rank.text = "";
        Nick.text = "";
        PD.text = "";
        Wins.text = "";
        Lose.text = "";
        GameTime.text = "";
        //Level.text = "";
        foreach (var item in listed_leader_board)
        {
            Rank.text += item.Pozycja + "\n";
            Nick.text += item.Nick + "\n";
            PD.text += item.PoziomDoswiadczenia + "\n";
            Wins.text += item.Zwyciestwa + "\n";
            Lose.text += item.Porazki + "\n";
            GameTime.text += item.CzasGry + "\n";
            //Level.text += item.PoziomDoswiadczenia + "\n";   
        }
    }


    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}
