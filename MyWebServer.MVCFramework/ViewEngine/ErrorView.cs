
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

            html.AppendLine($"<h1>View Compile Errors ({this.errors.Count()})")
        }
    }
}