namespace FrontEnd.Services
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminService : IAdminService
    {
        private readonly IServiceProvider serviceProvider;

        private bool adminExists;

        public AdminService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<bool> AllowAdminUserCreationAsync()
        {
            if (this.adminExists)
            {
                return false;
            }
            else
            {
                using var scope = this.serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

                if (await dbContext.Users.AnyAsync(user => user.IsAdmin))
                {
                    // There are already admin users so disable admin creation
                    this.adminExists = true;
                    return false;
                }

                // There are no admin users so enable admin creation
                return true;
            }
        }
    }
}
