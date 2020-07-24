namespace CameraBazaar.Web.Areas.Identity.Pages.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            private const string STRING_LENGTH_ERROR = "The {0} must be at least {2} and at max {1} characters long.";
            private const string USERNAME_FORMAT_ERROR = "Username must have only letters";
            private const string CONFIRM_PASSWORD_ERROR = "The password and confirmation password do not match.";
            private const string PHONE_FORMAT_ERROR = "Phone must start with '+' sign and contain between 10 and 12 symbols.";

            [Required]
            [StringLength(20, MinimumLength = 4, ErrorMessage = STRING_LENGTH_ERROR)]
            [RegularExpression("[A-Za-z]+", ErrorMessage = USERNAME_FORMAT_ERROR)]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 3, ErrorMessage = STRING_LENGTH_ERROR)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = CONFIRM_PASSWORD_ERROR)]
            public string ConfirmPassword { get; set; }

            [Required]
            [RegularExpression(@"\+\d{10,12}", ErrorMessage = PHONE_FORMAT_ERROR)]
            public string Phone { get; set; }
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
            if (this.ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = this.Input.Username, 
                    Email = this.Input.Email,
                    PhoneNumber = this.Input.Phone
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

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
