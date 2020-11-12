namespace CarDealer.Services.Models.Customers
{
    using System.Collections.Generic;
    using System.Linq;
    using Sales;

    public class CustomerTotalSalesModel
    {
        public string Name { get; set; }

        public bool IsYoungDriver { get; set; }

        public IEnumerable<SaleModel> BoughtCars { get; set; }

        public decimal TotalMoneySpent
            => this.BoughtCars
                   .Sum(c => c.Price * (decimal) (1 - c.Discount)) 
               * (this.IsYoungDriver ? 0.95M : 1);
    }
}
