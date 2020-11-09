namespace JokesApp.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using JokesApp.Web.Models;
    using Services.DataServices;
    using Services.Models.Home;

    public class HomeController : Controller
    {
        private readonly IJokesService jokesService;

        public HomeController(IJokesService jokesService)
        {
            this.jokesService = jokesService;
        }

        public async Task<IActionResult> Index()
        {

            var viewModel = new IndexViewModel()
            {
                Jokes = await this.jokesService.GetRandomJokesAsync(20),
            };
            
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
