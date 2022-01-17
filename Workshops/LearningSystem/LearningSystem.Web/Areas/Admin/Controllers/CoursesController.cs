namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Models;
    using Ganss.XSS;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Courses;
    using Services;
    using Services.Models.Admin.Courses;
    using Web.Controllers;
    using static Common.GlobalConstants;

    public class CoursesController : AdminController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<User> userManager;
        private readonly IHtmlSanitizer htmlSanitizer;
        private readonly IMapper mapper;

        public CoursesController(
            ICourseService courseService,
            UserManager<User> userManager,
            IHtmlSanitizer htmlSanitizer,
            IMapper mapper)
        {
            this.courseService = courseService;
            this.userManager = userManager;
            this.htmlSanitizer = htmlSanitizer;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Create()
        {
            var model = new AddCourseFormModel()
            {
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                Trainers = await this.GetTrainers(),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCourseFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Trainers = await this.GetTrainers();

                return this.View(model);
            }

            model.Description = this.htmlSanitizer.Sanitize(model.Description);

            var serviceModel = this.mapper.Map<AddCourseFormModel, CreateCourseServiceModel>(model);
            await this.courseService.CreateAsync(serviceModel);

            this.TempData.AddSuccessMessage($"Course {model.Name} created successfully.");
            return this.RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        private async Task<IEnumerable<SelectListItem>> GetTrainers()
            => (await this.userManager.GetUsersInRoleAsync(TrainerRole))
                .Select(u => new SelectListItem(u.UserName, u.Id))
                .ToList();
    }
}
