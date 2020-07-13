namespace Eventures.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        private const string ADMIN_ROLE_NAME = "Admin";
        private const string USER_ROLE_NAME = "User";

        /// <summary>
        /// Applies migrations
        /// </summary>
        /// <typeparam name="T">Migration context</typeparam>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder object</returns>
        public static IApplicationBuilder UseDatabaseMigration<T>(this IApplicationBuilder app)
            where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<T>();
            context.Database.Migrate();

            return app;
        }

        /// <summary>
        /// Seed roles
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder object</returns>
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var adminRoleExists = roleManager.RoleExistsAsync(ADMIN_ROLE_NAME).Result;

            if (adminRoleExists)
            {
                return app;
            }

            roleManager
                .CreateAsync(new IdentityRole(ADMIN_ROLE_NAME))
                .GetAwaiter()
                .GetResult();

            roleManager
                .CreateAsync(new IdentityRole(USER_ROLE_NAME))
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
