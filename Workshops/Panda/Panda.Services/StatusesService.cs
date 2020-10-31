namespace Panda.Services
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class StatusesService : IStatusesService
    {
        private readonly PandaDbContext pandaDb;

        public StatusesService(PandaDbContext pandaDb)
        {
            this.pandaDb = pandaDb;
        }

        public async Task<string> GetPackageStatusIdByNameAsync(string name)
        {
            if (name is null)
            {
                return string.Empty;
            }

            var status = await this.pandaDb
                .PackageStatuses
                .FirstOrDefaultAsync(ps => ps.Name.Equals(name));

            return status is null ? string.Empty : status.Id;
        }

        public Task<bool> ExistsAsync(string statusId)
            => this.pandaDb.PackageStatuses.AnyAsync(ps => ps.Id.Equals(statusId));
    }
}
