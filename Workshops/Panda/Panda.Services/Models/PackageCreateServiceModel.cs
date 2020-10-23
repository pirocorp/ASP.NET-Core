namespace Panda.Services.Models
{
    using System;
    using Mapping;
    using Panda.Models;

    public class PackageCreateServiceModel : IMapTo<Package>
    {
        public PackageCreateServiceModel()
        {
            this.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(2);
        }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string RecipientId { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string StatusId { get; set; }
    }
}
