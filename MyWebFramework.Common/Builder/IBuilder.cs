namespace MyWebFramework.Common.Builder
{
    internal interface IBuilder
    {
        public ResponseBuilder AppendLine(string line);

        public ResponseBuilder AppendNewLine();

        public string GetResponse();

    }
}