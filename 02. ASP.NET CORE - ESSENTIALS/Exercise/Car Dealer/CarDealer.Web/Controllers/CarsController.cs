namespace CarDealer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services;
    using ViewModels.Cars;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IPartsService partsService;

        public CarsController(ICarService carService, 
            IPartsService partsService)
        {
            this.carService = carService;
            this.partsService = partsService;
        }

        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {
            var cars = this
                .carService
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
            var carsWithParts = this.carService.WithParts();

            return this.View(carsWithParts);
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            var model = new CarFormModel()
            {
                AllParts = this.GetPartsSelectListItems(),
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(Create))]
        //if name of model and property of the model are the same
        //ASP.NET can't bind the model
        public IActionResult Create(CarFormModel carModel)
        {
            if (!this.ModelState.IsValid)
            {
                carModel.AllParts = this.GetPartsSelectListItems();

                return this.View(carModel);
            }

            this.carService.Create(
                carModel.Make,
                carModel.Model,
                carModel.TravelledDistance,
                carModel.SelectedParts);

            return this.RedirectToAction(nameof(this.Parts));
        }

        private IEnumerable<SelectListItem> GetPartsSelectListItems()
            => this.partsService
                .All()
                .Select(p => new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString(),
                })
                .ToList();
    }
}
