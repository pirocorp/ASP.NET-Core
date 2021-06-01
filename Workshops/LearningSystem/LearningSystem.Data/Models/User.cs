// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static Common.DataConstants;

    public class User : IdentityUser
    {
        public User()
        {
            this.Articles = new HashSet<Article>();
            this.Courses = new HashSet<StudentCourse>();
            this.Trainings = new HashSet<Course>();
        }

        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        
        public virtual ICollection<StudentCourse> Courses { get; set; }

        public virtual ICollection<Course> Trainings { get; set; }
    }
}
