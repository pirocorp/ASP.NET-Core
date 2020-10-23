namespace Panda.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Panda.App.Areas.Identity.Pages.Account;
    using Panda.App.Models.InputModels.Package;
    using Panda.App.Models.ViewModels.Package;
    using Panda.Infrastructure;
    using Panda.Mapping;
    using Panda.Models;
    using Panda.Services;
    using Panda.Services.Models;

    [Authorize(Roles = GlobalConstants.AdminRole)]
    public class PackageController : Controller
    {
        private readonly ILogger<LoginModel> logger;
        private readonly IPackageService packageService;
        private readonly IStatusesService packageStatusesService;
        private readonly UserManager<PandaUser> userManager;

        public PackageController(
            ILogger<LoginModel> logger,
            IPackageService packageService,
            IStatusesService packageStatusesService,
            UserManager<PandaUser> userManager)
        {
            this.logger = logger;
            this.packageService = packageService;
            this.packageStatusesService = packageStatusesService;
            this.userManager = userManager;
        }

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

            return this.RedirectToAction(nameof(this.Pending));
        }

        public async Task<IActionResult> Pending()
        {
            var pendingPackages =
                await this.GetPackagesByStatusAsync(ShipmentStatus.Pending.ToString());

            return this.View(pendingPackages.ToList());
        }

        public async Task<IActionResult> Shipped()
        {
            var shippedPackages =
                await this.GetPackagesByStatusAsync(ShipmentStatus.Shipped.ToString());

            return this.View(shippedPackages.ToList());
        }

        public async Task<IActionResult> Delivered()
        {
            var shippedPackages =
                await this.GetPackagesByStatusAsync(ShipmentStatus.Delivered.ToString());

            return this.View(shippedPackages.ToList());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var package = await this.packageService
                .GetByIdAsync<PackageDetailsViewModel>(id);

            var currentUserId = this.userManager.GetUserId(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdminRole)
                && !package.RecipientId.Equals(currentUserId))
            {
                return this.BadRequest();
            }

            return this.View(package);
        }

        public async Task<IActionResult> ChangeStatus(string id, string status)
        {
            var newStatus = Enum.Parse<ShipmentStatus>(status);
            newStatus += 1;

            var newStatusId = await this
                .packageStatusesService
                .GetPackageStatusIdByNameAsync(newStatus.ToString());

            var success = await this.packageService
                .ChangeStatusAsync(id, newStatusId);

            if (success is false)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction(newStatus.ToString());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Acquire(string id)
        {
            var package = await this.packageService
                .GetByIdAsync<PackageDetailsViewModel>(id);

            var currentUserId = this.userManager.GetUserId(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdminRole)
                && !package.RecipientId.Equals(currentUserId))
            {
                return this.BadRequest();
            }

            var statusId = await this
                .packageStatusesService
                .GetPackageStatusIdByNameAsync(ShipmentStatus.Acquired.ToString());

            await this.packageService.ChangeStatusAsync(id, statusId);

            // TODO: Create receipt
            var homeController = nameof(HomeController)
                .Replace("Controller", string.Empty);

            return this.RedirectToAction(nameof(HomeController.Index), homeController);
        }

        private async Task<IEnumerable<PackageViewModel>> GetPackagesByStatusAsync(string shipmentStatus)
        {
            var statusId = await this.packageStatusesService
                .GetPackageStatusIdByNameAsync(shipmentStatus);

            return await this.packageService
                .GetPackagesByStatusCodeAsync<PackageViewModel>(statusId);
        }
    }
}
