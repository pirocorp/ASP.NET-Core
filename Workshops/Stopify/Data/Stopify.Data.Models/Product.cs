// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace Stopify.Data.Models
{
    using System;

    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public int TypeId { get; set; }

        public virtual ProductType Type { get; set; }
    }
}
