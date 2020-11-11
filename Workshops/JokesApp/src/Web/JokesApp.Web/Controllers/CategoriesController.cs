namespace JokesApp.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Categories;
    using Services.DataServices;
    using Services.Models.Categories;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IJokesService jokesService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IJokesService jokesService)
        {
            this.categoriesService = categoriesService;
            this.jokesService = jokesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CategoryIndexViewModel()
            {
                Categories = await  this.categoriesService.GetAllAsync<CategoryListingModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Details(int id, int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            var model = new DetailsViewModel()
            {
                Category = await this.categoriesService.GetById<CategoriesDetailsViewModel>(id),
                Jokes = await this.jokesService.GetJokesFromCategory<CategoryDetailsJokeListingModel>(id, page),
                CurrentPage = page
            };

            return this.View(model);
        }
    }
}
