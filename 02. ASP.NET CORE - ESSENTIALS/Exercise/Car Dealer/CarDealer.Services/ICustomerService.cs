namespace CarDealer.Services
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Customers;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> Ordered(OrderDirection order);

        CustomerTotalSalesModel TotalSalesById(int id);

        void Create(string name, DateTime birthday, bool isYoungDriver);

        CustomerModel ById(int id);

        void Edit(int id, string modelName, DateTime modelBirthDay, bool modelIsYoungDriver);
        
        bool Exists(int id);
    }
}
