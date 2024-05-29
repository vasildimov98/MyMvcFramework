using MyWebServer.App.ViewModels;
using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;
using MyWebServer.MVCFramework.Attributes;

namespace MyWebServer.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            return this.View();
        }

        [HttpGet("/about")]
        public HttpResponse About()
        {
            this.SignIn("vasko");
            return this.View();
        }
    }
}
