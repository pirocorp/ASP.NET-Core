namespace LearningSystem.Services.Admin
{
    using System.Threading.Tasks;
    using Models.Admin.Blog;

    public interface IAdminArticlesService
    {
        Task<int> CreateAsync(CreateBlogArticleServiceModel model);
    }
}