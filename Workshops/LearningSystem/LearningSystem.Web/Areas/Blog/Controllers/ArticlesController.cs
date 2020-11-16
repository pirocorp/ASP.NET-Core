namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data.Models;
    using Ganss.XSS;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Articles;
    using Services.Blog;
    using Services.Models.Admin.Blog;
    using static Common.GlobalConstants;

    [Area(BlogArea)]
    [Authorize(Roles = BlogAuthorRole)]
    public class ArticlesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IBlogArticlesService blogArticlesService;
        private readonly IHtmlSanitizer htmlSanitizer;

        public ArticlesController(
            UserManager<User> userManager,
            IBlogArticlesService blogArticlesService, 
            IHtmlSanitizer htmlSanitizer)
        {
            this.userManager = userManager;
            this.blogArticlesService = blogArticlesService;
            this.htmlSanitizer = htmlSanitizer;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new BlogIndexViewModel()
            {
                Articles = await this.blogArticlesService.AllAsync<IndexArticleListingModel>(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(this.View());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PublishArticleFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            
            var sanitizedContent = this.htmlSanitizer.Sanitize(model.Content);

            var serviceModel = new CreateBlogArticleServiceModel()
            {
                Title = model.Title,
                Content = sanitizedContent,
                AuthorId = this.userManager.GetUserId(this.User),
                PublishedDate = DateTime.UtcNow
            };

            await this.blogArticlesService.CreateAsync(serviceModel);
            this.TempData.AddSuccessMessage($"Article {model.Title} is created successfully.");

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
