namespace ForumSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class PostService : DeletableEntityService<Post>, IPostService
    {
        public PostService(IDeletableEntityRepository<Post> postRepository)
            : base(postRepository)
        {
        }

        public async Task<int> CreateAsync(
            string title,
            string content,
            int categoryId,
            string userId)
        {
            var post = new Post()
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                UserId = userId,
            };

            await this.EntityRepository.AddAsync(post);
            await this.EntityRepository.SaveChangesAsync();

            return post.Id;
        }

        public IEnumerable<TModel> GetByCategoryId<TModel>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this
                .EntityRepository
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Where(p => p.CategoryId == categoryId)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query
                .To<TModel>()
                .ToList();
        }

        public int GetCountByCategoryId(int categoryId)
            => this.EntityRepository
                .All()
                .Count(p => p.CategoryId == categoryId);

        public TResult GetById<TResult>(int id)
            => this.EntityRepository
                .All()
                .Where(x => x.Id == id)
                .To<TResult>()
                .FirstOrDefault();
    }
}
