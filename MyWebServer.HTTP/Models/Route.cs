using MyWebServer.HTTP.Delegates;

namespace MyWebServer.HTTP.Models
{
    public class Route(string url, HttpMethod httpMethod, HttpHandler handler)
    {
        public string Url { get; set; } = url;

        public HttpMethod Method { get; set; } = httpMethod;

        public HttpHandler Handler { get; set; } = handler;
    }
}
