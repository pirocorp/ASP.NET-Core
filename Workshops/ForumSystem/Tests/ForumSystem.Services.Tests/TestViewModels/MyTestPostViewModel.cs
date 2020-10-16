namespace ForumSystem.Services.Tests.TestViewModels
{
    using ForumSystem.Data.Models;
    using Mapping;

    public class MyTestPostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
