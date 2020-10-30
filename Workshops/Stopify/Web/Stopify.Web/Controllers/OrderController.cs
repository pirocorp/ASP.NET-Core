namespace Stopify.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Infrastructure.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels.Cart;
    using Services.Data;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<StopifyUser> userManager;
        private readonly IOrderService orderService;

        public OrderController(
            UserManager<StopifyUser> userManager,
            IOrderService orderService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
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
    }
}
