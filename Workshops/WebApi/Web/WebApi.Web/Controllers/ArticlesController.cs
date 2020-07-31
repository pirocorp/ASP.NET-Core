namespace WebApi.Web.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Data.Models;
    using WebApi.Services.Data;
    using WebApi.Services.Models;
    using WebApi.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private readonly IArticleService articleService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesController(
            IArticleService articleService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleService = articleService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ArticleCreateInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var serviceModel = Mapper.Map<CreateArticleServiceModel>(model);
            serviceModel.Author = user;

            var article = await this.articleService.CreateAsync<ArticleCreateViewModel>(serviceModel);
            return this.Created($"articles/{article.Id}", article);
        }
    }
}
