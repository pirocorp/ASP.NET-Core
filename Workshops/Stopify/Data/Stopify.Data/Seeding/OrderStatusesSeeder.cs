namespace Stopify.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Common;
    using Models;

    public class OrderStatusesSeeder : ISeeder
    {
        public async Task SeedAsync(StopifyDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrderStatuses.Any())
            {
                return;
            }

            var orderStatuses = new List<OrderStatus>()
            {
                new OrderStatus(){ Name = OrderStatuses.Active.ToString() },
                new OrderStatus(){ Name = OrderStatuses.Completed.ToString() },
            };

            await dbContext.OrderStatuses.AddRangeAsync(orderStatuses);
            await dbContext.SaveChangesAsync();
        }
    }
}
