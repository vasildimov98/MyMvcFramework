using MyWebServer.HTTP.Models;

namespace MyWebServer.MVCFramework
{
    public interface IMVCApplication
    {
        void ConfigureServices();

        Task Configure(List<Route> routes);
    }
}
