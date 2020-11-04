namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Models;

    public interface IVoteService
    {
        Task<string> VoteAsync(VoteCreateServiceModel model);

        Task<double> GetTotalScoreAsync(string productId);
    }
}
