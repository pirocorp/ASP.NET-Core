namespace LearningSystem.Services.Models.Trainers
{
    using Data;

    public class GradeServiceModel
    {
        public int CourseId { get; set; }

        public string StudentId { get; set; }

        public Grade Grade { get; set; }
    }
}
