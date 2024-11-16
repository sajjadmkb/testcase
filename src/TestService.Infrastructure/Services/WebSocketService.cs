using TestService.Domain.Models;
using TestService.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestService.Infrastructure.Services
{
    public class WebSocketService : IWebSocketPublisher
    {
        private static readonly ConcurrentDictionary<string, WebSocket> _subscribers = new ConcurrentDictionary<string, WebSocket>();
        private readonly ILogger<WebSocketService> _logger;

        public WebSocketService(ILogger<WebSocketService> logger)
        {
            _logger = logger;
        }

        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                var clientId = System.Guid.NewGuid().ToString();
                _subscribers.TryAdd(clientId, socket);
                await ReceiveMessagesAsync(socket, clientId);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        public async Task ReceiveMessagesAsync(WebSocket socket, string clientId)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result;
            do
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    _subscribers.TryRemove(clientId, out _);
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
            } while (!result.CloseStatus.HasValue);
        }

        public async Task PublishPriceUpdateAsync(PriceUpdate priceUpdate)
        {
            var message = $"Price Update: {priceUpdate.Symbol} - {priceUpdate.Price}";
            var buffer = Encoding.UTF8.GetBytes(message);
            foreach (var socket in _subscribers.Values)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
