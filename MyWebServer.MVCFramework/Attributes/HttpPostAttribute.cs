using HttpMethod = MyWebServer.HTTP.Models.HttpMethod;

namespace MyWebServer.MVCFramework.Attributes
{
    public class HttpPostAttribute : BaseAttribute
    {
        public HttpPostAttribute()
        {
        }

        public HttpPostAttribute(string url)
        {
        }

        public override HttpMethod Method => HttpMethod.Post;
    }
}
