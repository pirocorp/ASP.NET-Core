namespace LearningSystem.Web.Models.Trainers
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Services.Mapping;

    public class TrainersStudentsStudentListingModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Grade? Grade { get; set; }

        public bool ExamIsSubmitted { get; set; }

        public void CreateMappings(IProfileExpression mapper)
        {
            var courseId = default(int);

            mapper
                .CreateMap<User, TrainersStudentsStudentListingModel>()
                .ForMember(s => s.Grade, opt => opt
                    .MapFrom(u => u.Courses
                        .Where(c => c.CourseId.Equals(courseId))
                        .Select(c => c.Grade)
                        .FirstOrDefault()))
                .ForMember(s => s.ExamIsSubmitted, opt => opt
                    .MapFrom(u => u.Courses
                        .Where(c => c.CourseId.Equals(courseId))
                        .Any(c => c.ExamSubmission != null)));
        }
    }
}
