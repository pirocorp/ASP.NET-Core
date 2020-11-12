namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using ViewModels.Suppliers;

    public class SuppliersController : Controller
    {
        private const string SuppliersView = "Suppliers";

        private readonly ISupplierService supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
        }

        public IActionResult Local()
        {
            var model = this.GetSuppliersModel(false);

            return this.View(SuppliersView, model);
        }

        public IActionResult Importers()
        {
            var model = this.GetSuppliersModel(true);

            return this.View(SuppliersView, model);
        }

        private SuppliersModel GetSuppliersModel(bool areImporters)
        {
            var type = areImporters
                ? "Importer"
                : "Local";

            var suppliers = this
                .supplierService
                .AllListings(areImporters);

            var result = new SuppliersModel()
            {
                Type = type,
                Suppliers = suppliers
            };

            return result;
        }
    }
}
