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
    using Services.Admin;
    using Services.Models.Admin.Blog;
    using static Common.GlobalConstants;

    [Area(BlogArea)]
    [Authorize(Roles = BlogAuthorRole)]
    public class ArticlesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminArticlesService adminArticlesService;
        private readonly IHtmlSanitizer htmlSanitizer;

        public ArticlesController(
            UserManager<User> userManager,
            IAdminArticlesService adminArticlesService, 
            IHtmlSanitizer htmlSanitizer)
        {
            this.userManager = userManager;
            this.adminArticlesService = adminArticlesService;
            this.htmlSanitizer = htmlSanitizer;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(this.View());
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

            await this.adminArticlesService.CreateAsync(serviceModel);
            this.TempData.AddSuccessMessage($"Article {model.Title} is created successfully.");

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
