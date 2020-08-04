namespace ForumSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel model)
        {
            var voteType = model.IsUpVote
                ? VoteType.UpVote
                : VoteType.DownVote;

            var userId = this.userManager.GetUserId(this.User);

            await this.votesService.VoteAsync(model.PostId, userId, voteType);

            var votes = this.votesService.GetVotes(model.PostId);

            return new VoteResponseModel()
            {
                VotesCount = votes,
            };
        }
    }
}
