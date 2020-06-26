namespace CarDealer.Web.ViewModels.Suppliers
{
    using System.Collections.Generic;
    using Services.Models;
    using Services.Models.Suppliers;

    public class SuppliersModel
    {
        public string Type { get; set; }

        public IEnumerable<SupplierListingModel> Suppliers { get; set; }
    }
}
