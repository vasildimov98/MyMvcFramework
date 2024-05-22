using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;
using System.Text;

namespace MyWebServer.App.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }

       
    }
}
