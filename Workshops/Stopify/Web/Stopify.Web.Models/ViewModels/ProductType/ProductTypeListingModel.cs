namespace Stopify.Web.Models.ViewModels.ProductType
{
    using Data.Models;
    using Services.Mapping;

    public class ProductTypeListingModel : IMapFrom<ProductType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
