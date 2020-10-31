namespace Panda.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    // ReSharper disable MemberCanBeProtected.Global
    // ReSharper disable VirtualMemberCallInConstructor
    // ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
        }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
