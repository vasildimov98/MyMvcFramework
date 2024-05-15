using MyWebFramework.HTTP.Models;
using MyWebFramework.HTTP.Servers;

internal class WebFrameworkStarter
{
    private readonly IHttpServer httpServer;

    public WebFrameworkStarter(IHttpServer httpServer)
    {
        this.httpServer = httpServer;
    }

    public async Task StartAsync()
    {
        httpServer.AddRoute("/", HomePage);
        httpServer.AddRoute("/about", AboutPage);
        httpServer.AddRoute("/login", LoginPage);

        await httpServer.StartListeningAsync(80);
    }

    private HttpResponse HomePage(HttpRequest request)
    {
        throw new NotImplementedException();
    }

    private HttpResponse AboutPage(HttpRequest request)
    {
        throw new NotImplementedException();
    }

    private HttpResponse LoginPage(HttpRequest request)
    {
        throw new NotImplementedException();
    }
}