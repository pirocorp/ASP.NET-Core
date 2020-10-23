namespace Panda.Services
{
    using System;
    using System.Threading.Tasks;

    using Data;
    using Models;

    public class PackageService : IPackageService
    {
        private readonly PandaDbContext pandaDb;

        public PackageService(PandaDbContext pandaDb)
        {
            this.pandaDb = pandaDb;
        }

        public async Task<string> CreateAsync(
            string description, 
            double weight, 
            string shippingAddress,
            string recipientId)
        {
            var package = new Package()
            {
                Description = description,
                Weight = weight,
                ShippingAddress = shippingAddress,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(2),
            };

            this.pandaDb.Add(package);
            await this.pandaDb.SaveChangesAsync();

            return package.Id;
        }
    }
}
