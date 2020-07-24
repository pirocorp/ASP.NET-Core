namespace IdentityDemo.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Models.Identity;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class IdentityController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityController(
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult All()
        {
            var users = this.db.Users
                .OrderBy(u => u.Email)
                .Select(u => new ListUserViewModel()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                })
                .ToList();

            return this.View(users);
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return this.NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);

            var viewModel = new UserWithRolesViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Roles = roles,
            };

            return this.View(viewModel);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCreateUserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                this.TempData["SuccessMessage"] = $"User ({model.Email}) successfully created.";
                return this.RedirectToAction(nameof(this.All));
            }
            else
            {
                this.AddModelErrors(result);

                return this.View(model);
            }
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this
                .userManager.FindByIdAsync(id);

            if (user == null)
            {
                return this.NotFound();
            }

            var model = new AdminChangePasswordViewModel()
            {
                Email = user.Email,
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, AdminChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return this.NotFound();
            }

            var token = await this.userManager
                .GeneratePasswordResetTokenAsync(user);

            var result = await this.userManager
                .ResetPasswordAsync(user, token, model.Password);

            if (result.Succeeded)
            {
                this.TempData["SuccessMessage"] = $"Password successfully changed for user {user.Email}.";

                return this.RedirectToAction(nameof(this.All));
            }
            else
            {
                this.AddModelErrors(result);

                return this.View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager
                .FindByIdAsync(id);

            if (user == null)
            {
                return this.NotFound();
            }

            var model = new AdminDeleteUserViewModel()
            {
                Id = id,
                Email = user.Email,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Destroy(string id)
        {
            var user = await this.userManager
                .FindByIdAsync(id);

            if (user is null)
            {
                return this.NotFound();
            }

            var result = await this.userManager.DeleteAsync(user);

            this.TempData["SuccessMessage"] = $"{user.Email} successfully deleted.";
            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult AddToRole(string id)
        {
            var roleSelectListItems = this.roleManager
                .Roles
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                })
                .ToList();

            return this.View(roleSelectListItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToRole(string id, string role)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (user is null || !roleExists)
            {
                return this.NotFound();
            }

            await this.userManager
                .AddToRoleAsync(user, role);

            this.TempData["SuccessMessage"] = $"Successfully added user {user.Email} to role {role}.";
            return this.RedirectToAction(nameof(this.All));
        }
        
        private void AddModelErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
