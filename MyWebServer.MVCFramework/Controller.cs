using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework.ViewEngine;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyWebServer.MVCFramework
{
    public class Controller
    {
        private readonly MyViewEngine _viewEngine = new();

        protected HttpRequest? Request { get; set; }

        protected HttpResponse View(object model = null,[CallerMemberName]string viewName = "")
        {
            var sharedBody = System.IO.File
                .ReadAllText("Views/Shared/Layout.cshtml");

            var htmlBody = System.IO.File
                .ReadAllText($"Views/{this
                    .GetType().Name
                        .Replace("Controller", string.Empty)}/{viewName}.cshtml");

            sharedBody = sharedBody
                .Replace("@RenderBody()", htmlBody);

            var responseHtml = this._viewEngine.GenerateHTML(sharedBody, model);

            var htmlBytes = Encoding.UTF8
                .GetBytes(responseHtml);

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
