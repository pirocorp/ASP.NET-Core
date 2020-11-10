namespace JokesApp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Jokes;
    using Services.DataServices;
    using Services.MachineLearning;
    using Services.Models.Jokes;

    [Authorize]
    public class JokesController : BaseController
    {
        private readonly IJokesService jokesService;
        private readonly ICategoriesService categoriesService;
        private readonly IJokesCategorizer jokesCategorizer;

        public JokesController(
            IJokesService jokesService, 
            ICategoriesService categoriesService,
            IJokesCategorizer jokesCategorizer)
        {
            this.jokesService = jokesService;
            this.categoriesService = categoriesService;
            this.jokesCategorizer = jokesCategorizer;
        }

        public async Task<IActionResult> Create()
        {
            await this.LoadCategories();

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJokeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                await this.LoadCategories();

                return this.View(input);
            }

            var id = await this.jokesService.CreateAsync(input.CategoryId, input.Content);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var joke = await this.jokesService.GetJokeByIdAsync<JokeDetailsViewModel>(id);

            return this.View(joke);
        }

        [HttpPost]
        public SuggestCategoryResult SuggestCategory(string joke)
        {
            var category = this.jokesCategorizer.Categorize("MlModels/JokesCategoryModel.zip", joke);
            var categoryId = this.categoriesService.GetCategoryId(category);
            return new SuggestCategoryResult { CategoryId = categoryId ?? 0, CategoryName = category };
        }

        private async Task LoadCategories()
        {
            this.ViewData["Categories"] = (await this.categoriesService.GetAllAsync())
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                .ToList();
        }

        public class SuggestCategoryResult
        {
            public int CategoryId { get; set; }

            public string CategoryName { get; set; }
        }
    }
}
