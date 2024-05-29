using Microsoft.EntityFrameworkCore;
using MyWebServer.App.Data;
using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;

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

        public void ConfigureServices()
        {
        }
    }
}