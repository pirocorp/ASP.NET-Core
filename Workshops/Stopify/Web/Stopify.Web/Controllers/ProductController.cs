namespace Stopify.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels.Product;
    using Services.Data;

    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly UserManager<StopifyUser> userManager;

        public ProductController(
            IProductService productService,
            IOrderService orderService,
            UserManager<StopifyUser> userManager)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details(string id)
        {
            var model = await this.productService.GetByIdAsync<ProductDetailsViewModel>(id);

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Order(string id)
        {
            if (await this.productService.ProductIsSoldAsync(id))
            {
                return this.RedirectToAction(nameof(this.Details), new {id});
            }
            
            var userId = this.userManager.GetUserId(this.User);

            var orderId = await this.orderService.GetCurrentUserOrderIdAsync(userId);

            if (orderId is null)
            {
                orderId = await this.orderService.CreateAsync(userId, id);
            }
            else
            {
                var success = await this.orderService.AddProductToOrderAsync(orderId, id);

                if (!success)
                {
                    return this.BadRequest();
                }
            }

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
