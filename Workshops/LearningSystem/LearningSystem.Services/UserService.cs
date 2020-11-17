namespace LearningSystem.Services
{
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
    }
}
