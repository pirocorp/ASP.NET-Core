namespace Eventures.Web.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Eventures.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<EventuresUser> _signInManager;

        public LogoutModel(SignInManager<EventuresUser> signInManager, ILogger<LogoutModel> logger)
        {
            this._signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await this._signInManager.SignOutAsync();

            return this.Redirect("/");
        }
    }
}
