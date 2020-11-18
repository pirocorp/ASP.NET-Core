namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly LearningSystemDbContext dbContext;

        public UserService(
            LearningSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TOut> GetByUsernameAsync<TOut>(string userId)
            => await this.dbContext.Users
                .Where(u => u.Id.Equals(userId))
                .To<TOut>(new{ studentId = userId })
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<TOut>> SearchAsync<TOut>(string filter = "")
        {
            var query = this.dbContext.Users.Select(x => x);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.ToLower();

                query = query.Where(x
                    => x.UserName.Contains(filter)
                       || x.Email.Contains(filter)
                       || x.Name.Contains(filter));
            }

            return await query
                .To<TOut>()
                .ToListAsync();
        }
    }
}
