namespace LearningSystem.Web.Models.Courses
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class CourseDetailsModel  : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TrainerName { get; set; }
    }
}
