namespace ForumSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVotesService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        #pragma warning disable SA1629 // Documentation text should end with a period
        #pragma warning disable SA1028 // Code should not contain trailing whitespace
        /// <summary>
        /// POST /api/votes
        /// 
        /// Request body: {
        ///     "postId": 1,
        ///     "isUpVote": true
        /// };
        /// 
        /// Response body: {
        ///     "votesCount": 16
        /// };
        /// 
        /// </summary>
        /// <param name="model">Post data model.</param>
        /// <returns>JSON object from VoteResponseModel.</returns>
        #pragma warning restore SA1028 // Code should not contain trailing whitespace
        #pragma warning restore SA1629 // Documentation text should end with a period

        [Authorize]
        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel model)
        {
            var voteType = model.IsUpVote
                ? VoteType.UpVote
                : VoteType.DownVote;

            var userId = this.userManager.GetUserId(this.User);

            await this.votesService.VoteAsync(model.PostId, userId, voteType);

            var postId = model.PostId;

            var upVotes = this.votesService.GetUpVotesCount(postId);
            var downVotes = this.votesService.GetDownVotesCount(postId);

            var response = new VoteResponseModel()
            {
                UpVotes = upVotes,
                DownVotes = downVotes,
            };

            return response;
        }
    }
}
