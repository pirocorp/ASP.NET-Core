namespace Stopify.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Common;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Stopify.Data;
    using Stopify.Data.Models;

    public class OrderService : IOrderService
    {
        private readonly StopifyDbContext dbContext;
        private readonly IProductService productService;
        private readonly IOrderStatusesService orderStatusesService;

        public OrderService(
            StopifyDbContext dbContext,
            IProductService productService,
            IOrderStatusesService orderStatusesService)
        {
            this.dbContext = dbContext;
            this.productService = productService;
            this.orderStatusesService = orderStatusesService;
        }

        public async Task<string> CreateAsync(string userId, string productId)
        {
            var orderStatusId = await this.orderStatusesService
                .GetIdByStatusAsync(OrderStatuses.Active);

            var order = new Order()
            {
                UserId = userId,
                StatusId = orderStatusId,
                IssuedOn = DateTime.UtcNow
            };

            await this.dbContext.AddAsync(order);
            await this.dbContext.SaveChangesAsync();

            await this.AddProductToOrderAsync(order.Id, productId);

            return order.Id;
        }

        public async Task<TOut> GetOrderByIdAsync<TOut>(string id)
            => await this.dbContext.Orders
                .Where(o => o.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TOut>> GetAllUserOrders<TOut>(string userId)
            => await this.dbContext.Orders
                .Where(o => o.UserId.Equals(userId))
                .To<TOut>()
                .ToListAsync();

        public async Task<decimal> GetTotalCostForOrderAsync(string orderId)
            => (await this.dbContext.Orders
                    .Where(o => o.Id.Equals(orderId))
                    .Select(o => o.Products)
                    .FirstOrDefaultAsync())
                .Sum(p => p.Price);

        public async Task<string> GetCurrentUserOrderIdAsync(string userId)
            => (await this.dbContext.Orders
                .Where(o
                    => o.UserId.Equals(userId)
                    && o.Status.Name.Equals(OrderStatuses.Active.ToString()))
                .FirstOrDefaultAsync())
                ?.Id;

        public async Task<bool> AddProductToOrderAsync(string orderId, string productId)
        {
            if (!await this.productService.ExistsAsync(productId))
            {
                return false;
            }

            await this.productService.AddProductInOrder(productId, orderId);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeOrderStatusAsync(string orderId, OrderStatuses orderStatus)
        {
            var statusId = await this.orderStatusesService.GetIdByStatusAsync(orderStatus);
            var order = await this.GetOrderByIdAsync(orderId);

            order.StatusId = statusId;
            var result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Exists(string orderId)
            => await this.dbContext.Orders.AnyAsync(o => o.Id.Equals(orderId));

        private async Task<Order> GetOrderByIdAsync(string id)
            => await this.dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id.Equals(id));
    }
}
