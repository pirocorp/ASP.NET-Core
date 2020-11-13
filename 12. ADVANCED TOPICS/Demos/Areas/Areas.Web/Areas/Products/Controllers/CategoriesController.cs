namespace Areas.Web.Areas.Products.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : ProductsBaseController
    {
        public IActionResult Create() => this.View();
    }
}
