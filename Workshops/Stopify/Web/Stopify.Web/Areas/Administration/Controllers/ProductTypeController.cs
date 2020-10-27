namespace Stopify.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels;
    using Models.ViewModels.ProductType;
    using Services.Data;
    using Web.Controllers;

    public class ProductTypeController : AdminController
    {
        private readonly IProductTypeService productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }

        public async Task<IActionResult> All()
        {
            var categories = await this.productTypeService
                .AllAsync<ProductTypeListingModel>();

            return this.View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(this.View());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductTypeCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (await this.productTypeService.ExistsAsync(model.Name))
            {
                this.ModelState.AddModelError("Name", "Category already exists");

                return this.View(model);
            }

            await this.productTypeService.CreateAsync(model.Name);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
