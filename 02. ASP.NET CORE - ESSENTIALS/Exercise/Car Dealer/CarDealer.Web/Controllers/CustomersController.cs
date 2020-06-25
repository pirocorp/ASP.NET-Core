namespace CarDealer.Web.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Models;
    using ViewModels.Customers;

    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [Route("all/{order?}")]
        public IActionResult All(string order)
        {
            var orderDirection = order?.ToLower() == "desc"
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            var allCustomers = this
                ._customerService
                .Ordered(orderDirection);

            var result = new AllCustomersModel()
            {
                Customers = allCustomers,
                OrderDirection = orderDirection,
            };

            return this.View(result);
        }

        [Route("{id}")]
        public IActionResult TotalSales(int id)
        {
            var result = this
                ._customerService
                .TotalSalesById(id);

            return this.ViewOrNotFound(result);
        }
    }
}
