namespace ForumSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public interface IPostService : IDeletableEntityService<Post, int>
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        TResult GetById<TResult>(int id);

        IEnumerable<TModel> GetByCategoryId<TModel>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
