namespace Stopify.Web.Models.ViewModels.Product
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class ProductDetailsViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public string TypeName { get; set; }

        public bool Sold { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(
                    view => view.Sold,
                    opt => opt.MapFrom(product => product.OrderId != null));
        }
    }
}
