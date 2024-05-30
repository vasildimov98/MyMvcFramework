using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework.DependencyContainer;

namespace MyWebServer.MVCFramework
{
    public interface IMVCApplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);

        Task Configure(List<Route> routes);
    }
}
