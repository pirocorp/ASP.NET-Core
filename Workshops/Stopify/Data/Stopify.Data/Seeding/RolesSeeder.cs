﻿namespace Stopify.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Common;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(StopifyDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (!(role is null))
            {
                return;
            }
           
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}
