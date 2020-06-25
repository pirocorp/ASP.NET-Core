namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Models;
    using ViewModels.Customers;

    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        public IActionResult All(string order)
        {
            var orderDirection = order?.ToLower() == "desc"
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            var allCustomers = this
                ._customerService
                .OrderedCustomers(orderDirection);

            var result = new AllCustomersModel()
            {
                Customers = allCustomers,
                OrderDirection = orderDirection,
            };

            return this.View(result);
        }
    }
}
