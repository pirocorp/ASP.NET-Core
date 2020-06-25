namespace CarDealer.Web.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            this._saleService = saleService;
        }


        [Route("")]
        public IActionResult All()
        {
            var result = this._saleService.All();

            this.ViewBag.Type = "All";

            return this.View(result);
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var result = this._saleService.Details(id);

            return this.ViewOrNotFound(result);
        }

        [Route("discounted/{percent?}")]
        public IActionResult Discounted(double? percent)
        {
            var result = this._saleService.Discounted(percent);

            this.ViewBag.Type = "Discounted";

            return this.View("All", result);
        } 
    }
}
