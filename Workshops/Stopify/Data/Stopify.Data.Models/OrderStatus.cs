// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace Stopify.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderStatus
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
