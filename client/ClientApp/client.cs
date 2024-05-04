using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        try
        {
            string serverIP = "127.0.0.1";
            int port = 7545;

            TcpClient client = new TcpClient(serverIP, port);
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[1024];

            // Odczytanie danych z serwera
            StringBuilder responseData = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                responseData.Append(Encoding.ASCII.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            Console.WriteLine("Odczytane dane:");
            Console.WriteLine(responseData.ToString());

            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Błąd: " + e.Message);
        }
    }
}
