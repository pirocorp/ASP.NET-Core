namespace ForumSystem.Web.Controllers
{
    using System;

    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class CategoriesController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly ICategoryService categoryService;
        private readonly ILogger<CategoriesController> logger;
        private readonly IPostService postService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CategoriesController(
            ICategoryService categoryService,
            ILogger<CategoriesController> logger,
            IPostService postService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.categoryService = categoryService;
            this.logger = logger;
            this.postService = postService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult ByName(string name, int page = 1)
        {
            // Mockable http context
            // ReSharper disable once UnusedVariable
            var context = this.httpContextAccessor.HttpContext;

            this.logger.LogInformation(page.ToString());

            var model = this.categoryService
                .GetByName<CategoryViewModel>(name);

            if (page < 1)
            {
                page = 1;
            }

            var skip = (page - 1) * ItemsPerPage;
            var categoryId = model.Id;

            model.ForumPosts = this.postService
                .GetByCategoryId<PostInCategoryViewModel>(categoryId, ItemsPerPage, skip);

            var count = this.postService.GetCountByCategoryId(categoryId);
            var totalPages = count / (double)ItemsPerPage;
            model.PagesCount = (int)Math.Ceiling(totalPages);

            model.CurrentPage = page;

            return this.View(model);
        }
    }
}
