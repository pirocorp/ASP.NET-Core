namespace Stopify.Services.Data
{
    using System;
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
            var product = await this.productService
                .GetByIdAsync(productId);

            var orderStatus = await this.orderStatusesService
                .GetByStatusAsync(OrderStatuses.Active);

            var order = new Order()
            {
                UserId = userId,
                Status = orderStatus,
                IssuedOn = DateTime.UtcNow
            };

            order.Products.Add(product);

            await this.dbContext.AddAsync(order);
            await this.dbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task<TOut> GetOrderByIdAsync<TOut>(string id)
            => await this.dbContext.Orders
                .Where(o => o.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<Order> GetOrderByIdAsync(string id)
            => await this.dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id.Equals(id));

        public async Task<string> GetCurrentUserOrderIdAsync(string userId)
            => (await this.dbContext.Orders
                .Where(o
                    => o.UserId.Equals(userId)
                    && o.Status.Name.Equals(OrderStatuses.Active.ToString()))
                .FirstOrDefaultAsync())
                ?.Id;

        public async Task<bool> AddProductToOrderAsync(string orderId, string productId)
        {
            var product = await this.productService.GetByIdAsync(productId);

            if (product is null)
            {
                return false;
            }

            product.OrderId = orderId;

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeOrderStatusAsync(string orderId, OrderStatuses orderStatus)
        {
            var status = await this.orderStatusesService.GetByStatusAsync(orderStatus);
            var order = await this.GetOrderByIdAsync(orderId);

            order.Status = status;
            var result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Exists(string orderId)
            => await this.dbContext.Orders.AnyAsync(o => o.Id.Equals(orderId));
    }
}
