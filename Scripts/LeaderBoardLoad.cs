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

        foreach (var entry in listed_leader_board)
        {
            entry.ParseCzasGry();
        }

        calculate_positon();

        display_data();
    }

    void calculate_positon()
    {
        listed_leader_board.Sort((x, y) =>
        {
            double xValue = CalculateValue(x);
            double yValue = CalculateValue(y);
            return yValue.CompareTo(xValue);
        });
    }

    double CalculateValue(LeaderBoardEntry entry)
    {
        if (entry.Zwyciestwa == 0) return double.MinValue; 
        return (entry.PoziomDoswiadczenia / (double)entry.Zwyciestwa) / entry.ParsedCzasGry.TotalSeconds;
    }

    [ContextMenu("Display_text")]
    public void display_data()
    {
        Rank.text = "";
        Nick.text = "";
        PD.text = "";
        Wins.text = "";
        GameTime.text = "";

        int position = 1;
        foreach (var item in listed_leader_board)
        {
            Rank.text += position.ToString() + "\n";
            Nick.text += item.Nick + "\n";
            PD.text += item.PoziomDoswiadczenia + "\n";
            Wins.text += item.Zwyciestwa + "\n";
            GameTime.text += (item.CzasGry / 60) + "Min" + "\n";
            position += 1;   
        }
    }


    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}
