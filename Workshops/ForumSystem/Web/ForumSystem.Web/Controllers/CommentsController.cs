namespace ForumSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly IPostService postService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            IPostService postService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.postService = postService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentCreateInputModel input)
        {
            var postId = input.PostId;

            if (!this.postService.Exists(postId))
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var userId = this.userManager.GetUserId(this.User);
            var parentId = input.ParentId == 0 ? (int?)null : input.ParentId;

            if (parentId.HasValue
                && !this.commentsService.IsInPostId(parentId.Value, input.PostId))
            {
                return this.BadRequest();
            }

            await this.commentsService.Create(postId, userId, input.Content, parentId);

            var actionName = nameof(PostsController.ById);
            var controllerName = nameof(PostsController)
                .Replace("Controller", string.Empty);

            return this.RedirectToAction(actionName, controllerName, new { id = postId });
        }
    }
}
