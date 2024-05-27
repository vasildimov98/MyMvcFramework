using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;

namespace MyWebServer.App.Controllers
{
    public class TripsController : Controller
    {
        public HttpResponse All()
        {
            return this.View();
        }

        public HttpResponse Add() 
        {
            return this.View();
        }

       public HttpResponse Details()
        {
            return this.View();
        }
    }
}
