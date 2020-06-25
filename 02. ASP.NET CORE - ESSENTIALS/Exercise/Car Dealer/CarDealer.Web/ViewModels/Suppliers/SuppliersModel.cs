namespace CarDealer.Web.ViewModels.Suppliers
{
    using System.Collections.Generic;
    using Services.Models;

    public class SuppliersModel
    {
        public string Type { get; set; }

        public IEnumerable<SupplierModel> Suppliers { get; set; }
    }
}
