namespace LearningSystem.Web.Models.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class UserProfileUserModel : IMapFrom<User>, IHaveCustomMappings
    {
        public UserProfileUserModel()
        {
            this.Courses = new List<UserProfileCourseModel>();
        }

        public string Username { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<UserProfileCourseModel> Courses { get; set; }

        public void CreateMappings(IProfileExpression mapper)
        {
            mapper.CreateMap<User, UserProfileUserModel>()
                .ForMember(u => u.Courses, opt => opt.MapFrom(s => s.Courses.Select(sc => sc.Course)));
        }
    }
}
