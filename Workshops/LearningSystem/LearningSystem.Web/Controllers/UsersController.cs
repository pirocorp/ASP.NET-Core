namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly ICourseService courseService;

        public UsersController(
            UserManager<User> userManager,
            IUserService userService,
            ICourseService courseService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user is null)
            {
                return this.NotFound();
            }

            var studentId = user.Id;

            var model = await this.userService.GetByUsernameAsync<UserProfileUserModel>(studentId);

            return this.View(model);
        }

        public async Task<IActionResult> DownloadCertificate(int id)
        {
            var student = await this.userManager.GetUserAsync(this.User);

            if (!await this.courseService.ExistsAsync(id))
            {
                return this.BadRequest();
            }

            if (!await this.courseService.UserIsSignedInCourse(id, student.Id))
            {
                return this.BadRequest();
            }

            var courseName = await this.courseService.GetCourseNameAsync(id);

            var certificateContents = await this.userService.GetPdfCertificate(id, student.Id);

            if (certificateContents is null)
            {
                return this.BadRequest();
            }

            return this.File(certificateContents, "application/pdf", $"{student.Name} - {courseName} Certificate.pdf");
        }
    }
}
