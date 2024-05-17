using MyWebServer.Common.Builder;

namespace MyWebServer.HTTP.Models
{
    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, StatusCode code = StatusCode.OK)
        {
            InializeResponse(code, contentType, body);
        }

        public StatusCode StatusCode { get; set; }

        public ICollection<Header>? Headers { get; set; }

        public ICollection<ResponseCookie> Cookies { get; set; } = [];

        public byte[]? Body { get; set; }

        public override string ToString()
        {
            var response = new ResponseBuilder()
                .AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers)
            {
                response
                    .AppendLine(header.ToString());
            }

            foreach (var cookie in Cookies)
            {
                response
                    .AppendLine(cookie.ToString());
            }

            response.AppendNewLine();

            return response.GetResponseAsString();
        }

        private void InializeResponse(StatusCode code, string contentType, byte[] body)
        {
            StatusCode = code;
            Headers = [
                    new Header("Content-Type", contentType) ,
                    new Header("Content-Length", body.Length.ToString()),
                ];
            Body = body;
        }
    }
}
