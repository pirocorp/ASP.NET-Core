namespace LearningSystem.Web.Models.Admin.Users
{
    using Microsoft.AspNetCore.Identity;
    using Services.Mapping;

    public class AdminRoleListingServiceModel : IMapFrom<IdentityRole>
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
    }
}
