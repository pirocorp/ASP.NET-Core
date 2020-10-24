namespace Panda.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
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
        private readonly IPackagesService packageService;
        private readonly IReceiptsService receiptsService;
        private readonly UserManager<PandaUser> userManager;

        public PackageController(
            ILogger<LoginModel> logger,
            IPackagesService packageService,
            IReceiptsService receiptsService,
            UserManager<PandaUser> userManager)
        {
            this.logger = logger;
            this.packageService = packageService;
            this.receiptsService = receiptsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            var recipients = await this
                .userManager
                .Users
                .To<PandaUserDropDownViewModel>()
                .ToListAsync();

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
                var recipients = await this
                    .userManager
                    .Users
                    .To<PandaUserDropDownViewModel>()
                    .ToListAsync();

                return this.View(new PackageCreateInputModel()
                {
                    Recipients = recipients,
                });
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
                .GetByIdAsync<PackageAcquireModel>(id);

            var currentUserId = this.userManager.GetUserId(this.User);

            if (!this.User.IsInRole(GlobalConstants.AdminRole)
                && !package.RecipientId.Equals(currentUserId))
            {
                return this.BadRequest();
            }

            await this.packageService.ChangeStatusAsync(id, ShipmentStatus.Acquired);

            var receipt = new ReceiptCreateServiceModel()
            {
                Weight = package.Weight,
                PackageId = package.Id,
                RecipientId = currentUserId,
            };

            await this.receiptsService.CreateAsync(receipt);

            var controller = nameof(ReceiptController)
                .Replace("Controller", string.Empty);

            return this.RedirectToAction(nameof(ReceiptController.Index), controller);
        }
    }
}
