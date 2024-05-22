
using MyWebFramework.Common.Interactors;
using MyWebServer.HTTP.Models;
using MyWebServer.HTTP.Servers;

namespace MyWebServer.MVCFramework
{
    public class Host
    {
        public static async Task CreateHostAsync(IMVCApplication application, int port)
        {
            var routes = new List<Route>();

            application.Configure(routes);
            application.ConfigureServices();

            var consoleUserInteractor = new CosoleUserInteractor();

            IHttpServer server = new HttpServer(routes, consoleUserInteractor);

            await server.StartListeningAsync(port);
        }
    }
}
