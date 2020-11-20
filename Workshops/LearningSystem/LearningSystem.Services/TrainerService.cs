namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.Trainers;

    public class TrainerService : ITrainerService
    {
        private readonly UserManager<User> userManager;
        private readonly LearningSystemDbContext dbContext;

        public TrainerService(
            UserManager<User> userManager,
            LearningSystemDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TOut>> GetTrainingsAsync<TOut>(ClaimsPrincipal trainer)
        {
            var trainerId = this.userManager.GetUserId(trainer);

            return await this.dbContext.Courses
                .Where(c => c.TrainerId.Equals(trainerId))
                .To<TOut>()
                .ToListAsync();
        }

        public async Task<bool> IsTrainerAsync(int courseId, ClaimsPrincipal trainer)
        {
            var trainerId = this.userManager.GetUserId(trainer);

            return await this.dbContext.Courses
                .Where(c => c.Id.Equals(courseId) && c.TrainerId.Equals(trainerId))
                .AnyAsync();
        }

        public async Task<IEnumerable<TOut>> GetAllStudentsInCourseAsync<TOut>(int courseId)
            => await this.dbContext.Users
                .Where(u => u.Courses.Any(c => c.CourseId.Equals(courseId)))
                .To<TOut>(new { courseId })
                .ToListAsync();

        public async Task<bool> Grade(GradeServiceModel model)
        {
            var studentInCourse = await this.dbContext.StudentCourses
                .SingleOrDefaultAsync(g => g.CourseId.Equals(model.CourseId) && g.StudentId.Equals(model.StudentId));

            if (studentInCourse is null)
            {
                return false;
            }

            studentInCourse.Grade = model.Grade;

            this.dbContext.Update(studentInCourse);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<byte[]> GetExamSubmission(int courseId, string studentId)
            => (await this.dbContext.FindAsync<StudentCourse>(studentId, courseId))?.ExamSubmission;
    }
}