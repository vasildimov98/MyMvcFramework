using System.ComponentModel.DataAnnotations;

namespace MyWebServer.MVCFramework
{
    public abstract class IdentityUser<T>
    {
        public T Id { get; set; }

        [MaxLength(20)]
        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public IdentityRole Role { get; set; } = IdentityRole.User;
    }
}
