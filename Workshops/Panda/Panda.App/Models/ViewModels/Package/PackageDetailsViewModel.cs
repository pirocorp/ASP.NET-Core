namespace Panda.App.Models.ViewModels.Package
{
    using System;

    using Panda.Infrastructure;
    using Panda.Mapping;
    using Panda.Models;

    public class PackageDetailsViewModel : IMapFrom<Package>
    {
        public string RecipientId { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string StatusName { get; set; }

        public string RecipientUserName { get; set; }

        public string DeliveryDate => this
            .StatusName
            .Equals(ShipmentStatus.Delivered.ToString())
            ? this.StatusName
            : this.EstimatedDeliveryDate.ToString("D");
    }
}
