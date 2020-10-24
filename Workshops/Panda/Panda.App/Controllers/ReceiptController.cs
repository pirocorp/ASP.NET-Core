namespace Panda.App.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Panda.App.Models.ViewModels.Receipt;
    using Panda.Infrastructure;
    using Panda.Models;
    using Panda.Services;

    [Authorize]
    public class ReceiptController : Controller
    {
        private readonly IReceiptsService receiptsService;
        private readonly UserManager<PandaUser> userManager;

        public ReceiptController(
            IReceiptsService receiptsService,
            UserManager<PandaUser> userManager)
        {
            this.receiptsService = receiptsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ReceiptIndexViewModel> model;

            if (this.User.IsInRole(GlobalConstants.AdminRole))
            {
                model = await this.receiptsService.GetAllAsync<ReceiptIndexViewModel>();
            }
            else
            {
                var userId = this.userManager.GetUserId(this.User);

                model = await this.receiptsService
                    .GetAllByUserAsync<ReceiptIndexViewModel>(userId);
            }

            return this.View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var model = await this.receiptsService
                .GetByIdAsync<ReceiptDetailsViewModel>(id);

            if (!this.User.IsInRole(GlobalConstants.AdminRole)
                && !model.RecipientId.Equals(userId))
            {
                return this.BadRequest();
            }

            return this.View(model);
        }
    }
}
