using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace MyWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyWebSocketHandler _webSocketHandler;

        public HomeController(MyWebSocketHandler webSocketHandler)
        {
            _webSocketHandler = webSocketHandler;
        }

        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _webSocketHandler.OnConnected(webSocket, HttpContext);
                await _webSocketHandler.ReceiveLoop(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        [HttpGet]
        public IActionResult HandleBadRequest()
        {
            return StatusCode(400);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
