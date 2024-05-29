using System.Drawing;

namespace MyWebServer.App.Services
{
    public interface IUserService
    {
        void CreateUser(string username, string email, string password);

        string? GetUserId(string username, string password);

        bool IsEmailAvailable(string email);

        bool IsUsernameAvailable(string username);
    }
}
