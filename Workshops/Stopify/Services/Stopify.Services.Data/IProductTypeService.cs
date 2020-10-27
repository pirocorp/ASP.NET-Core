namespace Stopify.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductTypeService
    {
        Task<bool> ExistsAsync(string name);

        Task<int> CreateAsync(string name);

        Task<IEnumerable<TOut>> AllAsync<TOut>();
    }
}