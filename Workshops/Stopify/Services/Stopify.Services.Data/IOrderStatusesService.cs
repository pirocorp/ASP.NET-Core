namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Infrastructure.Common;
    using Stopify.Data.Models;

    public interface IOrderStatusesService
    {
        Task<OrderStatus> GetByStatusAsync(OrderStatuses orderStatus);
    }
}