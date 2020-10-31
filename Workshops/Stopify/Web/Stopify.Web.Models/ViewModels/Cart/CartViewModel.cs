// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace Stopify.Web.Models.ViewModels.Cart
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Common;
    using Services.Mapping;

    public class CartViewModel : IMapFrom<Order>
    {
        public CartViewModel()
        {
            this.ImageTransformation = ImageTransformations.SmallImageListings;
            this.Products = new List<CartProductListingViewModel>();
        }

        public string Id { get; set; }

        public virtual ICollection<CartProductListingViewModel> Products { get; set; }

        public decimal Total => this.Products.Sum(p => p.Price);

        public string ImageTransformation { get; }
    }
}
