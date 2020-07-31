namespace WebApi.Services.Data
{
    using System.Threading.Tasks;

    using AutoMapper;

    using WebApi.Common.Mapping;
    using WebApi.Data.Common.Repositories;
    using WebApi.Data.Models;
    using WebApi.Services.Models;

    public class ArticleService : IArticleService
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;

        public ArticleService(IDeletableEntityRepository<Article> articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public async Task<TResult> CreateAsync<TResult>(CreateArticleServiceModel model)
            where TResult : IMapFrom<Article>
        {
            var article = new Article()
            {
                Title = model.Title,
                Content = model.Content,
                Author = model.Author,
            };

            this.articlesRepository.Add(article);
            await this.articlesRepository.SaveChangesAsync();

            return Mapper.Map<TResult>(article);
        }
    }
}
