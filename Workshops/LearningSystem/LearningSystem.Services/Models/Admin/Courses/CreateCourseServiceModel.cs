namespace LearningSystem.Services.Models.Admin.Courses
{
    using System;

    public class CreateCourseServiceModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string TrainerId { get; set; }
    }
}
