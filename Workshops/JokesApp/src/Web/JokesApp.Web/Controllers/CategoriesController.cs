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

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CategoryIndexViewModel()
            {
                Categories = await  this.categoriesService.GetAllAsync<CategoryListingModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await this.categoriesService.GetById<CategoriesDetailsViewModel>(id);

            return this.View(category);
        }
    }
}
