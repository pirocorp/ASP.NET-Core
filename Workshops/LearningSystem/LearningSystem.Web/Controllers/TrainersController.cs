namespace LearningSystem.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Trainers;
    using Services;
    using Services.Models.Trainers;
    using static Common.GlobalConstants;

    [Authorize(Roles = TrainerRole)]
    public class TrainersController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly ITrainerService trainerService;
        private readonly ICourseService courseService;

        public TrainersController(
            UserManager<User> userManager,
            IMapper mapper,
            ITrainerService trainerService,
            ICourseService courseService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.trainerService = trainerService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Courses()
        {
            var model = new TrainersCoursesViewModel()
            {
                Courses = await this.trainerService.GetTrainingsAsync<TrainersCoursesCourseListingModel>(this.User),
                Name = this.User.Identity?.Name,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Students(int courseId)
        {
            if (!await this.courseService.ExistsAsync(courseId))
            {
                return this.NotFound();
            }

            if (!await this.trainerService.IsTrainerAsync(courseId, this.User))
            {
                return this.BadRequest();
            }

            var model = await this.LoadTrainersStudentsViewModelAsync(courseId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Students(TrainersStudentsViewModel model)
        {
            var student = await this.userManager.FindByIdAsync(model.Input.StudentId);

            if (student is null)
            {
                return this.NotFound();
            }

            var courseId = model.Input.CourseId;

            if (!await this.courseService.ExistsAsync(courseId))
            {
                return this.NotFound();
            }

            if (!await this.trainerService.IsTrainerAsync(courseId, this.User))
            {
                this.ModelState.AddModelError("Input.Grade", "You are not trainer for this course");
            }

            if (!await this.courseService.UserIsSignedInCourse(courseId, model.Input.StudentId))
            {
                this.ModelState.AddModelError("Input.Grade", "Student is not signed for the course");
            }

            if (!this.ModelState.IsValid)
            {
                model = await this.LoadTrainersStudentsViewModelAsync(courseId);
                return this.View(model);
            }

            var serviceModel = this.mapper.Map<GradeServiceModel>(model.Input);
            await this.trainerService.Grade(serviceModel);

            this.TempData.AddSuccessMessage($"Student {student.Name} get grade {model.Input.Grade}");
            return this.RedirectToAction(nameof(this.Students), new{ courseId });
        }

        public async Task<IActionResult> DownloadExam(int courseId, string studentId)
        {
            var student = await this.userManager.FindByIdAsync(studentId);

            if (!await this.courseService.ExistsAsync(courseId)
                || student is null)
            {
                return this.NotFound();
            }

            if (!await this.trainerService.IsTrainerAsync(courseId, this.User)
                || !await this.courseService.UserIsSignedInCourse(courseId, studentId))
            {
                return this.BadRequest();
            }

            var examContents = await this.trainerService.GetExamSubmission(courseId, studentId);

            if (examContents is null)
            {
                return this.BadRequest();
            }

            var courseName = await this.courseService.GetCourseNameAsync(courseId);
            return this.File(examContents, "application/zip", $"{student.Name} - {courseName} Exam-Submission.zip");
        }

        private async Task<TrainersStudentsViewModel> LoadTrainersStudentsViewModelAsync(int courseId)
        {
            var model = await this.courseService.GetById<TrainersStudentsViewModel>(courseId);
            model.Students = await this.trainerService.GetAllStudentsInCourseAsync<TrainersStudentsStudentListingModel>(courseId);

            return model;
        }
    }
}
