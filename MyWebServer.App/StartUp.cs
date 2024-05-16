using MyWebFramework.HTTP.Models;
using MyWebFramework.HTTP.Servers;
using System.Text;

internal class StartUp(IHttpServer httpServer)
{
    private readonly IHttpServer httpServer = httpServer;

    public async Task StartAsync()
    {
        httpServer.AddRoute("/", HomePage);
        httpServer.AddRoute("/about", AboutPage);
        httpServer.AddRoute("/login", LoginPage);

        await httpServer.StartListeningAsync(80);
    }

    private HttpResponse HomePage(HttpRequest request)
    {
        var html = $"<h1>Home page from VaskoServer {DateTime.Now}</h1>";

        var byteBody = Encoding.UTF8.GetBytes(html);
        var httpResponse = new HttpResponse("text/html; charset=utf-8", byteBody);

        return httpResponse;
    }

    private HttpResponse AboutPage(HttpRequest request)
    {
        var html = $"<h1>About Page from VaskoServer {DateTime.Now}</h1>";

        var byteBody = Encoding.UTF8.GetBytes(html);
        var httpResponse = new HttpResponse("text/html; charset=utf-8", byteBody);

        return httpResponse;
    }

    private HttpResponse LoginPage(HttpRequest request)
    {
        var html = $"<h1>Login Page from VaskoServer {DateTime.Now}</h1>";

        var byteBody = Encoding.UTF8.GetBytes(html);
        var httpResponse = new HttpResponse("text/html; charset=utf-8", byteBody);

        return httpResponse;
    }
}