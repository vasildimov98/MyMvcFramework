using MyWebServer.MVCFramework;

namespace MyWebServer.App.Data.Models
{
    public class User : UserIdentity
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public virtual ICollection<UserTrip> UserTrips { get; set; } = [];
    }
}
