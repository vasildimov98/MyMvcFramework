using MyWebServer.HTTP.Delegates;

namespace MyWebServer.HTTP.Servers
{
    public interface IHttpServer
    {
        Task StartListeningAsync(int port);
    }
}
