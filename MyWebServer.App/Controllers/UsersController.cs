using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;
using System.Text;

namespace MyWebServer.App.Controllers
{
    internal class UsersController : Controller
    {
        public HttpResponse Login(HttpRequest request)
        {
            return View();
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }
    }
}
