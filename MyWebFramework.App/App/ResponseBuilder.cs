using System.Text;

namespace MyWebFramework.App.App
{
    internal class ResponseBuilder : IBuilder
    {
        private const string NEW_LINE = "\r\n";
        private readonly StringBuilder respose = new();

        public ResponseBuilder AppendLine(string line)
        {
            respose.Append(line + NEW_LINE);

            return this;
        }

        public string GetResponse()
        {
            return respose.ToString().TrimEnd();
        }

        public ResponseBuilder AppendNewLine()
        {
            respose.AppendLine(NEW_LINE);

            return this;
        }
    }
}