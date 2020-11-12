namespace CarDealer.Web.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService saleService;

        public SalesController(ISaleService saleService)
        {
            this.saleService = saleService;
        }
        
        [Route("")]
        public IActionResult All()
        {
            var result = this.saleService.All();

            this.ViewBag.Type = "AllListings";

            return this.View(result);
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var result = this.saleService.Details(id);

            return this.ViewOrNotFound(result);
        }

        [Route("discounted/{percent?}")]
        public IActionResult Discounted(double? percent)
        {
            var result = this.saleService.Discounted(percent);

            this.ViewBag.Type = "Discounted";

            return this.View("All", result);
        } 
    }
}
