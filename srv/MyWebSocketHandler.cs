using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyWebApplication
{
    public class MyWebSocketHandler
    {
        public void OnConnected(WebSocket webSocket, HttpContext context)
        {
            if (context != null)
            {
                // Get the address of the connected client
                var clientAddress = context.Connection.RemoteIpAddress?.ToString();
                Console.WriteLine($"Client connected from: {clientAddress}");

                // Get the device information of the connected client (if available)
                var userAgent = context.Request.Headers["User-Agent"].ToString();
                Console.WriteLine($"User-Agent: {userAgent}");
            }
        }

        public async Task ReceiveLoop(WebSocket webSocket)
        {
            var buffer = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                // Respond with "Hello" to incoming messages
                await webSocket.SendAsync(Encoding.UTF8.GetBytes("Hello"), WebSocketMessageType.Text, true, System.Threading.CancellationToken.None);

                // Continue receiving messages
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            }
        }
    }
}
