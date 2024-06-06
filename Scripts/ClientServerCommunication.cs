namespace ClientServerCommunication
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using UnityEngine;
    using Newtonsoft.Json;


    public class ClientServerCommunication
    {
        private TcpClient client;
        private NetworkStream stream;

        // Server IP and port
        private string serverIp = "127.0.1.1"; // Server's IP address
        private int port = 55000; // Server's port

        public void ConnectToServer()
        {
            try
            {
                client = new TcpClient(serverIp, port);
                stream = client.GetStream();
                Debug.Log("Connected to server");
            }
            catch (Exception e)
            {
                Debug.LogError("Error connecting to server: " + e.Message);
            }
        }

        public void SendMessageToServer(object message)
        {
            if (client == null || !client.Connected)
            {
                Debug.LogError("Client is not connected to server.");
                return;
            }

            try
            {
                // Serialize the object to a JSON string
                string jsonString = JsonConvert.SerializeObject(message);
                jsonString += "\n";
                byte[] data = Encoding.UTF8.GetBytes(jsonString);
                stream.Write(data, 0, data.Length);
                Debug.Log("Message sent: " + jsonString);
            }
            catch (Exception e)
            {
                Debug.LogError("Error sending message: " + e.Message);
            }
        }

        public string ReceiveMessageFromServer()
        {
            if (client == null || !client.Connected)
            {
                Debug.LogError("Client is not connected to server.");
                return "error";
            }

            try
            {
                // Read the response from the server
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string response = Encoding.UTF8.GetString(data, 0, bytesRead);
                Debug.Log("Response received: " + response);
                MessageHandler messageHandler = new MessageHandler();
                string message = messageHandler.HandleMessage(response);
                Debug.Log(message);
                stream.Close();
                return message;
            }
            catch (Exception e)
            {
                Debug.LogError("Error receiving message: " + e.Message);
                return "error";
            }
        }

        public void OnApplicationQuit()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }

        // Example data class to be serialized to JSON
        [Serializable]
        public class MyData
        {
            public string method;
            public string message;
        }
    }
}