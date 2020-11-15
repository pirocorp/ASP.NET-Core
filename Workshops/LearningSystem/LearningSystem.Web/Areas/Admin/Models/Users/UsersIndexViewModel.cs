namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using static Common.GlobalConstants;

    public class UsersIndexViewModel
    {
        public UsersIndexViewModel()
        {
            this.Users = new List<UserListingServiceModel>();
            this.Roles = new List<RoleListingServiceModel>();
        }

        public IEnumerable<UserListingServiceModel> Users { get; set; }

        public IEnumerable<RoleListingServiceModel> Roles { get; set; }

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
