using MyWebServer.App;
using MyWebServer.MVCFramework;

try
{
    await Host.CreateHostAsync(new StartUp(), 800);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());       
}



