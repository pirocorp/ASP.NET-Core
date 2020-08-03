namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public interface IPostService : IDeletableEntityService<Post>
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);
    }
}
