using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ClientServerCommunication.ClientServerCommunication Connection = new ClientServerCommunication.ClientServerCommunication();
    SaveLoadFile io = new SaveLoadFile();
    string player_data = "user_data.json";
    public static GameManager instance;
    private bool isPlayerDead;
    public bool gameHasEnded;
    private static List<GameObject> dontDestroyObjects = new List<GameObject>();
    private CreateTimeStamp timestamp = new CreateTimeStamp();

    public void GameOver()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("LoadGameOverScene", 2f);
            Invoke("OnExitDestroy", 3f);
        }
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Win()
    {
        if(gameHasEnded == false)
        {
            JObject player = JObject.Parse(io.Load_to_file(player_data));
            string string_to_add_to_leaderboard = $"{{\"Nick\": \"{player["Nickname"].ToString()}\", \"Win\": 1}}";
            MyData data = new MyData
            {
                method = "PUT",
                message = string_to_add_to_leaderboard
            };
            Connection.ConnectToServer();
            Connection.SendMessageToServer(data);
            string response2 = Connection.ReceiveMessageFromServer();
            Debug.Log(response2);
            gameHasEnded = true;
            Debug.Log("You Win");
            Invoke("LoadYouWinScene", 2f);
            Invoke("OnExitDestroy", 3f);
        }
    }

    void OnExitDestroy()
    {
        timestamp.Save_Time_Played();
        Destroy(gameObject);
    }

    void LoadYouWinScene()
    {
        SceneManager.LoadScene("YouWin");
    }
    

    void Awake()
    {
        gameHasEnded = false;
        isPlayerDead = false;
        if (instance == null)
        {
            instance = this;
            dontDestroyObjects.Add(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied()
    {
        isPlayerDead = true;
    }

    public bool IsPlayerDead()
    {
        return isPlayerDead;
    }

    [Serializable]
    public class MyData
    {
        public string method;
        public string message;
    }
}
