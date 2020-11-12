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
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [Route("all/{order?}")]
        public IActionResult All(string order)
        {
            var orderDirection = order?.ToLower() == "desc"
                ? OrderDirection.Descending
                : OrderDirection.Ascending;

            var allCustomers = this
                .customerService
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
                .customerService
                .TotalSalesById(id);

            return this.ViewOrNotFound(result);
        }

        [Route(nameof(Create))]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.customerService.Create(
                model.Name,
                model.BirthDay,
                model.IsYoungDriver);

            return this.RedirectToAction(nameof(this.All));
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.customerService.ById(id);

            if (customer == null)
            {
                return this.NotFound();
            }

            var viewModelCustomer = new CustomerFormModel()
            {
                Name = customer.Name,
                BirthDay = customer.BirthDay,
                IsYoungDriver = customer.IsYoungDriver
            };

            return this.View(viewModelCustomer);
        }

        [HttpPost]
        [Route(nameof(Edit) + "/{id}" )]
        public IActionResult Edit(int id, CustomerFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var customerExists = this.customerService.Exists(id);

            if (!customerExists)
            {
                return this.NotFound();
            }

            this.customerService.Edit(
                id, 
                model.Name,
                model.BirthDay,
                model.IsYoungDriver);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
