namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;
    using Models.Admin.Users;
    using Services.Admin;
    using Services.Mapping;

    public class UsersController : AdminController
    {
        private const string AddToRoleState = "AddToRoleState";
        private const string AddToRoleError = "Role or User not found!";

        private readonly IAdminUserService adminUserService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(
            IAdminUserService adminUserService, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.adminUserService = adminUserService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AdminUsersIndexViewModel()
            {
                Users = await this.adminUserService.AllAsync<AdminUserListingServiceModel>(),
                Roles = await this.roleManager.Roles.To<AdminRoleListingServiceModel>().ToListAsync(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddToRoleInputModel model)
        {
            var targetUser = await this.userManager.FindByIdAsync(model.UserId);
            var targetRole = await this.roleManager.FindByIdAsync(model.RoleId);

            if (!this.ModelState.IsValid 
                || targetUser is null
                || targetRole is null)
            {
                this.TempData.AddErrorMessage("User or role not found.");
                return this.RedirectToAction(nameof(this.Index));
            }

            await this.userManager.AddToRoleAsync(targetUser, targetRole.Name);
            this.TempData.AddSuccessMessage($"User {targetUser.UserName} added to role {targetRole.Name}");
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
