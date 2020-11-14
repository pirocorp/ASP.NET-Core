namespace LearningSystem.Web.Models.Admin.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using static Common.GlobalConstants;

    public class AdminUsersIndexViewModel
    {
        public AdminUsersIndexViewModel()
        {
            this.Users = new List<AdminUserListingServiceModel>();
            this.Roles = new List<AdminRoleListingServiceModel>();
        }

        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public IEnumerable<AdminRoleListingServiceModel> Roles { get; set; }

        public IEnumerable<SelectListItem> RolesDropDown
        {
            get
            {
                var roles = this.Roles
                    .Select(r => new SelectListItem(r.Name, r.Id))
                    .ToList();

                roles.Add(new SelectListItem(RemoveRole, RemoveRole, true, true));
                return roles;
            }
        }
    }
}
