namespace WebApi.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;

    using WebApi.Common.Mapping;
    using WebApi.Data.Models;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public ArticleViewModel()
        {
            this.Comments = new HashSet<CommentArticleViewModel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorUserName { get; set; }

        public string AuthorId { get; set; }

        public ICollection<CommentArticleViewModel> Comments { get; set; }
    }
}
