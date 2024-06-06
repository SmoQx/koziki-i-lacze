using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Linq;

public class MessageHandler : MonoBehaviour
{
    public string HandleMessage(string jsonMessage)
    {
        JObject messageObject = JObject.Parse(jsonMessage);
        string message = messageObject["message"].ToString();
        return message;
    }
}
