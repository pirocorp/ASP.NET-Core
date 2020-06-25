namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Cars;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            this._carService = carService;
        }

        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {
            var cars = this
                ._carService
                .ByMake(make);

            var result = new CarsByMakeModel()
            {
                Cars = cars,
                Make = make,
            };

            return this.View(result);
        }

        [Route("parts", Order = 1)]
        public IActionResult Parts()
        {
            var carsWithParts = this._carService.WithParts();

            return this.View(carsWithParts);
        }

        [Route("all")]
        public IActionResult All()
        {
            var cars = this._carService.All();

            var result = new CarsByMakeModel()
            {
                Make = "All",
                Cars = cars
            };

            return this.View("ByMake", result);
        }
    }
}
