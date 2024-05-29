using MyWebServer.MVCFramework;

namespace MyWebServer.App.Data.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<UserTrip> UserTrips { get; set; } = [];
    }
}
