namespace Chushka.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string ClientId { get; set; }

        public virtual User Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
