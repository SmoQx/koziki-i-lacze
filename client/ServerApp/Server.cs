using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        TcpListener server = null;
        try
        {
            // Adres IP serwera i port
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 7545;
            server = new TcpListener(ipAddress, port);

            // Start serwera
            server.Start();
            Console.WriteLine("Serwer uruchomiony. Oczekiwanie na połączenie...");

            // Dane z pliku JSON
            string jsonData = File.ReadAllText("../leaderboard.json");

            while (true) // Pętla obsługi połączeń z klientami
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Połączono z klientem.");
                byte[] bytes = Encoding.ASCII.GetBytes(jsonData);
                NetworkStream stream = client.GetStream();
                stream.Write(bytes, 0, bytes.Length);
                Console.WriteLine("Wysłano dane do klienta.");

                client.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Błąd: " + e.Message);
        }
        finally
        {
            server.Stop();
        }
    }
}
