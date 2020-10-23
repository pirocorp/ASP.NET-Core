namespace Panda.Services
{
    using System;
    using System.Threading.Tasks;

    using Data;
    using Models;

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

        public async Task<string> CreateAsync(
            string description, 
            double weight, 
            string shippingAddress,
            string recipientId)
        {
            var statusId = this.packageStatusService
                .GetPackageStatusIdByName("Acquired");

            var package = new Package()
            {
                Description = description,
                Weight = weight,
                ShippingAddress = shippingAddress,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(2),
                StatusId = statusId,
            };

            this.pandaDb.Add(package);
            await this.pandaDb.SaveChangesAsync();

            return package.Id;
        }
    }
}
