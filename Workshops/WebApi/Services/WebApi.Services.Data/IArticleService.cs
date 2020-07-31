namespace WebApi.Services.Data
{
    using System.Threading.Tasks;

    using WebApi.Common.Mapping;
    using WebApi.Data.Models;
    using WebApi.Services.Models;

    public interface IArticleService
    {
        Task<TResult> CreateAsync<TResult>(CreateArticleServiceModel model)
            where TResult : IMapFrom<Article>;
    }
}
