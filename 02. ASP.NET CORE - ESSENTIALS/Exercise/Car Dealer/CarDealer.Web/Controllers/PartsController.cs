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
        private const int PageSize = 25;

        private readonly IPartsService partsService;
        private readonly ISupplierService supplierService;

        public PartsController(IPartsService partsService, 
            ISupplierService supplierService)
        {
            this.partsService = partsService;
            this.supplierService = supplierService;
        }

        public IActionResult All(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            var parts = this
                .partsService
                .AllListings(page, PageSize);

            var totalParts = this.partsService.Count();
            var totalPages = Math.Ceiling(totalParts / (double) PageSize);

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
            if (!this.supplierService.Exists(model.SupplierId))
            {
                this.ModelState.AddModelError(nameof(PartFormModel.SupplierId), "Invalid supplier.");
            }

            if (!this.ModelState.IsValid)
            {
                model.Suppliers = this.GetSupplierListItems();

                return this.View(model);
            }

            this.partsService.Create(
                model.Name,
                model.Price,
                model.Quantity,
                model.SupplierId);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Delete(int id) => this.View(id);

        public IActionResult Destroy(int id)
        {
            this.partsService.Delete(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int id)
        {
            var part = this.partsService.ById(id);

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

            this.partsService.Edit(
                id,
                model.Price,
                model.Quantity);

            return this.RedirectToAction(nameof(this.All));
        }

        private IEnumerable<SelectListItem> GetSupplierListItems()
            => this.supplierService
                .All()
                .Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                });
    }
}
