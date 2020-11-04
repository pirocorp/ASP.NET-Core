namespace Stopify.Web.Models.ViewModels.Home
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Common;
    using Services.Mapping;

    public class ProductIndexViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }

        public double Rating { get; set; }

        public int FullStars => (int)Math.Ceiling(this.Rating);

        public int EmptyStars => GlobalConstants.MaxRating - this.FullStars;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Product, ProductIndexViewModel>()
                .ForMember(
                    view => view.Rating,
                    opt => opt.MapFrom(product => product.Ratings.Average(r => r.Score))
                );
        }
    }
}
