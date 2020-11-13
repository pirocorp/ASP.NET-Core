namespace Areas.Web.Areas.Products.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ProductsBaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
