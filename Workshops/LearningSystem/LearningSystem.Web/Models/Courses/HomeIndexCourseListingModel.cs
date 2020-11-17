namespace LearningSystem.Web.Models.Courses
{
    using System;
    using Data.Models;
    using Services.Mapping;
    using static Common.GlobalConstants;

    public class HomeIndexCourseListingModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public string TrainerName { get; set; }

        public string DisplayDescription => this.Description.Length <= ContentDemoLength 
            ? this.Description
            : this.Description
                .Substring(0, ContentDemoLength)
                .TrimEnd('/')
                .TrimEnd('<')
                + "...";
    }
}
