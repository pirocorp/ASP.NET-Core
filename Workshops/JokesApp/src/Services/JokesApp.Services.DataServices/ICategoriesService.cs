namespace JokesApp.Services.DataServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICategoriesService
    {
        Task<IEnumerable<TOut>> GetAllAsync<TOut>();

        Task<TOut> GetById<TOut>(int id);

        bool Exists(int categoryId);

        int? GetCategoryId(string name);
    }
}
