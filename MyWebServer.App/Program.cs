using MyWebFramework.Common.Interactors;
using MyWebServer.App;
using MyWebServer.HTTP.Servers;

var consoleUserInteractor = new CosoleUserInteractor();

var httpServer = new HttpServer(consoleUserInteractor);

var startUp = new StartUp(httpServer);

await startUp.StartAsync();
