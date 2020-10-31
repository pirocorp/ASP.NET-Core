namespace Stopify.Web.Models.ViewModels.Home
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class ProductIndexViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
