using MyWebServer.App.Data;
using MyWebServer.App.Data.Models;
using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;
using MyWebServer.MVCFramework.Attributes;

namespace MyWebServer.App.Controllers
{
    public class TripsController(SharedTripContext sharedTripContext) : Controller
    {
        private readonly SharedTripContext sharedTripContext = sharedTripContext;

        public HttpResponse All()
        {
            return this.View();
        }

        public HttpResponse Add() 
        {
            return this.View();
        }

        [HttpPost("/trips/add")]
        public HttpResponse DoAdd()
        {
            var trip = new Trip
            {
                StartPoint = this.Request!.FormData["startPoint"],
                EndPoint = this.Request.FormData["endPoint"],
                DepartureTime = DateTime.Parse(this.Request.FormData["departureTime"]),
                ImagePath = this.Request.FormData["imagePath"],
                Seats = int.Parse(this.Request.FormData["seats"]),
                Description = this.Request.FormData["description"],
            };

            this.sharedTripContext.Add(trip);
            this.sharedTripContext.SaveChanges();

            return this.Redirect("/trips/all");
        }

       public HttpResponse Details()
        {
            return this.View();
        }
    }
}
