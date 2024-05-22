using MyWebServer.App.Controllers;
using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;

namespace MyWebServer.App
{
    public class StartUp : IMVCApplication
    {
        public void Configure(List<Route> routeTable)
        {
            routeTable.Add(new Route("/", HTTP.Models.HttpMethod.Get, new HomeController().Index));
            routeTable.Add(new Route("/users/login", HTTP.Models.HttpMethod.Get, new UsersController().Login));
            routeTable.Add(new Route("/users/register", HTTP.Models.HttpMethod.Get, new UsersController().Register));
            routeTable.Add(new Route("/trips/all", HTTP.Models.HttpMethod.Get, new TripsController().All));
            routeTable.Add(new Route("/trips/add", HTTP.Models.HttpMethod.Get, new TripsController().Add));
            routeTable.Add(new Route("/trips/details", HTTP.Models.HttpMethod.Get, new TripsController().Details));

            routeTable.Add(new Route("/favicon.ico", HTTP.Models.HttpMethod.Get, new StaticContoller().Favicon));
            routeTable.Add(new Route("/css/bootstrap.css", HTTP.Models.HttpMethod.Get, new StaticContoller().Bootstrap));
        }

        public void ConfigureServices()
        {
        }
    }
}