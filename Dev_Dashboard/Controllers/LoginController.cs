using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;
using Dev_Dashboard.Services;
using Dev_Dashboard.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace Dev_Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ICommonService _commenService;
        private static WebSocket _webSocket;
        public LoginController(ICommonService commenService)
        {
            _commenService = commenService;
        }

        [HttpPost("Login")]
        public Task<CommonResponseModel> Login(LoginDTO userDetail)
        {
            _commenService.WebSockets();

            return _commenService.Login(userDetail);
        }

        //[HttpGet("connect")]
        //public async Task<IActionResult> ConnectWebSocket(string name)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        return BadRequest("Name cannot be empty.");
        //    }

        //    var ws = new ClientWebSocket();

        //    try
        //    {
        //        await ws.ConnectAsync(new Uri($"ws://localhost:6969/ws?name={name}"), CancellationToken.None);

        //        var receiveTask = Task.Run(async () =>
        //        {
        //            var buffer = new byte[1024 * 4];
        //            while (true)
        //            {
        //                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        //                if (result.MessageType == WebSocketMessageType.Close)
        //                {
        //                    break;
        //                }

        //                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        //                Console.WriteLine(message);
        //            }
        //        });

        //        var sendTask = Task.Run(async () =>
        //        {
        //            while (true)
        //            {
        //                Console.Write("Enter any msg: ");
        //                var message = Console.ReadLine();

        //                if (message == "exit")
        //                {
        //                    break;
        //                }

        //                var bytes = Encoding.UTF8.GetBytes(message);
        //                await ws.SendAsync(new ArraySegment<byte>(bytes),
        //                    WebSocketMessageType.Text, true,
        //                    CancellationToken.None);
        //            }
        //        });

        //        await Task.WhenAny(sendTask, receiveTask);

        //        if (ws.State != WebSocketState.Closed)
        //        {
        //            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        //        }

        //        await Task.WhenAll(sendTask, receiveTask);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions appropriately
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return StatusCode(500, $"Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        ws.Dispose(); // Dispose WebSocket to release resources
        //    }

        //    return Ok("WebSocket connection closed.");
        //}

        [HttpGet("connect")]
        public async Task<ActionResult<CommonResponseModel>> ConnectWebSocket(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: "Name cannot be empty.", Data: null);
            }

            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    _webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    await HandleWebSocket(name, _webSocket);
                    return new CommonResponseModel(StatusCode: 200, Success: true, Message: "WebSocket connection established.", Data: null);
                }
                else
                {
                    return new CommonResponseModel(StatusCode: 500, Success: false, Message: "WebSocket connection is required.", Data: null);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: $"Error: {ex.Message}", Data: null);
            }
        }


        private async Task HandleWebSocket(string name, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }

                    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received message from {name}: {receivedMessage}");

                    // Process received message as needed

                    // Echo the message back to the client
                    var bytes = Encoding.UTF8.GetBytes($"Server: {receivedMessage}");
                    await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }

                webSocket.Dispose();
            }
        }
    }
}
