namespace ForumSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Categories;
    using ForumSystem.Web.ViewModels.Posts;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostService postService,
            ICategoryService categoryService,
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.votesService = votesService;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            var model = this
                .postService
                .GetById<PostViewModel>(id);

            if (model is null)
            {
                return this.NotFound();
            }

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(this.User);

                var userVote = this.votesService.GetUserVoteForPost<PostUserVoteViewModel>(userId, id);

                model.UserVote = userVote;
            }

            return this.View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            var categories = this
                .categoryService
                .GetAll<CategoryDropDownViewModel, string>(null, x => x.Name);

            var model = new PostCreateInputModel()
            {
                Categories = categories,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var postId = await this.postService.CreateAsync(
                model.Title,
                model.Content,
                model.CategoryId,
                user.Id);

            return this.RedirectToAction(nameof(this.ById), new { id = postId });
        }
    }
}
