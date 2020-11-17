namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Courses;
    using Services;

    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var course = await this.courseService.GetById<CourseDetailsViewModel>(id);

            return this.View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Enlist(int courseId)
        {
            return this.Ok();
        }
    }
}
