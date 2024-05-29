using System.ComponentModel.DataAnnotations;

namespace MyWebServer.App.Data.Models
{
    public class Trip
    {
        public Trip()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }

        [MaxLength(80)]
        public string Description { get; set; }

        public string? ImagePath { get; set; }

        public virtual ICollection<UserTrip> UserTrips { get; set; } = [];
    }
}
