using MyWebServer.Common.Constants;
using System.Net;
using System.Text;

namespace MyWebServer.HTTP.Models
{
    public class HttpRequest
    {
        private static Dictionary<string, Dictionary<string, string>> Sessions = [];

        public HttpRequest(string httpRequest)
        {
            InitializeRequest(httpRequest);
        }

        public HttpMethod Method { get; set; }

        public string? Path { get; set; }

        public ICollection<Header> Headers { get; set; } = [];

        public ICollection<Cookie> Cookies { get; set; } = [];

        public Dictionary<string, string> Session { get; set; } = [];

        public Dictionary<string, string> FormData { get; set; } = [];

        public string? Body { get; set; }

        private void InitializeRequest(string httpRequest)
        {
            var lines = httpRequest.Split(HttpConstants.NEW_LINE, StringSplitOptions.None);

            var firstLineParts = lines[0].Split(" ");

            Method = Enum.Parse<HttpMethod>(firstLineParts[0], true);
            Path = firstLineParts[1];

            var bodyStartIndex = AddHeaders(lines);

            this.Body = CreateBody(lines, bodyStartIndex);

            TryExtractCookies();

            TryExtractSession();

            if (!string.IsNullOrWhiteSpace(Body))
            {
                FillFormData();
            }
        }

        private void TryExtractSession()
        {
            var sessionCookie = this.Cookies
                .FirstOrDefault(x => x.Name == HttpConstants.SESSION_COOKIE_NAME);

            if (sessionCookie == null)
            {
                var sessionId = Guid.NewGuid().ToString();
                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(HttpConstants.SESSION_COOKIE_NAME, sessionId));
                return;
            }

            if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                var sessionId = sessionCookie.Value;
                Sessions.Add(sessionId, this.Session);
                return;
            }

            this.Session = Sessions[sessionCookie.Value];
        }

        private static string CreateBody(string[] lines, int bodyStartIndex)
        {
            var bodyBuilder = new StringBuilder();
            for (int i = bodyStartIndex; i < lines.Length; i++)
            {
                bodyBuilder.AppendLine(lines[i]);
            }

            return bodyBuilder.ToString();
        }

        private int AddHeaders(string[] lines)
        {
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];

                if (string.IsNullOrWhiteSpace(line))
                {
                    return i + 1;
                }

                Headers.Add(new Header(line));
            }

            return -1;
        }

        private void FillFormData()
        {
            var datum = this.Body
                 .Split('&', StringSplitOptions.RemoveEmptyEntries)
                 .Select(x => new
                 {
                     Name = x.Split('=')[0],
                     Value = WebUtility.UrlDecode(x.Split("=")[1])
                 });

            foreach (var data in datum)
            {
                if (this.FormData.ContainsKey(data.Name))
                {
                    continue;
                }

                this.FormData.Add(data.Name, data.Value);
            }
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
