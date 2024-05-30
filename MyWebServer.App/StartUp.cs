using Microsoft.EntityFrameworkCore;
using MyWebServer.App.Data;
using MyWebServer.App.Services;
using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;
using MyWebServer.MVCFramework.DependencyContainer;

namespace MyWebServer.App
{
    public class StartUp : IMVCApplication
    {
        private readonly SharedTripContext _context;

        public StartUp()
        {
            this._context = new SharedTripContext();
        }

        public async Task Configure(List<Route> routeTable)
        {
            await this._context.Database.MigrateAsync();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();

        }

    }
}