using System.ComponentModel.DataAnnotations;

namespace MyWebServer.MVCFramework
{
    public abstract class UserIdentity
    {
        public required string Id { get; set; }

        [MaxLength(20)]
        public required string Username { get; set; }

        public required string Email { get; set; }

        [MaxLength(20)]
        public required string Password { get; set; }
    }
}
