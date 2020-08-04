namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public interface IVotesService
    {
        Task VoteAsync(int postId, string userId, VoteType voteType);

        int GetVotes(int postId);
    }
}
