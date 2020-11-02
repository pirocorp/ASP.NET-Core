// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace Stopify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Products = new HashSet<Product>();
        }

        public string Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string UserId { get; set; }

        public virtual StopifyUser User { get; set; }

        public int StatusId { get; set; }

        public virtual OrderStatus Status { get; set; }
    }
}
