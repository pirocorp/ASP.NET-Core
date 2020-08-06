namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public interface IVotesService
    {
        Task VoteAsync(int postId, string userId, VoteType voteType);

        int GetUpVotesCount(int postId);

        int GetDownVotesCount(int postId);

        TResult GetUserVoteForPost<TResult>(string userId, int postId)
            where TResult : IMapFrom<Vote>;
    }
}
