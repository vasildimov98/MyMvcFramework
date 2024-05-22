using MyWebServer.HTTP.Models;

namespace MyWebServer.MVCFramework
{
    public interface IMVCApplication
    {
        void ConfigureServices();

        void Configure(List<Route> routes);
    }
}
