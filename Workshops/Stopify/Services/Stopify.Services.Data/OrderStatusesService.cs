namespace Stopify.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Common;
    using Microsoft.EntityFrameworkCore;
    using Stopify.Data;

    public class OrderStatusesService : IOrderStatusesService
    {
        private readonly StopifyDbContext dbContext;

        public OrderStatusesService(StopifyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GetIdByStatusAsync(OrderStatuses status)
            => (await this.dbContext.OrderStatuses
                .Where(os => os.Name.Equals(status.ToString()))
                .FirstOrDefaultAsync()).Id;
    }
}
