using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Set the IP address and port number to listen on
        IPAddress ipAddress = IPAddress.Any; // Listen on any available network interface
        int port = 80; // Port 80 is commonly used for HTTP

        // Create the TCP listener
        TcpListener listener = new TcpListener(ipAddress, port);

        try
        {
            // Start listening for incoming connection requests
            listener.Start();
            Console.WriteLine("Server started. Listening on port 80...");

            // Enter the listening loop
            while (true)
            {
                // Accept an incoming connection
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");

                // Handle the client connection in a separate thread or process
                HandleClient(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Stop listening for incoming connections
            listener.Stop();
        }
    }

    static void HandleClient(TcpClient client)
    {
        // Get the client's IP address and port number
        IPEndPoint clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
        Console.WriteLine($"Client IP address: {clientEndPoint.Address}, Port: {clientEndPoint.Port}");

        // Get the client's network stream
        NetworkStream stream = client.GetStream();

        // Read data from the client
        byte[] buffer = new byte[1024];
        int bytesRead;
        StringBuilder data = new StringBuilder();
        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            // Convert the bytes to a string and append to the data StringBuilder
            data.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));
        }

        // Display the received data
        Console.WriteLine($"Received data from client: {data}");

        // Send a response to the client
        string response = "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\nHello, client!";
        byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
        stream.Write(responseBuffer, 0, responseBuffer.Length);

        // Close the connection
        client.Close();
        Console.WriteLine("Client disconnected.");
    }
}
