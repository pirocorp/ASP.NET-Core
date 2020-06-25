namespace CarDealer.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using Models.Cars;
    using Models.Customers;
    using Models.Sales;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext _db;

        public CustomerService(CarDealerDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var customersQuery =
                this._db
                    .Customers
                    .AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDay)
                        .ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDay)
                        .ThenBy(c => c.Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }

            return customersQuery
                .Select(c => new CustomerModel()
                {
                    Name = c.Name,
                    BirthDay = c.BirthDay,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerTotalSalesModel TotalSalesById(int id)
            => this._db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerTotalSalesModel()
                {
                    Name = c.Name,
                    IsYoungDriver = c.IsYoungDriver,
                    BoughtCars = c
                        .Sales
                        .Select(s => new SaleModel()
                        {
                            Price = s.Car.Parts.Sum(p => p.Part.Price),
                            Discount = s.Discount
                        })
                })
                .FirstOrDefault();
    }
}
