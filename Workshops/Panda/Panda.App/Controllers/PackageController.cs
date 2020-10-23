namespace Panda.App.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Panda.App.Areas.Identity.Pages.Account;
    using Panda.App.Models.InputModels.Package;
    using Panda.Mapping;
    using Panda.Models;
    using Panda.Services;
    using Panda.Services.Models;

    public class PackageController : Controller
    {
        private readonly ILogger<LoginModel> logger;
        private readonly IPackageService packageService;
        private readonly UserManager<PandaUser> userManager;

        public PackageController(
            ILogger<LoginModel> logger,
            IPackageService packageService,
            UserManager<PandaUser> userManager)
        {
            this.logger = logger;
            this.packageService = packageService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var recipients = this
                .userManager
                .Users
                .To<PandaUserDropDownViewModel>()
                .ToList();

            var model = new PackageCreateInputModel()
            {
                Recipients = recipients,
            };

            return this.View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(PackageCreateInputModel model)
        {
            if (!this.userManager.Users.Any(u => u.Id == model.RecipientId))
            {
                return this.View();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var package = model.To<PackageCreateServiceModel>();

            var packageId = await this.packageService.CreateAsync(package);

            this.logger.LogInformation("Package with id: {packageId} is add to the system.", packageId);

            var homeController = nameof(HomeController)
                .Replace("Controller", string.Empty);

            return this.RedirectToAction(nameof(HomeController.Index), homeController);
        }
    }
}
