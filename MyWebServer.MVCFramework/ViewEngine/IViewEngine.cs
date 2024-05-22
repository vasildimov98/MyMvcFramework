namespace MyWebServer.MVCFramework.ViewEngine
{
    internal interface IViewEngine
    {
        string GenerateHTML(string templateCode, object model);
    }
}
