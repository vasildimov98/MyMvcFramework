using System.Net.Sockets;

public interface IConnector : IDisposable
{
    internal void StartListening();

    internal TcpClient AcceptTcpClient();
}