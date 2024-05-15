using MyWebFramework.HTTP.Delegates;
using MyWebFramework.HTTP.Models;

namespace MyWebFramework.HTTP.Servers
{
    public interface IHttpServer
    {
        void AddRoute(string url, HttpHandler handler);

        Task StartListeningAsync(int port);
    }
}
