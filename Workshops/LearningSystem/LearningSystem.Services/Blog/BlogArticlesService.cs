namespace LearningSystem.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Admin.Blog;

    public class BlogArticlesService : IBlogArticlesService
    {
        private readonly LearningSystemDbContext dbContext;

        public BlogArticlesService(LearningSystemDbContext dbContext)
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

        public async Task<IEnumerable<TOut>> AllAsync<TOut>(int pageSize, int page = 1, bool newestFirst = true)
        {
            var query = this.dbContext.Articles.Select(x => x);

            if (newestFirst)
            {
                query = query.OrderByDescending(a => a.PublishedDate);
            }
            else
            {
                query = query.OrderBy(a => a.PublishedDate);
            }

            var skip = (page - 1) * pageSize;
            
            return await query
                .Skip(skip)
                .Take(pageSize)
                .To<TOut>()
                .ToListAsync();
        }

        public async Task<(IEnumerable<TOut> Collection, int Count)> SearchAsync<TOut>(string filter, int pageSize, int page = 1)
        {
            var query = this.dbContext.Articles.Select(x => x);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToLower();

                query = query
                    .Where(a
                        => a.Author.Name.ToLower().Contains(filter)
                           || a.Content.ToLower().Contains(filter));
            }

            var skip = (page - 1) * pageSize;

            var count = (int)Math.Ceiling((query.Count() / (double)pageSize));

            var collection =  await query
                .OrderByDescending(a => a.PublishedDate)
                .To<TOut>()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (collection, count);
        }

        public async Task<TOut> GetByIdAsync<TOut>(int id)
            => await this.dbContext.Articles
                .Where(a => a.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<int> CountAsync()
            => await this.dbContext.Articles.CountAsync();
    }
}
