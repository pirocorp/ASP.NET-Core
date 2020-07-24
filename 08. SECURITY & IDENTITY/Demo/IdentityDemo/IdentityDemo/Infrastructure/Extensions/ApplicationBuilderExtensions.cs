namespace IdentityDemo.Infrastructure.Extensions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Applies migrations
        /// </summary>
        /// <typeparam name="T">Migration context</typeparam>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder object</returns>
        public static IApplicationBuilder UseDatabaseMigration<T>(this IApplicationBuilder app)
            where T : DbContext
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<T>();
                context.Database.Migrate();

                var roleManager = serviceScope
                    .ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var userManager = serviceScope
                    .ServiceProvider.GetService<UserManager<User>>();

                Task
                    .Run(async () =>
                    {
                        var adminRole = GlobalConstants.AdministratorRole;
                        var roleExists = await roleManager.RoleExistsAsync(adminRole);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole(adminRole));
                        }

                        var adminUserName = GlobalConstants.AdministratorUserName;
                        var adminUser = await userManager.FindByNameAsync(adminUserName);

                        if (adminUser is null)
                        {
                            adminUser = new User()
                            {
                                Email = "admin@mysite.com",
                                UserName = "admin@mysite.com",
                            };

                            await userManager
                                .CreateAsync(adminUser, "admin12");

                            await userManager.AddToRoleAsync(adminUser, adminRole);
                        }
                    })
                    .Wait();
            }   
            
            return app;
        }
    }
}