namespace LearningSystem.Web.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using FluentAssertions;
    using LearningSystem.Services;
    using LearningSystem.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Courses;
    using Moq;
    using Xunit;

    public class CourseServiceTest
    {
        public CourseServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public void AutoMapperIsConfiguredSuccessfullyForSingleMappings()
        {
            var course = new Course()
            {
                Id = 3,
                Name = "Test",
                Description = "Test Course",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Students = new List<StudentCourse>(),
                Trainer = new User() { Name = "Nakov" },
            };

            var automapper = AutoMapperConfig.MapperInstance;

            var result = automapper.Map<HomeIndexCourseListingModel>(course);

            Assert.Equal(course.Id, result.Id);
            Assert.Equal(course.Name, result.Name);
            Assert.Equal(course.Description, result.Description);
            Assert.Equal(course.StartDate, result.StartDate);
            Assert.Equal(course.Trainer.Name, result.TrainerName);
        }

        [Fact]
        public void AutoMapperIsConfiguredSuccessfullyForQueryableMappings()
        {
            var courses = new List<Course>()
            {
                new Course()
                {
                    Id = 3,
                    Name = "Test",
                    Description = "Test Course",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(60),
                    Students = new List<StudentCourse>(),
                    Trainer = new User() { Name = "Nakov" },
                },
                new Course()
                {
                    Id = 4,
                    Name = "Test 2",
                    Description = "Test Course 2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(90),
                    Students = new List<StudentCourse>(),
                    Trainer = new User() { Name = "Kenov" },
                }
            };

            var automapper = AutoMapperConfig.MapperInstance;

            var result = courses.AsQueryable().To<HomeIndexCourseListingModel>().ToList();

            Assert.Equal(courses.Count, result.Count);
        }

        [Fact]
        public async Task AutoMapperIsConfiguredSuccessfullyWithInMemoryDatabase()
        {
            var course1 = new Course()
            {
                Id = 3,
                Name = "Test",
                Description = "Test Course",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(60),
                Trainer = new User() {Name = "Nakov"},
            };

            var course2 = new Course()
            {
                Id = 4,
                Name = "Test 2",
                Description = "Test Course 2",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(90),
                Trainer = new User() {Name = "Kenov"},
            };

            var db = this.GetDatabase();
            await db.AddRangeAsync(course1, course2);

            await db.SaveChangesAsync();

            var result = db.Courses.To<HomeIndexCourseListingModel>().ToList();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindAsyncShouldReturnCorrectResultWithFilterAndOrder()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCourse = new Course { Id = 1, Name = "First", StartDate = DateTime.Now, Trainer = new User() { Name = "Nakov" } };
            var secondCourse = new Course { Id = 2, Name = "Second", StartDate = DateTime.Now, Trainer = new User() { Name = "Asen" } };
            var thirdCourse = new Course { Id = 3, Name = "Third", StartDate = DateTime.Now, Trainer = new User() { Name = "Kenov" } };

            await db.AddRangeAsync(firstCourse, secondCourse, thirdCourse);

            await db.SaveChangesAsync();

            var mockUser = Tests.MockUserManager<User>();
            var courseService = new CourseService(db, mockUser.Object);

            // Act
            var result = await courseService.SearchAsync<HomeIndexCourseListingModel>("t", 10);

            // Assert
            result.collection
                .ToArray()
                .Should()
                .Match(r => r.ElementAt(0).Id == 3
                            && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task SignUpStudentAsyncShouldSaveCorrectDataWithValidCourseIdAndStudentId()
        {
            // Arrange
            var db = this.GetDatabase();

            const int courseId = 1;
            const string studentId = "TestStudentId";

            var course = new Course
            {
                Id = courseId,
                StartDate = DateTime.MaxValue,
                Students = new List<StudentCourse>()
            };

            db.Add(course);

            var user = new User()
            {
                Id = studentId,
                Name = "Test User",
                BirthDate = DateTime.UtcNow.AddYears(-20),
            };

            db.Add(user);

            await db.SaveChangesAsync();

            // Mocking userManager
            var userManager = Tests.MockUserManager<User>();
            userManager
                .Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);
            
            var courseService = new CourseService(db, userManager.Object);

            // Creating Claims Principle for current logged user
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, studentId),
                new Claim(ClaimTypes.Name, "test@example.com")
                // other required and custom claims
            }, "TestAuthentication"));

            // Act
            await courseService.SignInUserAsync(courseId, claimsPrincipal);
            var savedEntry = db.Find<StudentCourse>(studentId, courseId);

            // Assert
            savedEntry
                .Should()
                .NotBeNull();
        }

        private LearningSystemDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<LearningSystemDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LearningSystemDbContext(dbOptions);
        }
    }
}
