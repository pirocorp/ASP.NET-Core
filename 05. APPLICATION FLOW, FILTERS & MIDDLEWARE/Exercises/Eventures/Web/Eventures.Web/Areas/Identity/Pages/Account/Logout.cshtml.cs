namespace Eventures.Web.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Eventures.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<EventuresUser> signInManager;

        public LogoutModel(SignInManager<EventuresUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await this.signInManager.SignOutAsync();

            return this.Redirect("/");
        }
    }
}
