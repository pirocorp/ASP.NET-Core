namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Articles;
    using Services.Blog;

    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly IBlogArticlesService blogArticlesService;

        public ArticlesController(IBlogArticlesService blogArticlesService)
        {
            this.blogArticlesService = blogArticlesService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var article = await this.blogArticlesService.GetByIdAsync<ArticleDetailsModel>(id);

            return this.View(article);
        }
    }
}
