namespace Panda.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    // ReSharper disable MemberCanBeProtected.Global
    // ReSharper disable VirtualMemberCallInConstructor
    public class PandaUser : IdentityUser
    {
        public PandaUser()
        {
            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
        }

        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
