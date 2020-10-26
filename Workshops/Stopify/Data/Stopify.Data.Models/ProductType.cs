namespace Stopify.Data.Models
{
    using System.Collections.Generic;

    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}