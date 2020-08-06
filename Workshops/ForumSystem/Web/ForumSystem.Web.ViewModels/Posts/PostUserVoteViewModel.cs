namespace ForumSystem.Web.ViewModels.Posts
{
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Mapping;

    public class PostUserVoteViewModel : IMapFrom<Vote>
    {
        public VoteType Type { get; set; }
    }
}
