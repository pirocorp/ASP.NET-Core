namespace LearningSystem.Web.Models.Trainers
{
    using System.Collections.Generic;

    public class TrainersCoursesViewModel
    {
        public IEnumerable<TrainersCoursesCourseListingModel> Courses { get; set; }

        public string Name { get; set; }
    }
}
