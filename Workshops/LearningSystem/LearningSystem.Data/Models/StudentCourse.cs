namespace LearningSystem.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentCourse : IEntityTypeConfiguration<StudentCourse>
    {
        public string StudentId { get; set; }

        public virtual User Student { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public Grade? Grade { get; set; }

        public void Configure(EntityTypeBuilder<StudentCourse> studentCourse)
        {
            studentCourse
                .HasKey(st => new { st.StudentId, st.CourseId });

            studentCourse
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            studentCourse
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
