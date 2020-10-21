namespace Panda.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    // ReSharper disable VirtualMemberCallInConstructor
    public class PandaRole : IdentityRole
    {
        public PandaRole()
            : this(null)
        {
        }

        public PandaRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
