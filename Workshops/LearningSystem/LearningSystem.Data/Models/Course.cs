// ReSharper disable VirtualMemberCallInConstructor
namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static Common.DataConstants;

    public class Course : IEntityTypeConfiguration<Course>
    {
        public Course()
        {
            this.Students = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(CourseNameMinLength)]
        [MaxLength(CourseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(CourseDescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string TrainerId { get; set; }

        public virtual User Trainer { get; set; }

        public virtual ICollection<StudentCourse> Students { get; set; }

        public void Configure(EntityTypeBuilder<Course> course)
        {
            course
                .HasOne(c => c.Trainer)
                .WithMany(t => t.Trainings)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
