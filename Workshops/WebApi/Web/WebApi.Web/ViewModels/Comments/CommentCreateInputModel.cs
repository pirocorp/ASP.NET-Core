namespace WebApi.Web.ViewModels.Comments
{
    using WebApi.Common.Mapping;
    using WebApi.Services.Models;

    public class CommentCreateInputModel : IMapTo<CreateCommentServiceModel>
    {
        public string Content { get; set; }

        public int ArticleId { get; set; }
    }
}
