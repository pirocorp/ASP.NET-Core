namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext _db;

        public SupplierService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<SupplierModel> All(bool areImporters)
            => this._db
                .Suppliers
                .Where(s => s.IsImporter == areImporters)
                .Select(s => new SupplierModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    TotalParts = s.Parts.Count,
                })
                .ToList();
    }
}
