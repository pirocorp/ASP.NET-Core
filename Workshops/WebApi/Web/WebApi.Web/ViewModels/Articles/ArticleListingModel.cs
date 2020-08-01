namespace WebApi.Web.ViewModels.Articles
{
    using WebApi.Common.Mapping;
    using WebApi.Data.Models;

    public class ArticleListingModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorUserName { get; set; }

        public string AuthorId { get; set; }

        public int CommentsCount { get; set; }
    }
}
