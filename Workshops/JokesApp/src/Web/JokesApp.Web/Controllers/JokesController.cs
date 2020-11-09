namespace JokesApp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Jokes;
    using Services.DataServices;

    [Authorize]
    public class JokesController : BaseController
    {
        private readonly IJokesService jokesService;
        private readonly ICategoriesService categoriesService;

        public JokesController(
            IJokesService jokesService, 
            ICategoriesService categoriesService)
        {
            this.jokesService = jokesService;
            this.categoriesService = categoriesService;
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
            var joke = await this.jokesService.GetJokeByIdAsync(id);

            return this.View(joke);
        }

        private async Task LoadCategories()
        {
            this.ViewData["Categories"] = (await this.categoriesService.GetAllAsync())
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                .ToList();
        }
    }
}
