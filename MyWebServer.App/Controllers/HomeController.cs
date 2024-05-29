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
            var homeModel = new HomeViewModel
            {
                SessionText = this.Request.Session.ContainsKey("about") ?
                    "Hello from about" :
                    "I have not been to about page yet!"
            };

            return this.View(homeModel);
        }

        [HttpGet("/about")]
        public HttpResponse About()
        {
            this.Request.Session["about"] = "YES!";
            return this.View();
        }
    }
}
