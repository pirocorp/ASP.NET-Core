namespace LearningSystem.Services
{
    using System;
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

        public async Task<(IEnumerable<TOut> collection, int count)> SearchAsync<TOut>(string filter, int pageSize, int page = 1)
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

            var count = (int)Math.Ceiling((query.Count() / (double) pageSize));

            var skip = (page - 1) * pageSize;

            var collection =  await query
                .OrderBy(x => x.UserName)
                .Skip(skip)
                .Take(pageSize)
                .To<TOut>()
                .ToListAsync();

            return (collection, count);
        }
    }
}
