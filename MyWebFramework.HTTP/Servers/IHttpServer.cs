using MyWebFramework.HTTP.Models;

namespace MyWebFramework.HTTP.Servers
{
    public interface IHttpServer
    {
        void AddRoute(string url, Func<HttpRequest, HttpResponse> handler);

        void StartListening();
    }
}
