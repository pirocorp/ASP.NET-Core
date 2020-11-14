namespace LearningSystem.Web.Models.Admin.Users
{
    using Data.Models;
    using Services.Mapping;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
