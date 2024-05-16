
using MyWebFramework.Common.Constant;
using System.Text;

namespace MyWebFramework.HTTP.Models
{
    public class HttpRequest
    {
        public HttpRequest(string httpRequest)
        {
            this.Headers = [];
            this.Cookies = [];

            this.InitializeRequest(httpRequest);
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

            this.Method = Enum.Parse<HttpMethod>(firstLineParts[0], true);
            this.Path = firstLineParts[1];

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
                    this.Headers.Add(new Header(line));
                    continue;
                }

                bodyBuilder.AppendLine(line);
            }

            this.TryExtractCookies();

            this.Body = bodyBuilder.ToString();
        }

        private void TryExtractCookies()
        {
            var cookieHeader = this.Headers.FirstOrDefault(x => x.Name == HttpConstants.REQUEST_COOKIE_HEADER);

            if (cookieHeader == null)
            {
                return;
            }

            var cookies = cookieHeader.Value.Split("; ", StringSplitOptions.RemoveEmptyEntries);

            foreach ( var cookie in cookies)
            {
                this.Cookies.Add(new Cookie(cookie.Trim()));
            }

        }
    }
}
