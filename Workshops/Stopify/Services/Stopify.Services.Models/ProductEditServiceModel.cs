namespace Stopify.Services.Models
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class ProductEditServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IFormFile Picture { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public int TypeId { get; set; }
    }
}
