using MyWebFramework.HTTP.Servers;
using MyWebFramework.Common.Interactors;

var consoleUserInteractor = new CosoleUserInteractor();

var httpServer = new HttpServer(consoleUserInteractor);

var webFrameworkStarter = new WebFrameworkStarter(httpServer);

await webFrameworkStarter.StartAsync();
