using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DB.handlers;

class Program
{
    static void Main(string[] args)
    {
        StartListening(55000);
    }

    static void StartListening(int port)
    {
        try
        {
            // Establish the local endpoint for the socket
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0]; // Just using the first available IP address
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections
            listener.Bind(localEndPoint);
            listener.Listen(10); // Listen for maximum 10 connections

            Console.WriteLine($"Server listening on {localEndPoint}");

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept(); // Accept incoming connection

                // Incoming data from the client
                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine($"Received: {data}");

                // Handle the message using MessageHandler
                string response = MessageHandler.HandleMessage(data);

                // Echo the response back to the client
                byte[] msg = Encoding.ASCII.GetBytes(response);
                handler.Send(msg);

                // Release the socket
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error listening on port {port}: {e.ToString()}");
        }
    }
}
