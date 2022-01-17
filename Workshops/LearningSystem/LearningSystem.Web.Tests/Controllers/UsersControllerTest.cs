namespace LearningSystem.Web.Tests.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Models;
    using FluentAssertions;
    using LearningSystem.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Moq;
    using Web.Controllers;
    using Xunit;

    public class UsersControllerTest
    {
        [Fact]
        public void DownloadCertificateShouldBeOnlyForAuthorizedUsers()
        {
            // Arrange
            var controller = typeof(UsersController);

            // Act
            var attributes = controller.GetCustomAttributes(true);

            // Assert
            attributes
                .Should()
                .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }

        [Fact]
        public async Task ProfileShouldReturnNotFoundWithInvalidUsername()
        {
            User user = null;

            // Arrange
            var userManager = Tests.MockUserManager<User>();

            userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            var controller = new UsersController(userManager.Object, null, null);

            // Act
            var result = await controller.Profile("Username");

            // Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ProfileShouldReturnViewWithCorrectModelWithValidUsername()
        {
            // Arrange
            const string userId = "SomeId";
            const string username = "SomeUsername";

            var userManager = Tests.MockUserManager<User>();
            userManager
                .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = userId });

            var userService = new Mock<IUserService>();
            userService
                .Setup(u => u.GetByUsernameAsync<UserProfileUserModel>(It.Is<string>(id => id == userId)))
                .ReturnsAsync(new UserProfileUserModel { UserName = username });

            var controller = new UsersController(userManager.Object, userService.Object, null);

            // Act
            var result = await controller.Profile(username);

            // Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<UserProfileUserModel>().UserName == username);
        }
    }
}
