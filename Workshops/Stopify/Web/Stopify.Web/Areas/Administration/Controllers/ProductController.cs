﻿namespace Stopify.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.InputModels;
    using Models.ViewModels.ProductType;
    using Services.Data;
    using Services.Mapping;
    using Services.Models;

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
            return await Task.FromResult(this.Ok());
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

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
