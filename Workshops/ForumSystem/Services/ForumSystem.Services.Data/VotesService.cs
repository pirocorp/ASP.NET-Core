namespace ForumSystem.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task VoteAsync(int postId, string userId, VoteType voteType)
        {
            var vote = this.votesRepository
                .All()
                .FirstOrDefault(v => v.UserId == userId && v.PostId == postId);

            if (vote != null)
            {
                vote.Type = vote.Type == voteType
                    ? VoteType.Neutral
                    : voteType;
            }
            else
            {
                vote = new Vote()
                {
                    PostId = postId,
                    UserId = userId,
                    Type = voteType,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }

        public int GetUpVotesCount(int postId)
            => this.GetPostVotes(postId)
                .Count(v => v.Type == VoteType.UpVote);

        public int GetDownVotesCount(int postId)
            => this.GetPostVotes(postId)
                .Count(v => v.Type == VoteType.DownVote);

        private IQueryable<Vote> GetPostVotes(int postId)
            => this.votesRepository
                .All()
                .Where(v => v.PostId == postId);
    }
}
