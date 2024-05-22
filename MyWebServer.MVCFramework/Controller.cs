using MyWebServer.HTTP.Models;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyWebServer.MVCFramework
{
    public class Controller
    {
        protected HttpResponse View([CallerMemberName]string viewName = "")
        {
            var sharedBody = System.IO.File.ReadAllText("Views/Shared/Layout.cshtml");
            var htmlBody = System.IO.File.ReadAllText($"Views/{this.GetType().Name.Replace("Controller", string.Empty)}/{viewName}.cshtml");

            sharedBody = sharedBody.Replace("@RenderBody()", htmlBody);

            var htmlBytes = Encoding.UTF8.GetBytes(sharedBody);

            var httpResponse = new HttpResponse("text/html; charset=utf-8", htmlBytes);

            return httpResponse;
        }

        protected HttpResponse Favicon(string fileName = "favicon.ico")
        {
            return this.File(fileName, "image/vnd.microsoft.icon");
        }

        protected HttpResponse File(string fileName, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes($"wwwroot/{fileName}");

            var response = new HttpResponse(contentType, fileBytes);
            return response;
        }
    }
}
