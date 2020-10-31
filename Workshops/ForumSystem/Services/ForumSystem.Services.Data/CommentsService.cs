namespace ForumSystem.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;

    public class CommentsService : DeletableEntityService<Comment, int>, ICommentsService
    {
        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
            : base(commentsRepository)
        {
        }

        public async Task Create(int postId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment()
            {
                Content = content,
                ParentId = parentId,
                PostId = postId,
                UserId = userId,
            };

            await this.EntityRepository.AddAsync(comment);
            await this.EntityRepository.SaveChangesAsync();
        }

        public bool IsInPostId(int commentId, int postId)
        {
            var commentPostId = this.EntityRepository
                .AllAsNoTracking()
                .Where(c => c.Id == commentId)
                .Select(c => c.PostId)
                .FirstOrDefault();

            return commentPostId == postId;
        }
    }
}
