using HttpMethod = MyWebServer.HTTP.Models.HttpMethod;

namespace MyWebServer.MVCFramework.Attributes
{
    public abstract class BaseAttribute : Attribute
    {
        public string? Url { get; set; }

        public abstract HttpMethod Method { get; }
    }
}
