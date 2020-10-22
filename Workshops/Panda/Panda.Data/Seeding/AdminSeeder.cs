namespace Panda.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Panda.Models;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(PandaDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<PandaUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var admin = new PandaUser
            {
                UserName = "admin@abv.bg",
                Email = "admin@abv.bg",
            };

            var result = await userManager.CreateAsync(admin, "123456");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");

                var logger = serviceProvider
                    .GetService<ILoggerFactory>()
                    .CreateLogger(typeof(PandaDbContext));

                logger.LogInformation("User created a new account with password.");
            }
            else
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}
