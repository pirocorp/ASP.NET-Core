namespace Panda.App.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Panda.App.Models;
    using Panda.App.Models.ViewModels.Home;
    using Panda.Infrastructure;
    using Panda.Models;
    using Panda.Services;

    public class HomeController : Controller
    {
        private readonly IPackageService packageService;
        private readonly UserManager<PandaUser> userManager;

        public HomeController(
            IPackageService packageService,
            UserManager<PandaUser> userManager)
        {
            this.packageService = packageService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel();
            IEnumerable<HomeIndexPackageViewModel> packages;

            if (this.User.IsInRole(GlobalConstants.AdminRole))
            {
                packages = await this.packageService
                    .GetAllPackagesInTheSystemAsync<HomeIndexPackageViewModel>();
            }
            else
            {
                var userId = this.userManager.GetUserId(this.User);

                packages = await this.packageService
                    .GetAllUserPackagesAsync<HomeIndexPackageViewModel>(userId);
            }

            var groupedPackages = packages
                .GroupBy(p => p.StatusName)
                .ToDictionary(x => x.Key, x => x.ToList());

            model.Pending = groupedPackages.ContainsKey(ShipmentStatus.Pending.ToString())
                ? groupedPackages[ShipmentStatus.Pending.ToString()]
                : new List<HomeIndexPackageViewModel>();

            model.Shipped = groupedPackages.ContainsKey(ShipmentStatus.Shipped.ToString())
                ? groupedPackages[ShipmentStatus.Shipped.ToString()]
                : new List<HomeIndexPackageViewModel>();

            model.Delivered = groupedPackages.ContainsKey(ShipmentStatus.Delivered.ToString())
                ? groupedPackages[ShipmentStatus.Delivered.ToString()]
                : new List<HomeIndexPackageViewModel>();

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            });
        }
    }
}
