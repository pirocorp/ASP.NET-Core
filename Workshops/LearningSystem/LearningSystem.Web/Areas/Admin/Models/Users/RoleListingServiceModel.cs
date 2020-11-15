namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using Microsoft.AspNetCore.Identity;
    using Services.Mapping;

    public class RoleListingServiceModel : IMapFrom<IdentityRole>
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
    }
}
