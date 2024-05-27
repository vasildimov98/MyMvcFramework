using HttpMethod = MyWebServer.HTTP.Models.HttpMethod;

namespace MyWebServer.MVCFramework.Attributes
{
    public class HttpGetAttribute : BaseAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }

        public override HttpMethod Method => HttpMethod.Get;
    }
}
