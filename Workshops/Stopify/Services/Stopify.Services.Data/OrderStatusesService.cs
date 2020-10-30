namespace Stopify.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Stopify.Data;
    using Stopify.Data.Models;

    public class OrderStatusesService : IOrderStatusesService
    {
        private readonly StopifyDbContext dbContext;

        public OrderStatusesService(StopifyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<OrderStatus> GetByNameAsync(string name)
            => await this.dbContext.OrderStatuses
                .Where(os => os.Name.Equals(name))
                .FirstOrDefaultAsync();
    }
}
