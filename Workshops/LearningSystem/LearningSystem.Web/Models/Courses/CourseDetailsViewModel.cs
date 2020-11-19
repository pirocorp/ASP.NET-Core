namespace LearningSystem.Web.Models.Courses
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class CourseDetailsViewModel
    {
        public CourseDetailsModel Course { get; set; }

        public bool UserIsSignedInCourse { get; set; }

        public bool ExamIsSubmitted { get; set; }

        [Required]
        public IFormFile Solution { get; set; }
    }
}
