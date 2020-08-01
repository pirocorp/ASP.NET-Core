namespace WebApi.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WebApi.Common.Mapping;
    using WebApi.Data.Models;
    using WebApi.Services.Models;

    public interface IArticleService
    {
        Task<TResult> CreateAsync<TResult>(CreateArticleServiceModel model)
            where TResult : IMapFrom<Article>;

        Task<IEnumerable<TResult>> GetAllAsync<TResult>()
            where TResult : IMapFrom<Article>;

        Task<Article> GetArticleByIdAsync(int id);

        Task<TResult> GetArticleByIdAsync<TResult>(int id)
            where TResult : IMapFrom<Article>;
    }
}
