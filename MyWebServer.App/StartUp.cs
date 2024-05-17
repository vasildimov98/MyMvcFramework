using MyWebServer.HTTP.Models;
using MyWebServer.HTTP.Servers;
using System.Text;

namespace MyWebServer.App
{
    internal class StartUp(IHttpServer httpServer)
    {
        private readonly IHttpServer httpServer = httpServer;

        public async Task StartAsync()
        {
            httpServer.AddRoute("/", HomePage);
            httpServer.AddRoute("/about", AboutPage);
            httpServer.AddRoute("/login", LoginPage);
            httpServer.AddRoute("/register", RegisterPage);
            httpServer.AddRoute("/favicon.ico", Favicon);

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
            var httpResponse = new HttpResponse("text/html", byteBody);

            return httpResponse;
        }

        private HttpResponse LoginPage(HttpRequest request)
        {
            var html = $"<h1>Login Page from VaskoServer {DateTime.Now}</h1>";

            var byteBody = Encoding.UTF8.GetBytes(html);
            var httpResponse = new HttpResponse("text/html", byteBody);

            return httpResponse;
        }

        private HttpResponse RegisterPage(HttpRequest request)
        {
            var html = $"<h1>Register Page from VaskoServer {DateTime.Now}</h1>";

            var byteBody = Encoding.UTF8.GetBytes(html);
            var httpResponse = new HttpResponse("text/html", byteBody);

            return httpResponse;
        }

        private HttpResponse Favicon(HttpRequest request)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");

            var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes);

            return response;
        }
    }
}