namespace Panda.Services
{
    using System;
    using System.Threading.Tasks;

    using Data;
    using Mapping;
    using Models;
    using Panda.Models;

    public class PackageService : IPackageService
    {
        private readonly PandaDbContext pandaDb;
        private readonly IPackageStatusService packageStatusService;

        public PackageService(
            PandaDbContext pandaDb,
            IPackageStatusService packageStatusService)
        {
            this.pandaDb = pandaDb;
            this.packageStatusService = packageStatusService;
        }

        public async Task<string> CreateAsync(PackageCreateServiceModel model)
        {
            var statusId = this.packageStatusService
                .GetPackageStatusIdByName("Acquired");

            model.StatusId = statusId;
            var package = model.To<Package>();

            this.pandaDb.Add(package);
            await this.pandaDb.SaveChangesAsync();

            return package.Id;
        }
    }
}
