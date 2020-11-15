namespace LearningSystem.Services.Admin
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Models.Admin.Blog;

    public class AdminArticlesService : IAdminArticlesService
    {
        private readonly LearningSystemDbContext dbContext;

        public AdminArticlesService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateAsync(CreateBlogArticleServiceModel model)
        {
            var article = new Article()
            {
                Title = model.Title,
                Content = model.Content,
                PublishedDate = model.PublishedDate,
                AuthorId = model.AuthorId
            };

            await this.dbContext.AddAsync(article);
            await this.dbContext.SaveChangesAsync();

            return article.Id;
        }
    }
}
