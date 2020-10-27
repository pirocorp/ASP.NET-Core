namespace Stopify.Web.Models.InputModels
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class ProductCreateInputModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public IFormFile Picture { get; set; }

        public int TypeId { get; set; }
    }
}
