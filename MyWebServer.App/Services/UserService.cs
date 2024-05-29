using MyWebServer.App.Data;
using MyWebServer.App.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace MyWebServer.App.Services
{
    public class UserService : IUserService
    {
        private readonly SharedTripContext sharedTripContext = new();

        public void CreateUser(string username, string email, string password)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = HashPassword(password),
            };

            this.sharedTripContext.Add(user);

            this.sharedTripContext.SaveChanges();
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.sharedTripContext.Users
                .Any(u => u.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.sharedTripContext.Users
                .Any(u => u.Username == username);
        }

        public string? GetUserId(string username, string password)
        {
            var user = this.sharedTripContext.Users
                .FirstOrDefault(u => u.Username == username &&
                     u.Password == HashPassword(password));

            return user != null ? user.Id : null;
        }

        private static string HashPassword(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            var hashedInputBytes = SHA512.HashData(bytes);

            var hashedInputStringBuilder = new StringBuilder(128);

            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));

            return hashedInputStringBuilder.ToString();
        }
    }
}
