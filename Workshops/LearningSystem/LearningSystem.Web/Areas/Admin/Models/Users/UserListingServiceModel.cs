namespace LearningSystem.Web.Areas.Admin.Models.Users
{
    using Data.Models;
    using Services.Mapping;

    public class UserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
