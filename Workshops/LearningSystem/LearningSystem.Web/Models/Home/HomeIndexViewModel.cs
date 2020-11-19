namespace LearningSystem.Web.Models.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Courses;
    using Infrastructure.Enum;

    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            this.Articles = new List<HomeIndexArticleListingModel>();
            this.Courses = new List<HomeIndexCourseListingModel>();
            this.Users = new List<HomeIndexUserListingModel>();
        }

        public string SearchText { get; set; }

        public SearchType? Search { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public List<string> Searches => Enum.GetValues<SearchType>().Select(e => e.ToString()).ToList();

        public IEnumerable<HomeIndexArticleListingModel> Articles { get; set; }

        public IEnumerable<HomeIndexCourseListingModel> Courses { get; set; }

        public IEnumerable<HomeIndexUserListingModel> Users { get; set; }
    }
}
