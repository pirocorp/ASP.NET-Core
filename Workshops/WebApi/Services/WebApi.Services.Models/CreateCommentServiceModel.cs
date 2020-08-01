namespace WebApi.Services.Models
{
    using Data.Models;

    public class CreateCommentServiceModel
    {
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
