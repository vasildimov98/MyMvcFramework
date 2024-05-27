
using System.Text;

namespace MyWebServer.MVCFramework.ViewEngine
{
    internal class ErrorView : IView
    {
        private readonly IEnumerable<string> errors;
        private readonly string chsarpCode;

        public ErrorView(IEnumerable<string> errors, string csharpCode)
        {
            this.errors = errors;
            this.chsarpCode = csharpCode;
        }

        public string ExecuteTemplate(object model)
        {
            var html = new StringBuilder();

            html.AppendLine($"<h1>View Compile Errors (Count: {this.errors.Count()}):</h1><ul>");

            foreach (var error in errors)
            {
                html.AppendLine($"<li>{error}</li>");
            }

            html
                .AppendLine($"</ul><pre>{this.chsarpCode}</pre>");

            return html.ToString();
        }
    }
}