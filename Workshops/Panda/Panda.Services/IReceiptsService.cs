namespace Panda.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IReceiptsService
    {
        Task<string> CreateAsync(ReceiptCreateServiceModel model);

        Task<IEnumerable<TOutput>> GetAllAsync<TOutput>();

        Task<IEnumerable<TOutput>> GetAllByUserAsync<TOutput>(string userId);

        Task<TOutput> GetByIdAsync<TOutput>(string receiptId);
    }
}
