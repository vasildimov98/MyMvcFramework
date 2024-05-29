namespace MyWebServer.MVCFramework.ViewEngine
{
    public interface IView
    {
        string ExecuteTemplate(object? model);
    }
}
