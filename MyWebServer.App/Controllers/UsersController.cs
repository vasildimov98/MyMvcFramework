using MyWebServer.App.Services;
using MyWebServer.HTTP.Models;
using MyWebServer.MVCFramework;
using MyWebServer.MVCFramework.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MyWebServer.App.Controllers
{
    internal class UsersController : Controller
    {
        private readonly UserService _userService = new();

        public HttpResponse Login()
        {
            return View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var username = this.Request!.FormData["username"];
            var password = this.Request.FormData["password"];
            var userId = this._userService.GetUserId(username, password);

            if (userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);

            return Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (this.IsUserSignedIn())
                this.SingOut();

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var username = this.Request!.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmPassword = this.Request.FormData["confirmPassword"];

            if (string.IsNullOrWhiteSpace(username))
            {
                return this.Error("Username is required");
            }

            if (username.Length < 5 || username.Length >= 20)
            {
                return this.Error("Username is Invalid. Should be with min length 5 and max length 20");
            }

            if (!this._userService.IsUsernameAvailable(username))
            {
                return this.Error("Username is unavailable!");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return this.Error("Email is required!");
            }

            if (!new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Email is not valid!");
            }

            if (!this._userService.IsEmailAvailable(email))
            {
                return this.Error("Email in unavailable!");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return this.Error("Password is required!");
            }

            if (password.Length < 6 || username.Length >= 20)
            {
                return this.Error("Password is Invalid. Should be with min length 6 and max length 20");
            }

            if (password != confirmPassword)
            {
                return this.Error("Passwords are not matching!");
            }

            this._userService.CreateUser(username, email, password);


            return this.Redirect("/");
        }
    }
}
