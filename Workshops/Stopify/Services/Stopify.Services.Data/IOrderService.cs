namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Stopify.Data.Models;

    public interface IOrderService
    {
        Task<string> CreateAsync(string userId, string productId);

        Task<TOut> GetOrderByIdAsync<TOut>(string id);

        Task<Order> GetOrderByIdAsync(string id);

        Task<string> GetCurrentUserOrderIdAsync(string userId);

        Task<bool> AddProductToOrderAsync(string orderId, string productId);
    }
}