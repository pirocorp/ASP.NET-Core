namespace LearningSystem.Services.Admin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext dbContext;

        public AdminUserService(LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TOut>> AllAsync<TOut>()
            => await this.dbContext.Users
                .To<TOut>()
                .ToListAsync();
    }
}
