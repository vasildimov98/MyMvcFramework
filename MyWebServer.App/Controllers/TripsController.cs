using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;

namespace MyWebServer.App.Controllers
{
    public class TripsController : Controller
    {
        public HttpResponse All(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Add(HttpRequest request) 
        {
            return this.View();
        }

       public HttpResponse Details(HttpRequest request)
        {
            return this.View();
        }
    }
}
