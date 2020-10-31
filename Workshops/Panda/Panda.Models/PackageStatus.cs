// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace Panda.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PackageStatus
    {
        public PackageStatus()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Packages = new HashSet<Package>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Package> Packages { get; set; }
    }
}
