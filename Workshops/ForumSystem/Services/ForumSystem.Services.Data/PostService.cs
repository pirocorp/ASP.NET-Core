namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;

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
    }
}
