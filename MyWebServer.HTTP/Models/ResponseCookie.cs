using System.Text;

namespace MyWebServer.HTTP.Models
{
    public class ResponseCookie(string name, string value) : Cookie(name, value)
    {
        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Path { get; set; } = "/";

        public override string ToString()
        {
            var cookieBuilder = new StringBuilder();

            cookieBuilder.Append($"Set-Cookie: {base.ToString()}; Path={Path};");

            if (MaxAge != 0)
            {
                cookieBuilder.Append($" Max-Age={MaxAge};");
            }

            if (HttpOnly)
            {
                cookieBuilder.Append(" HttpOnly;");
            }

            return cookieBuilder.ToString();
        }
    }
}
