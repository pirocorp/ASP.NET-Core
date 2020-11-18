namespace LearningSystem.Web.Models.Home
{
    using Data.Models;
    using Services.Mapping;

    public class HomeIndexUserListingModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
