namespace LearningSystem.Web.Tests.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Areas.Admin.Models.Courses;
    using Common;
    using Data.Models;
    using FluentAssertions;
    using Ganss.XSS;
    using LearningSystem.Services;
    using LearningSystem.Services.Mapping;
    using LearningSystem.Services.Models.Admin.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using Web.Controllers;
    using Xunit;
    using CoursesController = Areas.Admin.Controllers.CoursesController;

    public class CoursesControllerTest
    {
        public CoursesControllerTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public void CoursesControllerShouldBeForAdminsOnly()
        {
            // Arrange
            var controller = typeof(CoursesController);

            // Act
            var attributes = controller
                .GetCustomAttributes(true);

            var areaAttribute = attributes
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            var authorizeAttribute = attributes
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            authorizeAttribute.Should().NotBeNull();

            areaAttribute.RouteValue.Should().Be(GlobalConstants.AdministratorArea);
            authorizeAttribute.Roles.Should().Be(GlobalConstants.AdministratorRole);
        }

        [Fact]
        public async Task GetCreateShouldReturnViewWithValidModel()
        {
            // Arrange
            var users = new List<User>()
            {
                new() { UserName = "Trainer1", Id = "1", },
                new() { UserName = "Trainer2", Id = "2", },
                new() { UserName = "Trainer3", Id = "3", },
            };
            var userManager = GetUserManagerMock(users);
            var controller = new CoursesController(null, userManager.Object, null, null);

            // Act
            var result = await controller.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<AddCourseFormModel>();

            var formModel = model.As<AddCourseFormModel>();

            formModel.StartDate.Year.Should().Be(DateTime.UtcNow.Year);
            formModel.StartDate.Month.Should().Be(DateTime.UtcNow.Month);
            formModel.StartDate.Day.Should().Be(DateTime.UtcNow.Day);

            var endDate = formModel.StartDate.AddDays(30);

            formModel.EndDate.Year.Should().Be(endDate.Year);
            formModel.EndDate.Month.Should().Be(endDate.Month);
            formModel.EndDate.Day.Should().Be(endDate.Day);

            this.AssertTrainersSelectList(formModel.Trainers, users);
        }

        [Fact]
        public async Task PostCreateShouldReturnViewWithValidModelWhenModelStateIsInvalid()
        {
            // Arrange
            var users = new List<User>()
            {
                new() { UserName = "Trainer1", Id = "1", },
                new() { UserName = "Trainer2", Id = "2", },
                new() { UserName = "Trainer3", Id = "3", },
            };
            var userManager = GetUserManagerMock(users);
            var controller = new CoursesController(null, userManager.Object, null, null);
            controller.ModelState.AddModelError(string.Empty, string.Empty);

            // Act
            var result = await controller.Create(new AddCourseFormModel());

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<AddCourseFormModel>();

            var formModel = model.As<AddCourseFormModel>();
            this.AssertTrainersSelectList(formModel.Trainers, users);
        }

        [Fact]
        public async Task PostCreateShouldReturnRedirectWithValidModel()
        {
            // Arrange
            var model = new AddCourseFormModel()
            {
                Name = "Course 1",
                Description = "Test Course",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30),
                TrainerId = "1"
            };

            var mapper = AutoMapperConfig.MapperInstance;

            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedAttributes.Add("class");

            CreateCourseServiceModel serviceModel = null;

            // Service is mocked to store data received from controller in serviceModel variable
            var courseService = new Mock<ICourseService>();
            courseService
                .Setup(cs => cs.CreateAsync(It.IsAny<CreateCourseServiceModel>()))
                .Callback((CreateCourseServiceModel input) =>
                {
                    serviceModel = input;
                })
                .ReturnsAsync(1);

            var controller = new CoursesController(courseService.Object, null, sanitizer, mapper);

            var message = string.Empty;

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(td => td[GlobalConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object value) => message = value as string);

            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Create(model);

            // Assert

            // Asserting that controller is passed same data to service it receives
            serviceModel.Name.Should().Be(model.Name);
            serviceModel.Description.Should().Be(model.Description);
            serviceModel.StartDate.Should().Be(model.StartDate);
            serviceModel.EndDate.Should().Be(model.EndDate);
            serviceModel.TrainerId.Should().Be(model.TrainerId);

            // Assert Message
            var expectedMessage = $"Course {model.Name} created successfully.";
            message.Should().Be(expectedMessage);

            // Assert Redirect
            result.Should().BeOfType<RedirectToActionResult>();
            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(HomeController.Index));
            result.As<RedirectToActionResult>().ControllerName.Should().Be("Home");
            result.As<RedirectToActionResult>().RouteValues["area"].Should().Be(string.Empty);
        }

        private Mock<UserManager<User>> GetUserManagerMock(List<User> users)
        {
            var userManager = Tests.MockUserManager<User>();

            userManager
                .Setup(um => um.GetUsersInRoleAsync(It.IsAny<string>()))
                .ReturnsAsync(users);

            return userManager;
        }

        private void AssertTrainersSelectList(IEnumerable<SelectListItem> trainers, List<User> users)
        {
            trainers.Should().Match(items => items.Count() == users.Count);

            trainers.First().Should().Match(
                u => u.As<SelectListItem>().Value == users.First().Id
                     && u.As<SelectListItem>().Text == users.First().UserName);

            trainers.Last().Should().Match(
                u => u.As<SelectListItem>().Value == users.Last().Id
                     && u.As<SelectListItem>().Text == users.Last().UserName);
        }
    }
}
