namespace LearningSystem.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(LearningSystemDbContext dbContext, IServiceProvider serviceProvider);
    }
}
