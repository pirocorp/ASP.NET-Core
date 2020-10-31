namespace Panda.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(PandaDbContext dbContext, IServiceProvider serviceProvider);
    }
}
