namespace Eventures.Web.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Eventures.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<EventuresUser> signInManager;
        private readonly UserManager<EventuresUser> userManager;

        public RegisterModel(
            UserManager<EventuresUser> userManager,
            SignInManager<EventuresUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string Firstname { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string Lastname { get; set; }

            [Required]
            [Display(Name = "UCN")]
            public string Ucn { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var user = new EventuresUser
            {
                UserName = this.Input.Username, 
                Email = this.Input.Email,
                Firstname = this.Input.Firstname,
                Lastname = this.Input.Lastname,
                Ucn = this.Input.Ucn
            };

            var result = await this.userManager
                .CreateAsync(user, this.Input.Password);
            
            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }

                await this.signInManager.SignInAsync(user, isPersistent: false);
                return this.LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
