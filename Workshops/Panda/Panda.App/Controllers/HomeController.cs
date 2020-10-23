namespace Panda.App.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Panda.App.Models;
    using Panda.App.Models.ViewModels.Home;
    using Panda.Infrastructure;
    using Panda.Services;

    public class HomeController : Controller
    {
        private readonly IPackageService packageService;

        public HomeController(IPackageService packageService)
        {
            this.packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var allPackages = await this.packageService
                .GetAllPackagesInTheSystemAsync<HomeIndexPackageViewModel>();

            var groupedPackages = allPackages
                .GroupBy(p => p.StatusName)
                .ToDictionary(x => x.Key, x => x.ToList());

            var model = new HomeIndexViewModel()
            {
                Pending = groupedPackages[ShipmentStatus.Pending.ToString()],
                Shipped = groupedPackages[ShipmentStatus.Shipped.ToString()],
                Delivered = groupedPackages[ShipmentStatus.Delivered.ToString()],
            };

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
