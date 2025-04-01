using LibertyRustAcquiring.Settings;

namespace LibertyRustAcquiring.Interfaces
{
    public interface IServerConnection
    {
        Task<string> SendCommand(ServerInfo server, string command);
    }
}
