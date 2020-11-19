namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Common;
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

            string userId = string.Empty;

            if (this.User.Identity?.IsAuthenticated ?? false)
            {
                userId = this.userManager.GetUserId(this.User);
                userIsSignedInCourse = await this.courseService.UserIsSignedInCourse(id, this.User);
            }

            var model = new CourseDetailsViewModel()
            {
                Course = await this.courseService.GetById<CourseDetailsModel>(id),
                UserIsSignedInCourse = userIsSignedInCourse,
                ExamIsSubmitted = await this.courseService.ExamIsSubmittedAsync(id, userId)
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(CourseDetailsViewModel model)
        {
            var courseId = model.Course.Id;

            if (!await this.courseService.ExistsAsync(courseId))
            {
                return this.BadRequest();
            }

            if (!await this.courseService.UserIsSignedInCourse(courseId, this.User))
            {
                return this.BadRequest();
            }

            var (title, id) = await this.CustomRedirectToAction(courseId);

            if (model.Solution is null 
                || !model.Solution.FileName.ToLower().EndsWith(".zip")
                || model.Solution.Length > GlobalConstants.AllowedExamUploadFileSize
                || !this.ModelState.IsValid)
            {
                model.Course = await this.courseService.GetById<CourseDetailsModel>(courseId);
                model.UserIsSignedInCourse = true;

                this.TempData.AddErrorMessage($"Invalid file {model.Solution?.FileName} max allowed file size is {GlobalConstants.AllowedExamUploadFileSize / 1024}Kb");
                return this.RedirectToAction(nameof(this.Details), new { title, id });
            }

            var fileContents = await model.Solution.ToByteArrayAsync();
            var userId = this.userManager.GetUserId(this.User);

            var success = await this.courseService.SaveExamSubmission(courseId, userId, fileContents);

            if (!success)
            {
                return this.BadRequest();
            }

            this.TempData.AddSuccessMessage($"{model.Solution.FileName} successfully added.");
            return this.RedirectToAction(nameof(this.Details), new { title, id });
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
