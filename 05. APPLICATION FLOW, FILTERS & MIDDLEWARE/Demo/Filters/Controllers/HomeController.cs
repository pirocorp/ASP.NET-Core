namespace Filters.Controllers
{
    using System.Diagnostics;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form() => this.View();

        [HttpPost]
        [ValidateModel]
        public IActionResult Form(RandomModel model)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        [MyHttpsOnly]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
