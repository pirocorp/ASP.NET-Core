namespace Chushka.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AccountController(SignInManager<User> signInManager, IUserService userService)
        {
            this._signInManager = signInManager;
            this._userService = userService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return null;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (this._userService.Exists(model.Username))
            {
                this.ModelState.AddModelError(nameof(model.Username), "Username is already taken");

                return this.View(model);
            }

            var user = new User()
            {
                Email = model.Email,
                FullName = model.FullName,
                UserName = model.Username,
            };

            var result = this._signInManager
                .UserManager
                .CreateAsync(user, model.Password)
                .Result;

            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        public IActionResult Logout()
        {
            return null;
        }
    }
}
