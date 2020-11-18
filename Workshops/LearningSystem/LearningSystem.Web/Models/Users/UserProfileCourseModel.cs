namespace LearningSystem.Web.Models.Users
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Services.Mapping;

    public class UserProfileCourseModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Grade? Grade { get; set; }

        public void CreateMappings(IProfileExpression mapper)
        {
            // This argument can be passed to automapper in ProjectTo method or my To extension method 
            string studentId = null;

            mapper
                .CreateMap<Course, UserProfileCourseModel>()
                .ForMember(p => p.Grade, opt => opt
                    .MapFrom(c => c.Students
                        .Where(sc => sc.StudentId.Equals(studentId))
                        .Select(s => s.Grade)
                        .FirstOrDefault()));

        }
    }
}
