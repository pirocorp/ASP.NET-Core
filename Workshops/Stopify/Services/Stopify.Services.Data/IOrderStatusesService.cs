namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Infrastructure.Common;

    public interface IOrderStatusesService
    {
        Task<int> GetIdByStatusAsync(OrderStatuses orderStatus);
    }
}