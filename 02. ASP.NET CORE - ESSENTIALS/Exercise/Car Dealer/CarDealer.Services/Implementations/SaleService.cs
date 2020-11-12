namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models.Cars;
    using Models.Sales;

    public class SaleService : ISaleService
    {
        private const double Epsilon = 1e-12;
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleListModel> All()
            => this.db
                .Sales
                .OrderByDescending(s => s.Id)
                .Select(s => new SaleListModel()
                {
                    Id = s.Id,
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Price = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();

        public SaleDetailsModel Details(int id)
            => this.db
                .Sales
                .Where(s => s.Id == id)
                .Select(s => new SaleDetailsModel()
                {
                    Id = s.Id,
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    Car = new CarModel()
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    }
                })
                .FirstOrDefault();

        public IEnumerable<SaleListModel> Discounted(double? discount)
        {
            var query = this.db
                .Sales
                .AsQueryable();
            
            if (discount != null)
            {
                query = query
                    .Where(s => (int)((s.Discount)* 100) == (int)discount);
            }
            else
            {
                query = query
                    .Where(s => s.Discount > Epsilon);
            }

            return query
                .Select(s => new SaleListModel()
                {
                    Id = s.Id,
                    CustomerName = s.Customer.Name,
                    IsYoungDriver = s.Customer.IsYoungDriver,
                    Discount = s.Discount,
                    Price = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();
        }
    }
}
