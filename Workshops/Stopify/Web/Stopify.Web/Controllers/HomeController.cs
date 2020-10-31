namespace Stopify.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using Models.ViewModels.Home;
    using Models.ViewModels.ProductType;
    using Services.Data;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService productService;
        private readonly IProductTypeService productTypeService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService, 
            IProductTypeService productTypeService)
        {
            this.logger = logger;
            this.productService = productService;
            this.productTypeService = productTypeService;
        }

        public async Task<IActionResult> Index(IndexViewModel model)
        {
            model.Products = await this.productService.AllNotSoldAsync<ProductIndexViewModel>(model.TypeId, model.IsAscending);

            var categories = (await this.productTypeService.AllAsync<ProductTypeListingModel>()).ToList();
            categories.Add(new ProductTypeListingModel()
            {
                Id = 0,
                Name = "All",
            });

            model.Categories = categories;

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
