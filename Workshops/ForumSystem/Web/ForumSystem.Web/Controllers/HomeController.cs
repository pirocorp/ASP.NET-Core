namespace ForumSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using ForumSystem.Data;
    using ForumSystem.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var categories = this.db
                .Categories
                .Select(c => new IndexCategoryViewModel()
                {
                    Name = c.Name,
                    Title = c.Title,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                })
                .ToList();

            viewModel.Categories = categories;
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
