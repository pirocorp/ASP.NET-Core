namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Suppliers;

    public class SuppliersController : Controller
    {
        private const string SUPPLIERS_VIEW = "Suppliers";

        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            this._supplierService = supplierService;
        }

        public IActionResult Local()
        {
            var model = this.GetSuppliersModel(false);

            return this.View(SUPPLIERS_VIEW, model);
        }

        public IActionResult Importers()
        {
            var model = this.GetSuppliersModel(true);

            return this.View(SUPPLIERS_VIEW, model);
        }

        private SuppliersModel GetSuppliersModel(bool areImporters)
        {
            var type = areImporters
                ? "Importer"
                : "Local";

            var suppliers = this
                ._supplierService
                .All(areImporters);

            var result = new SuppliersModel()
            {
                Type = type,
                Suppliers = suppliers
            };

            return result;
        }
    }
}
