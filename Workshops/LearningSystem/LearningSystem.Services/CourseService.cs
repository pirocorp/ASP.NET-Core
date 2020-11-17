namespace LearningSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models.Admin.Courses;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext dbContext;

        public CourseService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
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
    }
}