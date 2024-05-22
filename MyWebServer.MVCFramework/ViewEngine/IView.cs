namespace MyWebServer.MVCFramework.ViewEngine
{
    internal interface IView
    {
        string ExecuteTemplate(object model);
    }
}
