using MyWebFramework.Common.Constant;
using System.Text;

namespace MyWebFramework.Common.Builder
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
            respose.AppendLine(HttpConstants.NEW_LINE);

            return this;
        }

        public string GetResponseAsString()
        {
            return respose.ToString();
        }
    }
}