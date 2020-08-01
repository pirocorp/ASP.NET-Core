namespace WebApi.Web.ViewModels.Articles
{
    using System;

    using WebApi.Common.Mapping;
    using WebApi.Data.Models;

    public class CommentArticleViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUserName { get; set; }
    }
}
