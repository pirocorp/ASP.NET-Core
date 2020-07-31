namespace WebApi.Web.ViewModels.Articles
{
    using WebApi.Common.Mapping;
    using WebApi.Services.Models;

    public class ArticleCreateInputModel : IMapTo<CreateArticleServiceModel>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
