namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public interface ICommentsService : IDeletableEntityService<Comment, int>
    {
        Task Create(int postId, string userId, string content, int? parentId = null);

        bool IsInPostId(int commentId, int postId);
    }
}
