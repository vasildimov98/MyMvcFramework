using MyWebServer.HTTP.Delegates;

namespace MyWebServer.HTTP.Servers
{
    public interface IHttpServer
    {
        void AddRoute(string url, HttpHandler handler);

        Task StartListeningAsync(int port);
    }
}
