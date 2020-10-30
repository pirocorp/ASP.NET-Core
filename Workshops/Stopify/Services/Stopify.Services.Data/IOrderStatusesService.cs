namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Stopify.Data.Models;

    public interface IOrderStatusesService
    {
        Task<OrderStatus> GetByNameAsync(string name);
    }
}