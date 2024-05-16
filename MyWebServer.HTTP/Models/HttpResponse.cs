
using MyWebFramework.Common.Builder;

namespace MyWebFramework.HTTP.Models
{
    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, StatusCode code = StatusCode.OK)
        {
            this.InializeResponse(code, contentType, body);
        }

        public StatusCode StatusCode { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<ResponseCookie> Cookies { get; set; } = [];

        public byte[] Body { get; set; }

        public override string ToString()
        {
            var response = new ResponseBuilder()
                .AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");

            foreach (var header in this.Headers)
            {
                response
                    .AppendLine(header.ToString());
            }

            foreach (var cookie in this.Cookies)
            {
                response
                    .AppendLine(cookie.ToString());
            }

            response.AppendNewLine();

            return response.GetResponseAsString();
        }

        private void InializeResponse(StatusCode code, string contentType, byte[] body)
        {
            this.StatusCode = code;
            this.Headers = [
                    new Header("Content-Type",  contentType),
                    new Header("Content-Length", body.Length.ToString())
                ];
            this.Body = body;
        }
    }
}
