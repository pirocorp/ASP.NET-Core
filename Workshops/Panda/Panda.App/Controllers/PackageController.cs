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
        private readonly IStatusesService statusesService;
        private readonly UserManager<PandaUser> userManager;

        public PackageController(
            ILogger<LoginModel> logger,
            IPackageService packageService,
            IStatusesService statusesService,
            UserManager<PandaUser> userManager)
        {
            this.logger = logger;
            this.packageService = packageService;
            this.statusesService = statusesService;
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
            var pendingPackages = await this.packageService
                    .GetPackagesByStatusCodeAsync<PackageViewModel>(ShipmentStatus.Pending);

            return this.View(pendingPackages.ToList());
        }

        public async Task<IActionResult> Shipped()
        {
            var shippedPackages = await this.packageService
                    .GetPackagesByStatusCodeAsync<PackageViewModel>(ShipmentStatus.Shipped);

            return this.View(shippedPackages.ToList());
        }

        public async Task<IActionResult> Delivered()
        {
            var deliveredPackages = await this.packageService
                .GetPackagesByStatusCodeAsync<PackageViewModel>(ShipmentStatus.Delivered);

            return this.View(deliveredPackages.ToList());
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

            var success = await this.packageService
                .ChangeStatusAsync(id, newStatus);

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
                .GetByIdAsync<Package>(id);

            var currentUserId = this.userManager.GetUserId(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdminRole)
                && !package.RecipientId.Equals(currentUserId))
            {
                return this.BadRequest();
            }

            await this.packageService.ChangeStatusAsync(id, ShipmentStatus.Acquired);

            // TODO: Create receipt service and controller
            var receipt = new Receipt()
            {
                Fee = GlobalConstants.FeeRatio * (decimal)package.Weight,
                IssuedOn = DateTime.UtcNow,
                Package = package,
                RecipientId = currentUserId,
            };

            // TODO: Redirect to Receipts
            var homeController = nameof(HomeController)
                .Replace("Controller", string.Empty);

            return this.RedirectToAction(nameof(HomeController.Index), homeController);
        }
    }
}
