namespace LearningSystem.Web.Models.Courses
{
    using Data.Models;
    using Services.Mapping;

    public class CourseRedirectModel : IMapFrom<Course>
    {
        public string Name { get; set; }
    }
}
