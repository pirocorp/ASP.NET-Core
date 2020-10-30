namespace Stopify.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
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

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Cart(string id)
        {
            return await Task
                .FromResult(this.RedirectToAction(nameof(HomeController.Index), "Home"));
        }
    }
}
