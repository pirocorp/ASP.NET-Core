namespace Demo.Controllers
{
    using System;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using ViewModels;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IActionResult Index()
        {
            //Best way for passing data to views
            var model = new IndexViewModel()
            {
                Message = this._configuration["YouTube:ApiKey"],
                Year = DateTime.UtcNow.Year,
            };

            static string Action() => "Asen";

            //Second best
            this.ViewData["name"] = "Stoyan";

            //Worst way - it will override the name 
            //ViewBag exists  by legacy reasons
            this.ViewBag.Name = "Piro";

            this.ViewBag.Func = (Func<string>) Action;

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Test()
        {
            return this.Content(this._configuration["YouTube:ApiKey"]);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
