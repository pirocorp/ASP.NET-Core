namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using System.Collections.Generic;

    public class BlogIndexViewModel
    {
        public IEnumerable<IndexArticleListingModel> Articles { get; set; }
    }
}
