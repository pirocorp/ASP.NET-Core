namespace Stopify.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Stopify.Data;
    using Stopify.Data.Models;

    public class VoteService : IVoteService
    {
        private readonly StopifyDbContext dbContext;

        public VoteService(StopifyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> VoteAsync(VoteCreateServiceModel model)
        {
            var rating = await this.dbContext
                .Ratings.FirstOrDefaultAsync(r => 
                    r.ProductId.Equals(model.ProductId) 
                    && r.UserId.Equals(model.UserId));

            if (rating is null)
            {
                rating = new Rating()
                {
                    ProductId = model.ProductId,
                    UserId = model.UserId,
                    Score = model.Score
                };

                await this.dbContext.Ratings.AddAsync(rating);
            }
            else
            {
                rating.Score = model.Score;
                this.dbContext.Ratings.Update(rating);
            }
            
            await this.dbContext.SaveChangesAsync();
            return rating.Id;
        }

        public async Task<double> GetTotalScoreAsync(string productId)
            => await this.dbContext.Ratings
                .Where(r => r.ProductId.Equals(productId))
                .AverageAsync(r => r.Score);
    }
}