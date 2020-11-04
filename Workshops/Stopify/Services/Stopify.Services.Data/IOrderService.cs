namespace Stopify.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Common;

    public interface IOrderService
    {
        Task<string> CreateAsync(string userId, string productId);

        Task<TOut> GetOrderByIdAsync<TOut>(string id);

        Task<IEnumerable<TOut>> GetAllUserOrders<TOut>(string userId);

        Task<decimal> GetTotalCostForOrderAsync(string orderId);

        Task<string> GetCurrentUserOrderIdAsync(string userId);

        Task<bool> AddProductToOrderAsync(string orderId, string productId);

        Task<bool> ChangeOrderStatusAsync(string orderId, OrderStatuses orderStatus);

        Task<bool> Exists(string orderId);
    }
}