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

        [Route(nameof(Parts), Order = 1)]
        public IActionResult Parts()
        {
            var carsWithParts = this._carService.WithParts();

            return this.View(carsWithParts);
        }

        [Route(nameof(Create))]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Route(nameof(Create))]
        //if name of model and property of the model are the same
        //ASP.NET can't bind the model
        public IActionResult Create(CarFormModel carModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(carModel);
            }

            this._carService.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance);

            return this.RedirectToAction(nameof(this.Parts));
        }
    }
}
