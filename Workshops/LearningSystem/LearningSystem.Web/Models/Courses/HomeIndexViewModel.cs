namespace LearningSystem.Web.Models.Courses
{
    using System.Collections.Generic;

    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            this.Courses = new List<HomeIndexCourseListingModel>();
        }

        public IEnumerable<HomeIndexCourseListingModel> Courses { get; set; }
    }
}
