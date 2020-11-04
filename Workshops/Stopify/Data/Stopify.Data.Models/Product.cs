// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor
namespace Stopify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Ratings = new HashSet<Rating>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public int TypeId { get; set; }

        public virtual ProductType Type { get; set; }

        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
