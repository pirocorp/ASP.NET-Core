namespace WebApi.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
            where TResult : IMapFrom<Article>
            => await this.articlesRepository
                .All()
                .ProjectTo<TResult>()
                .ToArrayAsync();

        public async Task<Article> GetArticleByIdAsync(int id)
            => await this.articlesRepository
                .GetByIdAsync(id);

        public async Task<TResult> GetArticleByIdAsync<TResult>(int id)
            where TResult : IMapFrom<Article>
            => await this.articlesRepository
                .All()
                .Where(a => a.Id == id)
                .To<TResult>()
                .FirstOrDefaultAsync();
    }
}
