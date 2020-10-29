namespace Stopify.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels.Product;
    using Services.Data;

    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var model = await this.productService.GetById<ProductDetailsViewModel>(id);

            return this.View(model);
        }
    }
}
