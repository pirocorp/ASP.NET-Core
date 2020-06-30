namespace Chushka.Web.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class Seeder
    {
        private const string ADMIN_ROLE_NAME = "Administrator";

        public static void SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetService<RoleManager<IdentityRole>>();
            var adminRoleExists = roleManager.RoleExistsAsync(ADMIN_ROLE_NAME).Result;

            if (!adminRoleExists)
            {
                roleManager
                    .CreateAsync(new IdentityRole(ADMIN_ROLE_NAME))
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
