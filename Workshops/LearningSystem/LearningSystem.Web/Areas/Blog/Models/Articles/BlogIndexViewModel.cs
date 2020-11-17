namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using System.Collections.Generic;

    public class BlogIndexViewModel
    {
        public BlogIndexViewModel()
        {
            this.Articles = new List<IndexArticleListingModel>();
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<IndexArticleListingModel> Articles { get; set; }
    }
}
