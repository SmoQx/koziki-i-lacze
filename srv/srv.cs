using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class JsonRequest
{
    public string? Message { get; set; }
}

public class JsonResponse
{
    public string? Message { get; set; }
    public int StatusCode { get; set; }
    public string? ContentType { get; set; }
}


class Program
{
    static async Task Main(string[] args)
    {
        int port = 80; // Change to the desired port number

        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Server listening on port {port}...");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected.");

            await HandleClientAsync(client);

            Console.WriteLine("Client disconnected.");
        }
    }

//    static void HandleClient(TcpClient client)
//    {
//        // Get the client's IP address and port number
//        IPEndPoint clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
//        Console.WriteLine($"Client IP address: {clientEndPoint.Address}, Port: {clientEndPoint.Port}");
//
//        // Get the client's network stream
//        NetworkStream stream = client.GetStream();
//
//        // Read data from the client
//        byte[] buffer = new byte[1024];
//        inxt bytesRead;
//        StringBuilder data = new StringBuilder();
////        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
////        {
////            // Convert the bytes to a string and append to the data StringBuilder
////            data.Append(Encoding.ASCII.GetString(buffer, 0, bytesRead));
////        }
//
//        // Display the received data
//        Console.WriteLine($"Received data from client: {data}");
//
//        // Send a response to the client
//        string response = "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\nHello, client!";
//        byte[] responseBuffer = Encoding.ASCII.GetBytes(response);
//        stream.Write(responseBuffer, 0, responseBuffer.Length);
//
//        // Close the connection
//        client.Close();
//        Console.WriteLine("Client disconnected.");
//    }

    static async Task HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);
        try{
        // Read JSON data from the client
            string jsonData = await reader.ReadToEndAsync();
            Console.WriteLine();
            // Deserialize JSON data
            var request = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonRequest>(jsonData);
            Console.WriteLine(request);
            // Process the JSON data (e.g., perform some operation)
            string message = $"Received message: {jsonData}";
            // Prepare response JSON
            var response = new JsonResponse
            {
                Message = message,
                StatusCode = 200,
                ContentType = "application/json"
            };
            string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            // Send response back to the client
            await writer.WriteLineAsync(jsonResponse);
            await writer.FlushAsync();
        }
        catch (Exception e){
            Console.WriteLine(e);
        }
    }
}
