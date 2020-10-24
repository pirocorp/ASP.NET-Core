namespace Panda.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using Mapping;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Panda.Infrastructure;
    using Panda.Models;

    public class PackagesService : IPackagesService
    {
        private readonly PandaDbContext pandaDb;
        private readonly IStatusesService statusesService;

        public PackagesService(
            PandaDbContext pandaDb,
            IStatusesService statusesService)
        {
            this.pandaDb = pandaDb;
            this.statusesService = statusesService;
        }

        public async Task<string> CreateAsync(PackageCreateServiceModel model)
        {
            var statusId = await this.statusesService
                .GetPackageStatusIdByNameAsync(ShipmentStatus.Pending.ToString());

            model.StatusId = statusId;
            var package = model.To<Package>();

            this.pandaDb.Add(package);
            await this.pandaDb.SaveChangesAsync();

            return package.Id;
        }

        public async Task<IEnumerable<TOutput>> GetPackagesByStatusCodeAsync<TOutput>(ShipmentStatus status)
        {
            var statusId = await this
                .statusesService
                .GetPackageStatusIdByNameAsync(status.ToString());

            return await this.GetPackagesByStatusCodeAsync<TOutput>(statusId);
        }

        public async Task<IEnumerable<TOutput>> GetPackagesByStatusCodeAsync<TOutput>(string statusId)
            => await this.pandaDb
                .Packages
                .Where(p => p.StatusId.Equals(statusId))
                .To<TOutput>()
                .ToListAsync();

        public async Task<bool> ExistsAsync(string packageId)
            => await this.pandaDb
                .Packages
                .AnyAsync(p => p.Id.Equals(packageId));

        public async Task<bool> ChangeStatusAsync(string packageId, ShipmentStatus status)
        {
            var statusId = await this
                .statusesService
                .GetPackageStatusIdByNameAsync(status.ToString());

            return await this.ChangeStatusAsync(packageId, statusId);
        }

        public async Task<bool> ChangeStatusAsync(string packageId, string statusId)
        {
            if (   !await this.ExistsAsync(packageId) 
                || !await this.statusesService.ExistsAsync(statusId))
            {
                return false;
            }

            var package = this.pandaDb
                .Packages
                .First(p => p.Id == packageId);

            package.StatusId = statusId;
            await this.pandaDb.SaveChangesAsync();

            return true;
        }

        public async Task<TOutput> GetByIdAsync<TOutput>(string packageId)
            => await this.pandaDb
                .Packages
                .Where(p => p.Id.Equals(packageId))
                .To<TOutput>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TOutput>> GetAllPackagesInTheSystemAsync<TOutput>()
            => await this.pandaDb.Packages
                .Where(p => !p.Status.Name.Equals(ShipmentStatus.Acquired.ToString()))
                .To<TOutput>()
                .ToListAsync();

        public async Task<IEnumerable<TOutput>> GetAllUserPackagesAsync<TOutput>(string userId)
            => await this.pandaDb.Packages
                .Where(p => p.RecipientId.Equals(userId))
                .To<TOutput>()
                .ToListAsync();
    }
}
