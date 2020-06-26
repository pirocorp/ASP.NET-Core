namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models.Suppliers;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext _db;

        public SupplierService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<SupplierListingModel> AllListings(bool areImporters)
            => this._db
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
            => this._db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToList();

        public bool Exists(int id)
            => this._db.Suppliers.Any(s => s.Id == id);
    }
}
