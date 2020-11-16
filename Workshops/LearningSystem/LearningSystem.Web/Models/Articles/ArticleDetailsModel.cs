namespace LearningSystem.Web.Models.Articles
{
    using Data.Models;
    using Services.Mapping;

    public class ArticleDetailsModel : IMapFrom<Article>
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}
