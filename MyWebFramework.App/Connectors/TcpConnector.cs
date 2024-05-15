using System.Net;
using System.Net.Sockets;

internal class TcpConnector(IPAddress loopback, int port, IUserInteractor userInteractor) : IConnector
{
    private readonly TcpListener tcpListener = new(loopback, port);

    public void StartListening()
    {
        this.tcpListener.Start();
        userInteractor.ShowMessage($"Server started on port {port}. Waiting for connections...");
        userInteractor.AddNewline();
    }

    public TcpClient AcceptTcpClient()
    {
        return this.tcpListener.AcceptTcpClient();
    }

    public void Dispose()
    {
        tcpListener.Stop();
    }
}
