namespace LearningSystem.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using LearningSystem.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(LearningSystemDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var adminEmail = "admin@admin.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser is null)
            {
                adminUser = new User()
                {
                    Email = adminEmail,
                    UserName = "admin",
                    Name = "Admin",
                    BirthDate = DateTime.MinValue
                };

                await userManager.CreateAsync(adminUser, "123456"); // Crazy hard password :)
            }

            await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRole);
        }
    }
}
