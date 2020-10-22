// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace Panda.Models
{
    using System;

    public class Receipt
    {
        public Receipt()
        {
            this.Id = Guid.NewGuid().ToString();

            this.IssuedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientId { get; set; }

        public virtual PandaUser Recipient { get; set; }

        public string PackageId { get; set; }

        public virtual Package Package { get; set; }
    }
}
