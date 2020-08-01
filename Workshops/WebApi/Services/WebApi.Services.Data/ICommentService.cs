namespace WebApi.Services.Data
{
    using System.Threading.Tasks;

    using WebApi.Common.Mapping;
    using WebApi.Data.Models;
    using WebApi.Services.Models;

    public interface ICommentService
    {
        Task<TResult> CreateAsync<TResult>(CreateCommentServiceModel model)
            where TResult : IMapFrom<Comment>;
    }
}
