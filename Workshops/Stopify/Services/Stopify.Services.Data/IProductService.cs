namespace Stopify.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IProductService
    {
        Task<string> CreateAsync(ProductCreateServiceModel model);

        Task<IEnumerable<TOut>> AllAsync<TOut>(int typeId = 0, bool isAscending = true);

        Task<TOut> GetById<TOut>(string id);
    }
}
