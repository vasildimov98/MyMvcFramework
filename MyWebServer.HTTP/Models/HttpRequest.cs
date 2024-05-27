using MyWebServer.Common.Constants;
using System.Text;

namespace MyWebServer.HTTP.Models
{
    public class HttpRequest
    {
        public HttpRequest(string httpRequest)
        {
            Headers = [];
            Cookies = [];

            InitializeRequest(httpRequest);
        }

        public HttpMethod Method { get; set; }

        public string Path { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public string? Body { get; set; }

        private void InitializeRequest(string httpRequest)
        {
            var lines = httpRequest.Split(HttpConstants.NEW_LINE, StringSplitOptions.None);

            var firstLineParts = lines[0].Split(" ");

            Method = Enum.Parse<HttpMethod>(firstLineParts[0], true);
            Path = firstLineParts[1];

            var isHeader = true;
            var bodyBuilder = new StringBuilder();
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];

                if (string.IsNullOrWhiteSpace(line))
                {
                    isHeader = false;
                    continue;
                }

                if (isHeader)
                {
                    Headers.Add(new Header(line));
                    continue;
                }

                bodyBuilder.AppendLine(line);
            }

            TryExtractCookies();

            Body = bodyBuilder.ToString();
        }

        private void TryExtractCookies()
        {
            var cookieHeader = Headers.FirstOrDefault(x => x.Name == HttpConstants.REQUEST_COOKIE_HEADER);

            if (cookieHeader == null)
            {
                return;
            }

            var cookies = cookieHeader.Value.Split("; ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var cookie in cookies)
            {
                Cookies.Add(new Cookie(cookie.Trim()));
            }

        }
    }
}
