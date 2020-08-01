namespace WebApi.Web.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Data.Models;
    using WebApi.Services.Data;
    using WebApi.Services.Models;
    using WebApi.Web.ViewModels.Comments;

    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentService commentService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CommentCreateInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var serviceModel = Mapper.Map<CreateCommentServiceModel>(model);
            serviceModel.Author = user;

            var comment = await this.commentService
                .CreateAsync<CommentCreateViewModel>(serviceModel);

            return this.Created($"articles/{comment.Id}", comment);
        }
    }
}
