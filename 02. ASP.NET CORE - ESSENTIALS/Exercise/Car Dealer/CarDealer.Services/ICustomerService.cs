namespace CarDealer.Services
{
    using System.Collections.Generic;
    using Models;
    using Models.Customers;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> Ordered(OrderDirection order);

        CustomerTotalSalesModel TotalSalesById(int id);
    }
}
