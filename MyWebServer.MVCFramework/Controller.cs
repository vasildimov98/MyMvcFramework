using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework.ViewEngine;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyWebServer.MVCFramework
{
    public class Controller
    {
        private readonly MyViewEngine _viewEngine = new();

        protected HttpRequest? Request { get; set; }

        protected HttpResponse View(object? model = null, [CallerMemberName]string viewName = "")
        {
            var view = System.IO.File
              .ReadAllText($"Views/{this
                  .GetType().Name
                      .Replace("Controller", string.Empty)}/{viewName}.cshtml");

            string sharedBody = PutViewInLayout(view);

            var responseHtml = this._viewEngine.GenerateHTML(sharedBody, model);

            var htmlBytes = Encoding.UTF8
                .GetBytes(responseHtml);

            var httpResponse = new HttpResponse("text/html; charset=utf-8", htmlBytes);

            return httpResponse;
        }

        private string PutViewInLayout(string view)
        {
            var layout = System.IO.File
                            .ReadAllText("Views/Shared/Layout.cshtml");

            layout = layout
                .Replace("@RenderBody()", view);

            return layout;
        }

        protected HttpResponse Redirect(string url)
        {
            var httpResponse = new HttpResponse(StatusCode.Found);
            httpResponse.Headers!.Add(new Header("Location", url));
            return httpResponse;
        }

        protected HttpResponse Error(string message, object model = null)
        {
            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">{message}</div>";
            var layout = this.PutViewInLayout(viewContent);
            var responseHtml = this._viewEngine.GenerateHTML(layout, model);
            var responseBodyBytes = Encoding.UTF8
                .GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes, StatusCode.InternalServerError);
            return response;
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
