namespace Stopify.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Infrastructure.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels;
    using Models.ViewModels.Cart;
    using Services.Data;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<StopifyUser> userManager;
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        public OrderController(
            UserManager<StopifyUser> userManager,
            IOrderService orderService, 
            IProductService productService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
            this.productService = productService;
        }

        public async Task<IActionResult> Cart()
        {
            var userId = this.userManager.GetUserId(this.User);
            var orderId = await this.orderService.GetCurrentUserOrderIdAsync(userId);

            var model = await this.orderService.GetOrderByIdAsync<CartViewModel>(orderId);

            if (model is null)
            {
                model = new CartViewModel();
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string id)
        {
            if (id is null)
            {
                return this.RedirectToAction(nameof(this.Cart));
            }

            if (!await this.orderService.Exists(id))
            {
                return this.BadRequest();
            }

            // TODO: Go to payment form
            // TODO: Generate receipt

            await this.orderService.ChangeOrderStatusAsync(id, OrderStatuses.Completed);

            return await Task
                .FromResult(this.RedirectToAction(nameof(this.Cart)));
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromBody]CartRemoveProductInputModel model)
        {
            if (model?.ProductId is null
                || !await this.productService.ExistsAsync(model.ProductId))
            {
                return this.BadRequest();
            }

            await this.productService.RemoveProductFromOrder(model.ProductId);

            return this.Ok();
        }
    }
}
