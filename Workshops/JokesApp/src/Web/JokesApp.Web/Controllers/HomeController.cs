namespace JokesApp.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using JokesApp.Web.Models;
    using Data.Common;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Home;

    public class HomeController : Controller
    {
        private readonly IRepository<Joke> jokesRepository;

        public HomeController(IRepository<Joke> jokesRepository)
        {
            this.jokesRepository = jokesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var jokes = await this.jokesRepository
                .All()
                .OrderBy(x => Guid.NewGuid())
                .Select(j => new IndexJokeViewModel()
                {
                    Content = j.Content
                        .Replace("\r\n", "<br />")
                        .Replace("\n", "<br />"),
                    CategoryName = j.Category.Name
                })
                .Take(20)
                .ToListAsync();

            var viewModel = new IndexViewModel()
            {
                Jokes = jokes
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
