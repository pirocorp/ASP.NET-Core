namespace Stopify.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(StopifyDbContext dbContext, IServiceProvider serviceProvider);
    }
}
