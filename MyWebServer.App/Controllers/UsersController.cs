using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;
using System.Text;

namespace MyWebServer.App.Controllers
{
    internal class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }
    }
}
