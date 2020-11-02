namespace Stopify.Web.Models.ViewModels.Receipt
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class ReceiptOrderListingModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public decimal TotalAmount { get; set; }

        public int ProductsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Order, ReceiptOrderListingModel>()
                .ForMember(
                    rolm => rolm.TotalAmount,
                    opt => opt.MapFrom(o => o.Products.Sum(p => p.Price)));
        }
    }
}
