namespace Stopify.Web.Controllers
{

    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Api;
    using Services.Data;
    using Services.Models;

    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly UserManager<StopifyUser> userManager;
        private readonly IVoteService voteService;

        public VoteController(
            UserManager<StopifyUser> userManager,
            IVoteService voteService)
        {
            this.userManager = userManager;
            this.voteService = voteService;
        }

        /// <summary>
        /// POST /api/votes
        /// 
        /// Request body: {
        ///     "productId": guid,
        ///     "score": 5
        /// };
        /// 
        /// Response body: {
        ///     "rating": between 1-5
        /// };
        /// 
        /// </summary>
        /// <param name="model">Post data model.</param>
        /// <returns>JSON object from VoteResponseModel.</returns>

        [Authorize]
        [EnableCors]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            var serviceModel = new VoteCreateServiceModel()
            {
                ProductId = model.ProductId,
                Score = model.Score,
                UserId = userId
            };

            await this.voteService.VoteAsync(serviceModel);

            var rating = await this.voteService.GetTotalScoreAsync(model.ProductId);

            var responseModel = new VoteResponseModel()
            {
                Rating = rating,
            };

            return responseModel;
        }
    }
}
