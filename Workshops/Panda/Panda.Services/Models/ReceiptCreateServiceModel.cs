namespace Panda.Services.Models
{
    using AutoMapper;
    using Infrastructure;
    using Mapping;
    using Panda.Models;

    public class ReceiptCreateServiceModel : IMapTo<Receipt>, IHaveCustomMappings
    {
        public double Weight { get; set; }

        public string PackageId { get; set; }

        public string RecipientId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ReceiptCreateServiceModel, Receipt>()
                .ForMember(
                    r => r.Fee,
                    opt => opt.MapFrom(m => GlobalConstants.FeeRatio * (decimal)m.Weight));
        }
    }
}
