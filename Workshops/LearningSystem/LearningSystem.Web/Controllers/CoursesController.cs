namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Courses;
    using Services;

    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;

        public CoursesController(
            ICourseService courseService,
            UserManager<User> userManager)
        {
            this.courseService = courseService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (!await this.courseService.ExistsAsync(id))
            {
                return this.NotFound();
            }

            var userIsSignedInCourse = false;

            if (this.User.Identity?.IsAuthenticated ?? false)
            {
                var userId = this.userManager.GetUserId(this.User);
                userIsSignedInCourse = await this.courseService.UserIsSignedInCourse(id, this.User);
            }

            var model = new CourseDetailsViewModel()
            {
                Course = await this.courseService.GetById<CourseDetailsModel>(id),
                UserIsSignedInCourse = userIsSignedInCourse
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(int courseId)
        {
            if (!await this.courseService.ExistsAsync(courseId))
            {
                return this.NotFound();
            }

            var (title, id) = await this.CustomRedirectToAction(courseId);

            if (await this.courseService.UserIsSignedInCourse(courseId, this.User))
            {
                return this.RedirectToAction(nameof(this.Details), new { title, id });
            }

            await this.courseService.SignInUserAsync(courseId, this.User);
            return this.RedirectToAction(nameof(this.Details), new { title, id });
        }

        [HttpPost]
        public async Task<IActionResult> SignOut(int courseId)
        {
            if (!await this.courseService.ExistsAsync(courseId))
            {
                return this.NotFound();
            }

            var (title, id) = await this.CustomRedirectToAction(courseId);

            if (!await this.courseService.UserIsSignedInCourse(courseId, this.User))
            {
                return this.RedirectToAction(nameof(this.Details), new { title, id });
            }

            await this.courseService.SignOutUserAsync(courseId, this.User);
            return this.RedirectToAction(nameof(this.Details), new { title, id });
        }

        private async Task<(string title, int id)> CustomRedirectToAction(int courseId)
        {
            var title = (await this.courseService.GetById<CourseRedirectModel>(courseId)).Name.ToFriendlyUrl();

            return (title, courseId);
        }
    }
}
