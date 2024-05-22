using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;

namespace MyWebServer.App.Controllers
{
    public class StaticContoller : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            return this.Favicon();
        }

        public HttpResponse Bootstrap(HttpRequest request)
        {
            return this.File("css/bootstrap.css", "text/css");
        }
    }
}
