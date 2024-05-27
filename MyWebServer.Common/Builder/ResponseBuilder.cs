using MyWebServer.Common.Constants;
using System.Text;

namespace MyWebServer.Common.Builder
{
    public class ResponseBuilder : IBuilder
    {
        private readonly StringBuilder respose = new();

        public ResponseBuilder AppendLine(string line)
        {
            respose.Append(line + HttpConstants.NEW_LINE);

            return this;
        }

        public ResponseBuilder AppendNewLine()
        {
            respose.Append(HttpConstants.NEW_LINE);

            return this;
        }

        public string GetResponseAsString()
        {
            return respose.ToString();
        }
    }
}