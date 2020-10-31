// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace Stopify.Data.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class StopifyUser : IdentityUser
    {
        public StopifyUser()
        {
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Orders = new HashSet<Order>();
        }

        public string FullName { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
