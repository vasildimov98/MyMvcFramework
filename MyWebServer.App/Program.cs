using MyWebFramework.HTTP.Servers;
using MyWebFramework.Common.Interactors;

var consoleUserInteractor = new CosoleUserInteractor();

var httpServer = new HttpServer(consoleUserInteractor);

var startUp = new StartUp(httpServer);

await startUp.StartAsync();
