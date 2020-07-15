namespace Demo.Seeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Data;

    public class ApplicationDbContextSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextSeeder(
            IServiceProvider serviceProvider,
            ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;

            this._userManager = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetService<UserManager<IdentityUser>>();

            this._roleManager = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetService<RoleManager<IdentityRole>>();
        }

        public async Task SeedDataAsync()
        {
            await this.SeedUsersAsync();
            await this.SeedRolesAsync();
            await this.SeedUserToRolesAsync();
        }

        private async Task SeedUsersAsync()
        {
            var user = await this._userManager
                .FindByNameAsync("admin");

            if (user != null)
            {
                return;
            }

            await this._userManager
                .CreateAsync(new IdentityUser()
                {
                    UserName = "admin@admin.admin",
                    Email = "admin@admin.admin",
                    EmailConfirmed = true
                }, "admin");

            await this._userManager
                .CreateAsync(new IdentityUser()
                {
                    UserName = "moderator@admin.admin",
                    Email = "moderator@admin.admin",
                    EmailConfirmed = true
                }, "moderator");

            await this._userManager
                .CreateAsync(new IdentityUser()
                {
                    UserName = "user@abv.bg",
                    Email = "user@abv.bg",
                    EmailConfirmed = true
                }, "123");
        }

        private async Task SeedRolesAsync()
        {
            var role = await this._roleManager
                .FindByNameAsync("admin");

            if (role != null)
            {
                return;
            }

            await this._roleManager
                .CreateAsync(new IdentityRole()
                {
                    Name = "admin"
                });

            await this._roleManager
                .CreateAsync(new IdentityRole()
                {
                    Name = "moderator"
                }); 

            await this._roleManager
                .CreateAsync(new IdentityRole()
                {
                    Name = "user"
                });
        }

        private async Task SeedUserToRolesAsync()
        {
            var user = await this._userManager.FindByNameAsync("admin@admin.admin");
            var role = await this._roleManager.FindByNameAsync("admin");

            if (this._dbContext.UserRoles.Any())
            {
                return;
            }

            this._dbContext.Add(new IdentityUserRole<string>()
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            user = await this._userManager.FindByNameAsync("moderator@admin.admin");
            role = await this._roleManager.FindByNameAsync("moderator");

            this._dbContext.Add(new IdentityUserRole<string>()
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            role = await this._roleManager.FindByNameAsync("user");

            this._dbContext.Users
                .Where(u => u.UserName != "admin@admin.admin" 
                         && u.UserName != "moderator@admin.admin")
                .ToList()
                .ForEach(u =>
                {
                    this._dbContext.Add(new IdentityUserRole<string>()
                    {
                        RoleId = role.Id,
                        UserId = u.Id
                    });
                });

            await this._dbContext.SaveChangesAsync();
        }
    }
}
