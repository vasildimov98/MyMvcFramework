using MyWebServer.HTTP.Models;

namespace MyWebServer.HTTP.Delegates
{
    public delegate HttpResponse HttpHandler(HttpRequest request);
}
