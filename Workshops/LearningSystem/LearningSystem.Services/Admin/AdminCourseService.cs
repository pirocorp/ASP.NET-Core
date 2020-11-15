namespace LearningSystem.Services.Admin
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Models.Admin.Courses;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly LearningSystemDbContext dbContext;

        public AdminCourseService(LearningSystemDbContext dbContext)
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
    }
}