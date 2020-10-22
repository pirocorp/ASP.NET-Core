namespace Panda.App.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using Panda.Models;

    [AllowAnonymous]
    #pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
    #pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<PandaUser> signInManager;
        private readonly RoleManager<PandaRole> roleManager;
        private readonly UserManager<PandaUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<PandaUser> userManager,
            SignInManager<PandaUser> signInManager,
            RoleManager<PandaRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        #pragma warning disable SA1201 // Elements should appear in the correct order
        public async Task OnGetAsync(string returnUrl = null)
        #pragma warning restore SA1201 // Elements should appear in the correct order
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new PandaUser { UserName = this.Input.Email, Email = this.Input.Email };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    if (this.userManager.Users.Count() == 1)
                    {
                        const string adminRoleName = "Admin";

                        if (!await this.roleManager.RoleExistsAsync(adminRoleName))
                        {
                            var role = new PandaRole()
                            {
                                Name = adminRoleName,
                            };

                            await this.roleManager.CreateAsync(role);
                        }

                        await this.userManager.AddToRoleAsync(user, "Admin");
                    }

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
