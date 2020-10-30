namespace Stopify.Web.Models.ViewModels.Cart
{
    using Data.Models;
    using Services.Mapping;

    public class CartProductListingViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
