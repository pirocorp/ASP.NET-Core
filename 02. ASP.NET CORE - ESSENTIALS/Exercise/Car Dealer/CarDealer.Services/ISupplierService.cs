namespace CarDealer.Services
{
    using System.Collections.Generic;
    using Models.Suppliers;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllListings(bool areImporters);

        IEnumerable<SupplierModel> All();

        bool Exists(int modelSupplierId);
    }
}
