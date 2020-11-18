namespace LearningSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.Admin.Courses;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext dbContext;
        private readonly UserManager<User> userManager;

        public CourseService(
            LearningSystemDbContext dbContext,
            UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<int> CreateAsync(CreateCourseServiceModel model)
        {
            var course = new Course()
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                TrainerId = model.TrainerId
            };

            await this.dbContext.AddAsync(course);
            await this.dbContext.SaveChangesAsync();

            return course.Id;
        }

        public async Task<IEnumerable<TOut>> ActiveAsync<TOut>()
            => await this.dbContext.Courses
                .Where(c => c.StartDate > DateTime.UtcNow)
                .OrderByDescending(c => c.StartDate)
                .To<TOut>()
                .ToListAsync();

        public async Task<TOut> GetById<TOut>(int id)
            => await this.dbContext.Courses
                .Where(c => c.Id.Equals(id))
                .To<TOut>()
                .FirstOrDefaultAsync();

        public async Task<bool> UserIsSignedInCourse(int courseId, ClaimsPrincipal user)
            => await this.UserIsSignedInCourse(courseId, this.userManager.GetUserId(user));

        public async Task<bool> UserIsSignedInCourse(int courseId, string userId)
            => await this.dbContext.Courses
                .Where(c => c.Id.Equals(courseId))
                .AnyAsync(c => c.Students.Any(s => s.StudentId.Equals(userId)));

        public async Task<bool> ExistsAsync(int courseId)
            => await this.dbContext.Courses
                .AnyAsync(c => c.Id.Equals(courseId));

        public async Task SignInUserAsync(int courseId, ClaimsPrincipal user)
        {
            var course = await this.dbContext
                .Courses
                .FirstAsync(c => c.Id.Equals(courseId));

            var currentUser = await this.userManager.GetUserAsync(user);

            var studentCourse = new StudentCourse()
            {
                Course = course,
                Student = currentUser
            };

            await this.dbContext.AddAsync(studentCourse);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task SignOutUserAsync(int courseId, ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);

            var studentCourse = await this.dbContext.StudentCourses
                .FirstOrDefaultAsync(sc => 
                       sc.StudentId.Equals(userId) 
                    && sc.CourseId.Equals(courseId));

            this.dbContext.Remove(studentCourse);
            await this.dbContext.SaveChangesAsync();
        }
    }
}