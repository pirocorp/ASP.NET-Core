namespace LearningSystem.Services.Blog
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Admin.Blog;

    public interface IBlogArticlesService
    {
        Task<int> CreateAsync(CreateBlogArticleServiceModel model);

        Task<IEnumerable<TOut>> AllAsync<TOut>(int pageSize, int page = 1, bool newestFirst = true);

        Task<(IEnumerable<TOut> Collection, int Count)> SearchAsync<TOut>(string filter, int pageSize, int page = 1);

        Task<TOut> GetByIdAsync<TOut>(int id);

        Task<int> CountAsync();
    }
}