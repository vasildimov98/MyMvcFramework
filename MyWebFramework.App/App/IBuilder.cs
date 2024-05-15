using MyWebFramework.App.App;

internal interface IBuilder
{
    public ResponseBuilder AppendLine(string line);

    public ResponseBuilder AppendNewLine();

    public string GetResponse();

}