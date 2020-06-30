namespace Chushka.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Orders =  new HashSet<Order>();
        }

        public string FullName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
