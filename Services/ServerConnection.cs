using LibertyRustAcquiring.Settings;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;
using LibertyRustAcquiring.Interfaces;
using LibertyRustAcquiring.Data;
using Microsoft.EntityFrameworkCore;
using LibertyRustAcquiring.Models.Enums;

namespace LibertyRustAcquiring.Utils
{
    public class ServerConnection(
        ILogger<ServerConnection> logger
        ) : IServerConnection
    {      
        
        public async Task<string> SendCommand(ServerInfo server, string command)
        {
            var serverUri = new Uri($"ws://{server.Hostname}:{server.RconPort}/{server.RconPassword}");

            using var ws = new ClientWebSocket();

            var payload = new
            {
                Identifier = 1,
                Message = command,
                Name = "WebRcon"
            };

            try
            {
                await ws.ConnectAsync(serverUri, CancellationToken.None);

                var jsonString = JsonSerializer.Serialize(payload);

                var bytesToSend = Encoding.UTF8.GetBytes(jsonString);

                await ws.SendAsync(
                    new ArraySegment<byte>(bytesToSend),
                    WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken: CancellationToken.None
                );

                var buffer = new byte[4096];

                var result = await ws.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None
                );

                var responseString = Encoding.UTF8.GetString(buffer, 0, result.Count);

                using var doc = JsonDocument.Parse(responseString);

                var message = doc.RootElement.GetProperty("Message").GetString();

                logger.LogInformation("Result: {message}", message);

                return message;
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error: {message}", ex.Message);
                throw new Exception(ex.Message);
            }            
        }
    }
}

