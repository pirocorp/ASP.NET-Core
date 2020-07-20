namespace ForumSystem.Web.Controllers
{
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult ByName(string name)
        {
            var model = this.categoryService
                .GetByName<CategoryViewModel>(name);

            return this.View(model);
        }
    }
}
