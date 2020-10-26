namespace Stopify.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Models;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(StopifyDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<StopifyUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var admin = new StopifyUser()
            {
                UserName = "admin",
                Email = "admin@stopify.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            var result = await userManager.CreateAsync(admin, "123456");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);

                var logger = serviceProvider
                    .GetService<ILoggerFactory>()
                    .CreateLogger(typeof(StopifyDbContext));

                logger.LogInformation("Admin was created successfully.");
            }
            else
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}
