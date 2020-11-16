namespace LearningSystem.Services.Blog
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Admin.Blog;

    public interface IBlogArticlesService
    {
        Task<int> CreateAsync(CreateBlogArticleServiceModel model);

        Task<IEnumerable<TOut>> AllAsync<TOut>(int page = 1, bool newestFirst = true);

        Task<TOut> GetByIdAsync<TOut>(int id);

        Task<int> CountAsync();
    }
}