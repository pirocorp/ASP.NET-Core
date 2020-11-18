namespace LearningSystem.Web.Models.Trainers
{
    using System;
    using Data.Models;
    using Services.Mapping;

    public class TrainersCoursesCourseListingModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StudentsCount { get; set; }
    }
}
