namespace CarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services;
    using ViewModels.Parts;

    public class PartsController : Controller
    {
        private const int PAGE_SIZE = 25;

        private readonly IPartsService _partsService;
        private readonly ISupplierService _supplierService;

        public PartsController(IPartsService partsService, 
            ISupplierService supplierService)
        {
            this._partsService = partsService;
            this._supplierService = supplierService;
        }

        public IActionResult All(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            var parts = this
                ._partsService
                .AllListings(page, PAGE_SIZE);

            var totalParts = this._partsService.Count();
            var totalPages = Math.Ceiling(totalParts / (double) PAGE_SIZE);

            var model =  new PartPageListingModel()
            {
                Parts = parts,
                CurrentPage = page,
                TotalPages = (int)totalPages
            };

            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new PartFormModel()
            {
                Suppliers = this.GetSupplierListItems(),
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(PartFormModel model)
        {
            if (!this._supplierService.Exists(model.SupplierId))
            {
                this.ModelState.AddModelError(nameof(PartFormModel.SupplierId), "Invalid supplier.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Suppliers = this.GetSupplierListItems();

                return this.View(model);
            }

            this._partsService.Create(
                model.Name,
                model.Price,
                model.Quantity,
                model.SupplierId);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Delete(int id) => this.View(id);

        public IActionResult Destroy(int id)
        {
            this._partsService.Delete(id);

            return this.RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            var part = this._partsService.ById(id);

            if (part == null)
            {
                return this.NotFound();
            }

            var model = new PartFormModel()
            {
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity,
                IsEdit = true,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, PartFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.IsEdit = true;
                return this.View(model);
            }

            this._partsService.Edit(
                id,
                model.Price,
                model.Quantity);

            return this.RedirectToAction(nameof(this.All));
        }

        private IEnumerable<SelectListItem> GetSupplierListItems()
            => this._supplierService
                .All()
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                });
    }
}
