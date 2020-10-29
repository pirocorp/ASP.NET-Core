namespace Stopify.Web.Models.ViewModels.Product
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class ProductDetailsViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public string TypeName { get; set; }
    }
}
