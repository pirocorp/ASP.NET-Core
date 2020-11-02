namespace Stopify.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels.Receipt;
    using Services.Data;

    [Authorize]
    public class ReceiptController : Controller
    {
        private readonly IOrderService orderService;
        private readonly UserManager<StopifyUser> userManager;

        public ReceiptController(
            IOrderService orderService,
            UserManager<StopifyUser> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (! await this.orderService.Exists(id))
            {
                return this.BadRequest();
            }

            var model = await this.orderService.GetOrderByIdAsync<ReceiptDetailsViewModel>(id);
            return await Task.FromResult(this.View(model));
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var orders = (await this.orderService.GetAllUserOrders<ReceiptOrderListingModel>(userId)).ToList();

            var model =  new ReceiptIndexViewModel()
            {
                Orders = await this.orderService.GetAllUserOrders<ReceiptOrderListingModel>(userId),
            };
            
            return this.View(model);
        }
    }
}
