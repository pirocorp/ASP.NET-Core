// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace Panda.Models
{
    using System;

    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public string StatusId { get; set; }

        public virtual PackageStatus Status { get; set; }

        public string RecipientId { get; set; }

        public virtual PandaUser Recipient { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}
