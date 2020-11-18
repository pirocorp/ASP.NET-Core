namespace LearningSystem.Web.Models.Trainers
{
    using Data;
    using Services.Mapping;
    using Services.Models.Trainers;

    public class TrainersStudentsInputModel : IMapTo<GradeServiceModel>
    {
        public int CourseId { get; set; }

        public string StudentId { get; set; }

        public Grade Grade { get; set; }
    }
}
