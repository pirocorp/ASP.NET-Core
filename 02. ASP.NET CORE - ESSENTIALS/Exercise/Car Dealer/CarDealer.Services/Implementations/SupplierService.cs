namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models.Suppliers;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierListingModel> AllListings(bool areImporters)
            => this.db
                .Suppliers
                .OrderByDescending(s => s.Id)
                .Where(s => s.IsImporter == areImporters)
                .Select(s => new SupplierListingModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    TotalParts = s.Parts.Count,
                })
                .ToList();

        public IEnumerable<SupplierModel> All()
            => this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToList();

        public bool Exists(int id)
            => this.db.Suppliers.Any(s => s.Id == id);
    }
}
