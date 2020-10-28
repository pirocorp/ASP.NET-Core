namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Models;

    public interface IProductService
    {
        Task<string> CreateAsync(ProductCreateServiceModel model);
    }
}
