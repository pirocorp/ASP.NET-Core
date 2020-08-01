namespace WebApi.Web.ViewModels.Comments
{
    using WebApi.Common.Mapping;
    using WebApi.Data.Models;

    public class CommentCreateViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUserName { get; set; }

        public int ArticleId { get; set; }

        public string ArticleTitle { get; set; }
    }
}
