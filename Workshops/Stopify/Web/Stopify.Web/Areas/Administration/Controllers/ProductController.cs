namespace Stopify.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels;
    using Models.ViewModels.Product;
    using Models.ViewModels.ProductType;
    using Services.Data;
    using Services.Mapping;
    using Services.Models;
    using Web.Controllers;

    public class ProductController : AdminController
    {
        private readonly IProductService productService;
        private readonly IProductTypeService productTypeService;

        public ProductController(
            IProductService productService,
            IProductTypeService productTypeService)
        {
            this.productService = productService;
            this.productTypeService = productTypeService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.productService.AllNotSoldAsync<ProductAdminViewModel>();

            return this.View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateInputModel()
            {
                Categories = await this.productTypeService
                    .AllAsync<ProductTypeListingModel>(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.productTypeService
                    .AllAsync<ProductTypeListingModel>();

                return this.View(model);
            }

            var serviceModel = model.To<ProductCreateServiceModel>();
            await this.productService.CreateAsync(serviceModel);

            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = ""});
        }

        public async Task<IActionResult> Edit(string productId)
        {
            if (productId is null)
            {
                return this.BadRequest();
            }

            var model = await this.productService
                .GetByIdAsync<ProductEditInputModel>(productId);

            model.Categories =  await this.productTypeService
                .AllAsync<ProductTypeListingModel>();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditInputModel model)
        {
            var serviceModel = model.To<ProductEditServiceModel>();
            await this.productService.EditAsync(serviceModel);

            return this
                .RedirectToAction(
                    nameof(Web.Controllers.ProductController.Details),
                    "Product",
                    new
                    {
                        area = "",
                        id = model.Id
                    });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string productId)
        {
            await this.productService.DeleteAsync(productId);

            return this.RedirectToAction(
                nameof(HomeController.Index),
                "Home",
                new { area = "" });
        }
    }
}
