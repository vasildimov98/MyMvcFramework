
using HeyRed.Mime;
using MyWebFramework.Common.Interactors;
using MyWebServer.HTTP.Models;
using MyWebServer.HTTP.Servers;
using MyWebServer.MVCFramework.Attributes;
using System.Reflection;
using HttpMethod = MyWebServer.HTTP.Models.HttpMethod;

namespace MyWebServer.MVCFramework
{
    public class Host
    {
        public static async Task CreateHostAsync(IMVCApplication application, int port)
        {
            var consoleUserInteractor = new CosoleUserInteractor();

            var routes = new List<Route>();

            RegisterStaticFiles(routes);
            RegisterControllerRoutes(routes, application);

            consoleUserInteractor.ShowMessage("Register Routes:");
            foreach (var route in routes)
            {
                consoleUserInteractor.ShowMessage($"{route.Method} => {route.Url}");
            }

            application.Configure(routes);
            application.ConfigureServices();

            var server = new HttpServer(routes, consoleUserInteractor);

            await server
                .StartListeningAsync(port);
        }

        private static void RegisterControllerRoutes(List<Route> routes, IMVCApplication application)
        {

            var controllerTypes = application
                .GetType().Assembly
                .GetTypes()
                .Where(x =>
                    x.IsSubclassOf(typeof(Controller)));

            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType
                    .GetMethods()
                    .Where(x =>
                    !x.IsAbstract &&
                    !x.IsSpecialName &&
                    !x.IsConstructor &&
                    x.IsPublic &&
                    x.DeclaringType == controllerType);

                foreach (var method in methods)
                {
                    var url = "/" + controllerType.Name
                        .Replace("Controller", string.Empty) + "/" + method.Name;

                    var attibute = method
                        .GetCustomAttributes<BaseAttribute>(false)
                        .FirstOrDefault();

                    var httpMethod = HttpMethod.Get;
                    if (attibute != null)
                    {
                        httpMethod = attibute.Method;
                    }

                    if (!string.IsNullOrWhiteSpace(attibute?.Url))
                    {
                        url = attibute?.Url;
                    }

                    routes.Add(new Route(url!, httpMethod, (request) =>
                    {
                        var instance = Activator.CreateInstance(controllerType) as Controller;

                        var requestProp = controllerType
                            .GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic);

                        requestProp?.SetValue(instance, request);

                        var response = method.Invoke(instance, null);

                        return (response as HttpResponse)!;
                    }));
                }
            }
        }

        private static void RegisterStaticFiles(List<Route> routes)
        {
            var staticFiles = Directory
                .GetFiles("wwwroot", "*", SearchOption.AllDirectories);

            foreach (var staticFile in staticFiles)
            {
                var url = staticFile
                    .Replace("wwwroot", string.Empty)
                    .Replace("\\", "/");
                routes.Add(new Route(url, HTTP.Models.HttpMethod.Get, (request) =>
                {
                    var contentType = MimeTypesMap.GetMimeType(url);
                    var byteResponse = File.ReadAllBytes(staticFile);
                    return new HttpResponse(contentType, byteResponse);
                }));
            }
        }
    }
}
